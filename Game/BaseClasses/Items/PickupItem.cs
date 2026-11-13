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
    /// <summary>
    /// basic implementation of what an item that can be instantiated within the game world might look like?
    /// this does item interaction based upon walking into the item. for other items where you dont want to need to directly touch to interact,
    /// add another trigger hitbox collider
    /// </summary>

    public PickupItem(Sprite sprite, Scene scene, ItemInfo info) : base(sprite, scene, info)
    {
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
        string InstanceID = GetComponent<PersistenceComponent>()?.InstanceId;
        EventSystem.OnPickup(Info, InstanceID);
    }

}
