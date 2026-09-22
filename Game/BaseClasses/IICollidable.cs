using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGameLibrary.Graphics;


public interface ICollidable
{
    bool IsActive { get; }
    //pooling.... maybe new interface? but too fragmented
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
    Sprite sprite { get; }

    void OnCollision(ICollidable other);
    //each object can manage it's reaction to collisions
}

