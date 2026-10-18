
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GameBase.Dialogue;

public class NPC : GameObject, ICollidable, IInteractable
{
    //other interfaces like iinteractable can also be implemented here

    public Rectangle BoxCollider => new Rectangle((int)transform.position.X, (int)transform.position.Y, (int)transform.scale.X, (int)transform.scale.Y);

    public bool IsTrigger { get; protected set; } = false;

    public bool ColliderIsActive { get; protected set; } = true;

    Layer ICollidable.layer { get; } = Layer.Interactable;

    public bool CanInteract { get; protected set; } = true;
    public string ID => name;

    public Character character = Character.NPC1;
    readonly List<DialogueInfo> dialogueInfoList;


    public NPC(string spriteName, Scene owner, NPCInfo info, string name = null) : base(spriteName, owner, name)
    {
        //update sprites, animated sprites and then size in here
        character = info.character;
        dialogueInfoList = new();

        foreach (string id in info.dialogueIDOrder)
        {
            dialogueInfoList.Add(Services.GameDef.dialogueById[id]);
        }

    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }


    public void OnCollision(ICollidable other)
    {

    }

    public void OnCollisionEnter(ICollidable other)
    {
    }

    public void OnCollisionExit(ICollidable other)
    {
    }

    public void Interact(GameObject player)
    {
        if (CanInteract)
        {
            EventSystem.NPCInteract(character);
            EventSystem.Interact(ID);
            int index = Services.GamePersistence.GetDialogueIndex(character);
            if (index < dialogueInfoList.Count)
            {
                Services.Dialogue.DialogueSetup(dialogueInfoList[index]);
            }
        }
    }

}
