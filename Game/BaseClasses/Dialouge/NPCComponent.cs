
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GameBase.Dialogue;

public class NPCComponent : Component
{

    public Character character = Character.NPC1;
    readonly List<DialogueInfo> dialogueInfoList;
    public HitboxComponent hitbox;
    public InteractableComponent interact;


    public NPCComponent(GameObject owner, NPCInfo info) : base( owner)
    {
        //update sprites, animated sprites and then size in here
        character = info.character;
        dialogueInfoList = new();

        foreach (string id in info.dialogueIDOrder)
        {
            dialogueInfoList.Add(Services.GameDef.dialogueById[id]);
        }

    }

    public override void Connect()
    {
        base.Connect();
        hitbox = owner.GetComponent<HitboxComponent>();
        hitbox.CollisionStay += OnCollision;
        hitbox.CollisionEnter += OnCollisionEnter;
        hitbox.CollisionExit += OnCollisionExit;

        interact = owner.GetComponent<InteractableComponent>();
        interact.Interacted += OnInteract;
    }

    public void OnInteract(GameObject obj) {
        if (activeSelf && interact.CanInteract)
        {
            EventSystem.OnNPCInteract(character);
            int index = Services.GamePersistence.GetDialogueIndex(character);
            if (index < dialogueInfoList.Count)
            {
                Services.Dialogue.DialogueSetup(dialogueInfoList[index]);
            }
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
