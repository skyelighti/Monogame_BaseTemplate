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

    public PickupItem(Sprite sprite, Scene scene, ItemInfo info, string instanceId, bool rememberCollection = false) : base(sprite, scene, info, instanceId)
    {
        InstanceID = instanceId;
        RememberCollection = rememberCollection;

        hitbox.SetTrigger(true);
    }


    public override void OnCollisionEnter(ICollidable collidable)
    {
        if (collidable.layer != Layer.Player || !activeSelf) { return; }
        Pickup();
    }
    public override void OnCollisionExit(ICollidable collidable)
    {
    }

    public void Pickup() {
        Services.PlayerInventory.AddItem(Info);
        interact.SetCanInteract(false);
        ActiveOnSceneEnter = false;
        SetActive(false);
        EventSystem.OnPickup(Info, RememberCollection ? InstanceID : null);
    }
}
