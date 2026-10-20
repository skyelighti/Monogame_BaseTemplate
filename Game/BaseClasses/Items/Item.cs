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


    public Item(string spriteName, Scene owner, string name = null) : base(spriteName, owner, name)
    {
        layer = Layer.Interactable;

        var hb = new HitboxComponent(GetComponent<TransformComponent>(), this);
        hitbox = hb;
        AddComponent(hb);
        var i = new InteractableComponent(this);
        interact = i;
        AddComponent(i);
        hb.CollisionStay += OnCollision;
        hb.CollisionEnter += OnCollisionEnter;
        hb.CollisionExit += OnCollisionExit;
        i.Interacted += OnInteract;
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

}
