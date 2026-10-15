
using GameBase.Dialogue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBase;
/// <summary>
/// Event system, for events that should be able to be subscribed to from anywhere within the project
/// </summary>
public static partial class EventSystem
{
    //edit this as needed.
    public static event Action<string> OnInteract;
    public static event Action<string> OnPickup;
    public static event Action<DialogueInfo> OnDialogueComplete;
    public static event Action<Character> OnNPCInteract;
    //global events that the GameInfoManager can like subscribe to, so a reference isnt needed.
    public static void Interact(string ID) {
        OnInteract?.Invoke(ID);
    }
    public static void Pickup(string ID)
    {
        OnPickup?.Invoke(ID);
    }
    public static void DialogueComplete(DialogueInfo dialogueInfo)
    {
        OnDialogueComplete?.Invoke(dialogueInfo);
    }
    public static void NPCInteract(Character c) {
        OnNPCInteract?.Invoke(c);
    }
}
