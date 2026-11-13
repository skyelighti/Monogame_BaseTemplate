using GameBase.Dialogue;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBase;

public static partial class Prefabs
{
    /// <summary>
    /// defaults for when creating gameobjects. feel free to modify based on need/game settings
    /// these apply mainly to the base prefab class
    /// </summary>
    public static int defaultSize = 16;
    public static int defaultrotation = 0;
    public static Layer defaultLayer = Layer.Default;

    /// <summary>
    /// create bullet - mainly for use with object pooling as the factory creationn method
    /// attaches the bullet component to a factory solid, changes layer to enemy atk
    /// </summary>
    /// <param name="owner"></param>
    /// <param name="sprite"></param>
    /// <param name="position"></param>
    /// <returns></returns>
    public static GameObject CreateBullet(Scene owner, Sprite sprite, Vector2 position) { 
        GameObject bullet = CreateSolid(owner, sprite, position);
        bullet.UpdateLayer(Layer.EnemyAtk);
        //to be updated to use a bullet component instead of a hitbo
        bullet.AddComponent(new Bullet(bullet));

        return bullet;  
    }
    /// <summary>
    /// creates a pickup item
    /// uses base interactable prefab, adds a pickup item component
    /// modify as needed, ex. if pickup happens on collision
    /// </summary>
    /// <param name="owner">scene owner of this gameobject</param>
    /// <param name="position">location of this object in the world</param>
    /// <param name="info">item info, supplies the item's properties</param>
    /// <param name="instanceId">unique identifier for this instance</param>
    /// <param name="sprite">optional sprite for the item. if we dont pass a sprite, it uses the item info's texture region/icon</param>
    /// <returns></returns>
    public static GameObject CreatePickupItem(Scene owner, Vector2 position, ItemInfo info, Sprite sprite = null)
    {
        sprite ??= new Sprite(info.sprite.Region);
        GameObject item = new PickupItem(sprite,owner,info);

        item.UpdateLocation(position);
        item.UpdateSize(defaultSize, defaultSize);
        item.UpdateRotation(defaultrotation);
        item.UpdateLayer(Layer.Interactable);
        item.AddTag(Tag.Item);

        return item;
    }

    /// <summary>
    /// creates a timed pickup, item will despawn after a certain amount of time- controlled by the timed pickup item component
    /// uses interactable prefab as base, adds a timed pickup item component
    /// </summary>
    /// <param name="owner">scene owner of this gameobject</param>
    /// <param name="position">location of this object in the world</param>
    /// <param name="info">item info, supplies the item's properties</param>
    /// <param name="instanceId">unique identifier for this instance</param>
    /// <param name="lifecycle">duration for which the item will exist</param>
    /// <param name="sprite">optional sprite for the item. if we dont pass a sprite, it uses the item info's texture region/icon</param>
    /// <returns></returns>
    public static GameObject CreateTimedPickupItem(Scene owner, Vector2 position, ItemInfo info, float lifecycle, bool pooled, Sprite sprite = null)
    {
        sprite ??= new Sprite(info.sprite.Region);
        GameObject item = CreatePickupItem(owner, position, info);
        item.AddComponent(new LifecycleComponent(lifecycle, item, pooled));
        return item;
    }

    /// <summary>
    /// creates an npc. ummm located in game prefabs for ez modification
    /// uses interactable prefab as base
    /// </summary>
    /// <param name="owner">scene owner of this gameobject</param>
    /// <param name="sprite">optional sprite for the npc</param>
    /// <param name="position">location of this object in the world</param>
    /// <param name="info">npc info, supplies the npc's properties</param>
    /// <returns></returns>
    public static GameObject CreateNPC(Scene owner, Sprite sprite, Vector2 position, NPCInfo info)
    {
        GameObject npc = new NPC(sprite, owner, info);
        npc.UpdateLocation(position);
        npc.UpdateSize(defaultSize, defaultSize);
        npc.UpdateLayer(Layer.Interactable);
        npc.AddTag(Tag.NPC);
        return npc;
    }

}
