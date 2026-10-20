using System;
using Microsoft.Xna.Framework;

namespace GameBase.Animations;

public class AnimatedSprite : Sprite
{
    private SpriteSheet spriteSheet;
    private IAnimation pendingAnimation;

    public AnimationController Controller { get; }

    public AnimatedSprite(SpriteSheet sheet, string animationName)
    {
        spriteSheet = sheet;
        Controller = new AnimationController();

        Controller.ChangeAnimation(sheet.GetAnimation(animationName));
        Controller.Play();

        UpdateRegion();
    }
    private void UpdateRegion()
    {
        IAnimationFrame frame = Controller.Animation.Frames[Controller.CurrentFrame];

        Region = spriteSheet.GetTextureRegion(frame.AtlasFrame);
    }

    public void RequestAnimation(string animationName)
    {
        pendingAnimation = spriteSheet.GetAnimation(animationName);
    }

    public override void Update(GameTime gameTime)
    {
        Controller.Update(gameTime);

        if (pendingAnimation != null)
        {
            IAnimation nextAnimation = pendingAnimation;
            pendingAnimation = null;

            Controller.ChangeAnimation(nextAnimation);
            Controller.Play();
        }

        UpdateRegion();
    }
}
