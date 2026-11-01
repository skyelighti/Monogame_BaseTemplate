using System;
using Microsoft.Xna.Framework;

namespace GameBase.Animations;

public class AnimatedSprite : Sprite
{
    private IAnimation pendingAnimation;
    private Vector2 offSet = Vector2.Zero;

    public AnimationController Controller { get; }



    public AnimatedSprite(IAnimation anim)
    {
        Controller = new AnimationController();

        Controller.ChangeAnimation(anim);
        Controller.Play();

        UpdateRegion();
    }
    private void UpdateRegion()
    {
        IAnimationFrame frame = Controller.Animation.Frames[Controller.CurrentFrame];

        Region = frame.Region;
    }


    public override void Update(GameTime gameTime)
    {
        

        if (pendingAnimation != null)
        {
            IAnimation nextAnimation = pendingAnimation;
            pendingAnimation = null;
            DrawOffset = offSet;

            Controller.ChangeAnimation(nextAnimation);
            Controller.Play();
        }
        Controller.Update(gameTime);

        UpdateRegion();
    }
    public void RequestAnimation(IAnimation animation, Vector2 offset = default)
    {
        if (animation == null) { return; }
        pendingAnimation = animation;
        offSet = offset;
    }
}
