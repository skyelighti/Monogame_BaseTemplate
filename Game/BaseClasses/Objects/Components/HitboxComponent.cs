using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBase;

public class HitboxComponent : Component, ICollidable
{
    TransformComponent transformComponent;
    Hitbox hitbox;

    public bool IsTrigger => hitbox.IsTrigger;

    public bool ColliderIsActive => hitbox.ColliderIsActive;

    public Rectangle BoxCollider => hitbox.BoxCollider;

    public Layer layer => hitbox.layer;

    public Transform transform => transformComponent.transform;
    public event Action<ICollidable> CollisionEnter
    {
        add { hitbox.CollisionEnter += value; }
        remove { hitbox.CollisionEnter -= value; }
    }

    public event Action<ICollidable> CollisionExit
    {
        add { hitbox.CollisionExit += value; }
        remove { hitbox.CollisionExit -= value; }
    }

    public event Action<ICollidable> CollisionStay
    {
        add { hitbox.CollisionStay += value; }
        remove { hitbox.CollisionStay -= value; }
    }

    public HitboxComponent(TransformComponent t, GameObject o) : base(o)
    {
        transformComponent = t;
        var hb = new Hitbox(transformComponent.transform, o);
        hitbox = hb;
    }
    public HitboxComponent(GameObject o): base(o)
    {
        owner = o;
        var hb = new Hitbox(o);
        hitbox = hb;
        transformComponent = o.GetComponent<TransformComponent>();
    }

    public void OnCollision(ICollidable other)
    {
        hitbox.OnCollision(other);
    }

    public void OnCollisionEnter(ICollidable other)
    {
        hitbox.OnCollisionEnter(other);
    }

    public void OnCollisionExit(ICollidable other)
    {

        hitbox.OnCollisionExit(other);
    }
    public bool SetTrigger(bool state)
    {
        return hitbox.SetTrigger(state);
    }
    public override bool SetActive(bool state)
    {
        hitbox.SetActive(state);
        return base.SetActive(state);
    }
    public bool SetColliderActive(bool state)
    {
        return hitbox.SetColliderActive(state);
    }
}
