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
    public bool RememberCollection { get; }

    public PickupItem(GameObject owner, ItemInfo info, string instanceId, bool rememberCollection = false) : base(owner, info)
    {
        InstanceID = instanceId;
        RememberCollection = rememberCollection;
    }

    public override void Connect()
    {
        base.Connect();
        hitbox.SetTrigger(true);
    }

    public override void OnCollisionEnter(ICollidable collidable)
    {
        if (collidable.layer != Layer.Player || !activeSelf) { return; }
        Services.PlayerInventory.AddItem(Info);
        interact.SetCanInteract(false);
        owner.ActiveOnSceneEnter = false;
        owner.SetActive(false);
        EventSystem.Pickup(Info,RememberCollection ? InstanceID : null);
    }
    public override void OnCollisionExit(ICollidable collidable)
    {
    }
}
