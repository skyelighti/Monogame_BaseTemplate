using GameBase.Items;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBase;

public class PickupItem : Item
{
    public PickupItem(ContentManager content, ItemInfo info, string instanceId, Scene owner)
        : base(content, info.spriteName, info.name, owner)
    { 
        Info = info;
        ID = instanceId;
    }
    public override void Interact(GameObject player)
    {
        if (CanInteract)
        {
            Services.GameInfo.playerInventory.AddItem(Info);
            CanInteract = false;
            ActiveOnSceneEnter = false;
            SetActive(false);
            EventSystem.Pickup(ID);
        }
    }
    public override void OnCollisionEnter(ICollidable collidable)
    {
    }
    public override void OnCollisionExit(ICollidable collidable)
    {
    }
}
