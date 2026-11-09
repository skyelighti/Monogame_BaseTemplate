
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GameBase.Dialogue;
/// <summary>
/// NPC is a premade gameobject
/// instantaate  with game prefabs tho?
/// gameobject with hitbox, interact, and dialouge component
/// </summary>
public class NPC : GameObject
{
    public Character character = Character.NPC1;
    readonly List<DialogueInfo> dialogueInfoList;
    public HitboxComponent hitbox;
    public InteractableComponent interact;

    public DialougeComponent dialouge;


    public NPC(Sprite sprite, Scene owner, NPCInfo info) : base( sprite, owner)
    {
        //update sprites, animated sprites and then size in here
        character = info.character;
        dialogueInfoList = new();

        foreach (string id in info.dialogueIDOrder)
        {
            dialogueInfoList.Add(Services.GameDef.dialogueById[id]);
        }
        hitbox = new HitboxComponent(this);
        interact = new InteractableComponent(this);
        dialouge = new DialougeComponent(this);

        AddComponent(hitbox);
        AddComponent(interact);
        AddComponent(dialouge);

        Connect();
    }

    public void Connect()
    {
       
        hitbox = GetComponent<HitboxComponent>();
        hitbox.CollisionStay += OnCollision;
        hitbox.CollisionEnter += OnCollisionEnter;
        hitbox.CollisionExit += OnCollisionExit;

        interact = GetComponent<InteractableComponent>();
        interact.Interacted += OnInteract;

        dialouge = GetComponent<DialougeComponent>();
    }

    public void OnInteract(GameObject obj) {
        if (activeSelf && interact.CanInteract)
        {
            EventSystem.OnNPCInteract(character);

            int index = Services.GamePersistence.GetDialogueIndex(character);
            if (index >= 0 && index < dialogueInfoList.Count)
                dialouge.Play(dialogueInfoList[index]);
        }
    }

    public void OnCollision(ICollidable col)
    {
    }
    public void OnCollisionEnter(ICollidable col)
    {
    }
    public void OnCollisionExit(ICollidable col)
    {
    }
    public override void OnDestroy()
    {
        hitbox.CollisionStay -= OnCollision;
        hitbox.CollisionEnter -= OnCollisionEnter;
        hitbox.CollisionExit -= OnCollisionExit;
        interact.Interacted -= OnInteract;

        base.OnDestroy();
    }
}
