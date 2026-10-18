using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameBase.Dialogue;
using GameBase.Items;

namespace GameBase;

public class GameProgressionManager
{
    public GameProgressionManager() {
        EventSystem.OnDialogueComplete += DialogueComplete;
        EventSystem.OnInteract += HandleInteract;
        EventSystem.OnPickup += HandlePickup;
        EventSystem.OnNPCInteract += HandleNPCInteract;
    }

    public void DialogueComplete(DialogueInfo dInfo) { 
        //i can parse for the specific dialouge ID and character here, and do case specific handling? 
        //this might get really really freaking bloated 
    }
    public void HandleInteract(string ID)
    {
    }

    public void HandlePickup(string ID) { 
    
    }
    public void HandleNPCInteract(Character character) { 
    
    }
}
