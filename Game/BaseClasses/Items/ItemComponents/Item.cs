using Gum.Forms.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBase.Items;
public abstract class Item : Component
{
    public ItemInfo Info { get; protected set; }


    public HitboxComponent hitbox;

    public InteractableComponent interact;
    //should i have a data container for items? like a scriptable object? 

    public Item(GameObject gameObject, ItemInfo info): base(gameObject)
    {
        Info = info;
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

    public override void Connect()
    {
        base.Connect();
        hitbox = owner.GetComponent<HitboxComponent>();
        interact = owner.GetComponent<InteractableComponent>();
        if (hitbox != null)
        {
            hitbox.CollisionStay += OnCollision;
            hitbox.CollisionEnter += OnCollisionEnter;
            hitbox.CollisionExit += OnCollisionExit;
        }
        if (interact != null)
        {
            interact.Interacted += OnInteract;
        }
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
