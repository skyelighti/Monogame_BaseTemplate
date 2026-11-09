using Gum.Forms.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBase.Items;
public abstract class Item : GameObject
{
    public ItemInfo Info { get; protected set; }


    public HitboxComponent hitbox;

    public InteractableComponent interact;
    //should i have a data container for items? like a scriptable object? 

    protected Item( Sprite sprite, Scene scene, ItemInfo info, string name = null): base(sprite, scene, name)
    {
        Info = info;

        hitbox = new HitboxComponent(this);
        interact = new InteractableComponent(this);

        AddComponent(hitbox);
        AddComponent(interact);

        hitbox.CollisionStay += OnCollision;
        hitbox.CollisionEnter += OnCollisionEnter;
        hitbox.CollisionExit += OnCollisionExit;
        interact.Interacted += OnInteract;
    }
    public virtual void OnInteract(GameObject obj) { 
        //checking for interaction is already handled by the component
    }
    public virtual void OnCollision(ICollidable col)
    {
    }
    public virtual void OnCollisionEnter(ICollidable col)
    {
    }
    public virtual void OnCollisionExit(ICollidable col)
    {
    }

    public override void OnDestroy()
    {
        if (hitbox != null)
        {
            hitbox.CollisionStay -= OnCollision;
            hitbox.CollisionEnter -= OnCollisionEnter;
            hitbox.CollisionExit -= OnCollisionExit;
        }

        if (interact != null)
            interact.Interacted -= OnInteract;
        base.OnDestroy();
    }
}
