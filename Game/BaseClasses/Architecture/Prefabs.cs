using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameBase.Dialogue;

namespace GameBase;

public static partial class Prefabs
{
    public static GameObject CreateGameObject(Scene owner, Sprite sprite, Layer layer, Vector2 position, Vector2 size, float rotation)
    {
        GameObject obj = new GameObject(sprite, owner);
        obj.UpdateLayer(layer);
        obj.UpdateSprite(sprite);
        obj.UpdateLocation(position);
        obj.UpdateRotation(rotation);
        obj.UpdateSize((int)size.X, (int)size.Y);
        return obj;
    }
    public static GameObject CreateGameObject(Scene owner, string spritename, Layer layer, Vector2 position, Vector2 size)
    {
        return CreateGameObject(owner, Services.Sprites.CreateSprite(spritename), layer, position, size, 0f);
    }

    public static GameObject CreateGameObject(Scene owner, string spritename, Layer layer, Vector2 position)
    {
        return CreateGameObject(owner, Services.Sprites.CreateSprite(spritename), layer, position, new Vector2(16,16), 0f);
    }

    public static GameObject CreateGameObject(Scene owner, Sprite sprite, Layer layer, Vector2 position, Vector2 size)
    {
        return CreateGameObject(owner, sprite, layer, position, size, 0f);
    }

    public static GameObject CreateGameObject(Scene owner, Sprite sprite, Layer layer, Vector2 position)
    {
        return CreateGameObject(owner, sprite, layer, position, new Vector2(16, 16), 0f);
    }


    static GameObject CreateInteractable(Scene owner, Sprite sprite, Vector2 position) { 
        GameObject item = CreateGameObject(owner, sprite, Layer.Interactable, position);
        item.AddComponent(new HitboxComponent(item));
        item.AddComponent(new InteractableComponent(item));
        return item;

    }

    public static GameObject CreatePickupItem(Scene owner, Vector2 position, ItemInfo info, string instanceId, Sprite sprite = null)
    {
        sprite ??= new Sprite(info.sprite.Region);
        GameObject item = CreateInteractable(owner, sprite, position);
        item.AddComponent(new PickupItem(item, info, instanceId));
        return item;
    }

    public static GameObject CreateTimedPickupItem(Scene owner, Vector2 position, ItemInfo info, string instanceId, float lifecycle, Sprite sprite = null)
    {
        sprite ??= new Sprite(info.sprite.Region);
        GameObject item = CreateInteractable(owner, sprite, position);
        item.AddComponent(new TimedPickupItem(lifecycle, info, instanceId, item));
        return item;
    }

    public static GameObject CreateNPC(Scene owner, Sprite sprite, Vector2 position, NPCInfo info) { 
        GameObject npc = CreateInteractable(owner, sprite, position);
        npc.AddComponent(new NPCComponent(npc, info));
        return npc;
    }

    public static GameObject CreateSolid(Scene owner, Sprite sprite, Vector2 position, bool IsStatic = false) { 
        GameObject gameObject = CreateGameObject(owner, sprite, Layer.Default, position);
        gameObject.AddComponent(new HitboxComponent(gameObject, IsStatic));
        return gameObject;
    }
}