using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBase.Items;
public abstract class Item : GameObject, ICollidable, IInteractable
{
    public ItemInfo Info { get; protected set; }
    public string ID { get; protected set; }
    public Rectangle BoxCollider
    {
        get
        {
            return new Rectangle((int)transform.position.X, (int)transform.position.Y, (int)transform.scale.X, (int)transform.scale.Y);
        }
    }
    public bool CanInteract { get; protected set; } = true;

    public bool ColliderIsActive { get; protected set; } = true;

    public bool IsTrigger => true;


    //should i have a data container for items? like a scriptable object? 


    public Item(ContentManager content, string spriteName, string name, Scene owner) : base(content, spriteName, name, owner)
    {
        layer = Layer.Interactable;
    }

    public abstract void Interact(GameObject player);

    public virtual void OnCollision(ICollidable other)
    { }
    public virtual void OnCollisionEnter(ICollidable other)
    { }
    public virtual void OnCollisionExit(ICollidable other)
    { }
}
