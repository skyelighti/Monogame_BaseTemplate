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
    public string InstanceID { get; }

    public PickupItem(ItemInfo info, string instanceId, Scene owner) : base(info.spriteName, owner, info.name)
    { 
        Info = info;
        InstanceID = instanceId;
    }
    public override void OnInteract(GameObject player)
    {
        Services.PlayerInventory.AddItem(Info);
        interact.SetCanInteract(false);
        ActiveOnSceneEnter = false;
        SetActive(false);
        EventSystem.Pickup(InstanceID);
    }
    public override void OnCollisionEnter(ICollidable collidable)
    {
    }
    public override void OnCollisionExit(ICollidable collidable)
    {
    }
}
