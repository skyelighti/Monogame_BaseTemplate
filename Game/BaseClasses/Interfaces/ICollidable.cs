using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBase;
public interface ICollidable
{
    bool IsActive { get; }
    //pooling.... maybe new interface? but too fragmented
    bool IsTrigger { get; }
    bool ColliderIsActive { get; }
    //idk seems useful might remove turn into trigger? 
    Rectangle BoxCollider { get; }
    //implement this in colliable gameobject class
    //public Rectangle BoxCollider
    //    {
    //        get
    //        {
    //            return new Rectangle((int) transform.position.X, (int) transform.position.Y, (int) transform.scale.X, (int) transform.scale.Y);
    //    }
    //}
    public Transform transform { get; }

    void OnCollision(ICollidable other);
    void OnCollisionEnter(ICollidable other);
    void OnCollisionExit(ICollidable other);
    //each object can manage it's reaction to collisions
    Layer layer { get; }
    GameObject owner { get; }
}

