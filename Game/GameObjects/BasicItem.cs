using GameBase.Items;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBase;

public class BasicItem : Item
{
    public BasicItem(ContentManager content, string spriteName, string name, Scene owner) : base(content, spriteName, name, owner)
    { 
        //assign Info here
    }
    public override void Interact(GameObject player)
    {
        if (CanInteract)
        {
            //interact here.
        }
    }
    public override void OnCollisionEnter(ICollidable collidable)
    {
    }
    public override void OnCollisionExit(ICollidable collidable)
    {
    }
}
