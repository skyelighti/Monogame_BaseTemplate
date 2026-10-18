using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBase;

public class BasicSolidGameObject : GameObject, ICollidable
{
    //other interfaces like iinteractable can also be implemented here
    public BasicSolidGameObject(string spriteName, Scene owner, string name = null) : base(spriteName, owner, name)
    {
        //update sprites, animated sprites and then size in here

    }

    public Rectangle BoxCollider
    {
        get
        {
            return new Rectangle((int)transform.position.X, (int)transform.position.Y, (int)transform.scale.X, (int)transform.scale.Y);
        }
    }

    public bool IsTrigger { get; protected set; } = true;

    public bool ColliderIsActive { get; protected set; } = true;

    Layer ICollidable.layer { get;} = Layer.Default;

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }


    public void OnCollision(ICollidable other)
    {
        throw new NotImplementedException();
    }

    public void OnCollisionEnter(ICollidable other)
    {
        throw new NotImplementedException();
    }

    public void OnCollisionExit(ICollidable other)
    {
        throw new NotImplementedException();
    }
}
