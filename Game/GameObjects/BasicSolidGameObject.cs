using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseTemplate.Game.GameObjects
{
    public class BasicSolidGameObject : GameObject, ICollidable
    {
        //other interfaces like iinteractable can also be implemented here
        public BasicSolidGameObject(ContentManager content, string spriteName, string name, Scene owner) : base(content, spriteName, name, owner)
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

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }


        public void OnCollision(ICollidable other)
        {
            throw new NotImplementedException();
        }
    }
}
