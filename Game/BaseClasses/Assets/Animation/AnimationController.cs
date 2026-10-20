using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBase.Animations;

public class AnimationController: IAnimationController
{
    public bool IsPaused { get; private set; }
    bool isPlaying;

    public IAnimation Animation { get; private set; }

    public bool IsAnimating => isPlaying && !IsPaused;

    public bool IsLooping => Animation?.IsLooping ?? false;
    public bool IsReversed => Animation?.IsReversed ?? false;
    public bool IsPingPong => Animation?.IsPingPong ?? false;
    public double Speed { get; private set; } = 1;
    private int direction = 1;

    public TimeSpan CurrentFrameTimeRemaining { get; private set; }

    public int CurrentFrame { get; private set; }

    public int FrameCount => Animation?.FrameCount ?? 0;
    //if animation isnt null gets frame count, otherwise 0

    private IAnimation pendingAnimation;
    public event Action<IAnimationController, AnimationEventTrigger> OnAnimationEvent;

    public void ChangeAnimation(IAnimation animation)
    {
        if (animation == null) { return; }
        Animation = animation;
        direction = IsReversed ? -1 : 1;
        SetFrame(direction == 1 ? 0 : FrameCount - 1);
        //need to copy playback settings
    }
    public void SetSpeed(double speed) {
        if (speed >= 0) {
            Speed = speed;
        }
    }
    public void SetFrame(int index)
    {
        if (Animation == null) { return; }
        if (index < FrameCount && index >= 0)
        {
            CurrentFrame = index;
            CurrentFrameTimeRemaining = Animation.Frames[index].Duration;
            OnAnimationEvent?.Invoke(this, AnimationEventTrigger.FrameBegin);
        }
    }
    public void RequestAnimation(IAnimation animation)
    {
        pendingAnimation = animation;
    }
    public bool Pause()
    {
        if (!IsAnimating) { 
            return false;
            //cannot pause cuz already paused
        }

        IsPaused = true;
        return true;
    }

    public bool Pause(bool resetFrameDuration)
    {
    
        if (!Pause())
            return false;

        if (resetFrameDuration)
            CurrentFrameTimeRemaining = Animation.Frames[CurrentFrame].Duration;

        return true;
    }

    public bool Play()
    {
        int startingFrame = IsReversed ? FrameCount - 1 : 0;
        return Play(startingFrame);
    }

    public bool Play(int startingFrame)
    {
        if (Animation == null || FrameCount == 0) { return false; }
        isPlaying = true;
        IsPaused = false;
        direction = IsReversed ? -1 : 1;
        SetFrame(startingFrame);
        return true;
    }

    public void Reset()
    {
        Stop();
        direction = IsReversed ? -1 : 1;
        SetFrame(IsReversed ? FrameCount - 1 : 0);
    }

    public bool NextFrame() {
        if (Animation == null) { return false; }
        int nextF = CurrentFrame + direction;
        if (nextF >= 0 && nextF < FrameCount)
        {
            SetFrame(nextF);
            return true;
        }
        if (FrameCount == 1)
        {
            if (!IsLooping) 
            {
                Complete();
                return false;
            }

            SetFrame(0);
            OnAnimationEvent?.Invoke(this, AnimationEventTrigger.AnimationLoop);
            return true;
        }
        if (IsPingPong)
        {
            int startingDirection = IsReversed ? -1 : 1;
            bool returning = direction != startingDirection;

            if (returning && !IsLooping)
            {
                Complete();
                return false;
            }

            direction = -direction;
            SetFrame(CurrentFrame + direction);
            return true;
        }

        if (IsLooping)
        {
            SetFrame(direction == 1 ? 0 : FrameCount - 1);
            OnAnimationEvent?.Invoke(this, AnimationEventTrigger.AnimationLoop);
            return true;
        }

        Complete();
        return false;

    }

    public bool Complete() {
        isPlaying = false;
        IsPaused = false;
        CurrentFrameTimeRemaining = TimeSpan.Zero;

        OnAnimationEvent?.Invoke(this, AnimationEventTrigger.AnimationCompleted);
        return true;
    }

    public bool Stop()
    {
        if (!isPlaying && !IsPaused) { return true; }
        isPlaying = false;
        IsPaused = false;
        OnAnimationEvent?.Invoke(this, AnimationEventTrigger.AnimationStopped);
        return true;

    }

    public bool Unpause()
    {
        if (!isPlaying || !IsPaused)
        {
            return false;
        }

        IsPaused = false;
        return true;
    }

    public bool Unpause(bool advanceToNextFrame)
    {
        if (!isPlaying || !IsPaused)
            return false;

        if (advanceToNextFrame && !NextFrame())
            return false;

        return Unpause();
    }

    public void Update(GameTime gameTime)
    {
        if (!IsAnimating || Speed == 0)
            return;

        CurrentFrameTimeRemaining -= gameTime.ElapsedGameTime * Speed;

        while (IsAnimating && CurrentFrameTimeRemaining <= TimeSpan.Zero)
        {
            TimeSpan leftover = CurrentFrameTimeRemaining;
            OnAnimationEvent?.Invoke(this, AnimationEventTrigger.FrameEnd);
            if (!IsAnimating) return;
            if (!NextFrame())
            {
                return;
            }

            CurrentFrameTimeRemaining += leftover;
        }
    }
    //holy fuck im not implementing this this week
    //https://deepwiki.com/craftworkgames/MonoGame.Extended/2.2-animation-system

}
