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
    //still need to set when the interaction is valid though. 
    public BasicItem(string spriteName, Scene owner, string name = null) : base(spriteName, owner, name)
    { 
        //assign Info here
    }
    public override void OnInteract(GameObject player)
    {
        //checking for if an interact is valid is already handled by the component
    }
    public override void OnCollisionEnter(ICollidable collidable)
    {
    }
    public override void OnCollisionExit(ICollidable collidable)
    {
    }
}
