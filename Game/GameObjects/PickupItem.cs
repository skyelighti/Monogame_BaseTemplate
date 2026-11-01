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

    public PickupItem(ItemInfo info, string instanceId, Scene owner) : base(new Sprite(info.sprite.Region), owner, info.name)
    { 
        Info = info;
        InstanceID = instanceId;
        hitbox.SetTrigger(true);
    }

    public override void OnCollisionEnter(ICollidable collidable)
    {
        if (collidable.layer != Layer.Player || !activeSelf) { return; }
        Services.PlayerInventory.AddItem(Info);
        interact.SetCanInteract(false);
        ActiveOnSceneEnter = false;
        SetActive(false);
        EventSystem.Pickup(Info, InstanceID);
    }
    public override void OnCollisionExit(ICollidable collidable)
    {
    }
}
