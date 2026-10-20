using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBase;

public class BasicSolidGameObject : GameObject
{
    //other interfaces like iinteractable can also be implemented here
    public HitboxComponent hitbox;
    public BasicSolidGameObject(string spriteName, Scene owner, string name = null) : base(spriteName, owner, name)
    {
        //update sprites, animated sprites and then size in here
        var hb = new HitboxComponent(GetComponent<TransformComponent>(), this);
        hitbox = hb;
        AddComponent(hb);
        hb.CollisionStay += OnCollision;
        hb.CollisionEnter += OnCollisionEnter;
        hb.CollisionExit += OnCollisionExit;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }
    public void OnCollision(ICollidable col) { 
    }
    public void OnCollisionEnter(ICollidable col)
    {
    }
    public void OnCollisionExit(ICollidable col)
    {
    }
}
