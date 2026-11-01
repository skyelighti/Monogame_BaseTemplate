using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBase;

public class SpawnEffect : GameObject
{
    //other interfaces like iinteractable can also be implemented here
    public HitboxComponent hitbox;
    GameObject objToSpawn;
    AnimatedSprite animSprite;

    public SpawnEffect(GameObject obj, string spriteName, Scene owner, string sheetName, string animationName, int size = 16, string name = null) : this(obj, Services.Sprites.CreateSprite(spriteName), owner, sheetName, animationName, size, name)
    {
    }

    public SpawnEffect(GameObject obj, Sprite sprite, Scene owner, string sheetName, string animationName, int size = 16, string name = null) : base(sprite, owner, name)
    {
        animSprite = Services.Sprites.CreateAnimatedSprite(sheetName, animationName);
        spriteC.UpdateSprite(animSprite);
        animSprite.Controller.OnAnimationEvent += OnAnimEvent;
        obj.SetActive(false);
        objToSpawn = obj;
        UpdateSize(size,size);
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }

    public override void OnDestroy()
    {
        animSprite.Controller.OnAnimationEvent -= OnAnimEvent;
        base.OnDestroy();
    }
    public void OnAnimEvent(IAnimationController anim, AnimationEventTrigger trigger) {
        if (trigger == AnimationEventTrigger.AnimationCompleted) {
            objToSpawn.SetActive(true);
            SetActive(false);
            ownerScene.RemoveObject(this);
            //spawn object and set active false? can i pool this
        }
    }
}
