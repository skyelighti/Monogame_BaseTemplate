using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GameBase;

public class Hitbox : ICollidable
{
    GameObject owner;
    public bool IsActive => owner.IsActive;

    public bool IsTrigger => true;

    public bool ColliderIsActive { get; set; } = false;

    public Rectangle BoxCollider => new Rectangle((int)transform.position.X, (int)transform.position.Y, (int)transform.scale.X, (int)transform.scale.Y);

    public Transform transform { get; private set; }

    public Layer layer => Layer.Default;
    public Hitbox(GameObject o, Transform t)
    {
        owner = o;
        transform = t;
    }

    public void OnCollision(ICollidable other)
    {

    }

    public void OnCollisionEnter(ICollidable other)
    {
        if (other.layer == owner.layer)
        {
            return;
        }
        if (other.layer == Layer.Enemy || other.layer == Layer.Obstacle)
        {
            if (other is IDamagable damagable)
            {
                damagable.takeDamage(1);
            }
        }
    }

    public void OnCollisionExit(ICollidable other)
    {

    }

    public void UpdateTransform(Transform t)
    {
        transform = t;
    }
}
