using GameBase.Dialogue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBase.Dialogue;
/// <summary>
/// this should play for both items and NPCs, ex, if the player inspects something within the game world
/// </summary>
public class DialougeComponent: Component
{
    public DialougeComponent(GameObject o) : base(o)
    {
    }

    public override void Connect()
    {
        
    }
    public void Play(DialogueInfo info)
    {
        if (!activeSelf || info == null) return;
        Services.Dialogue.DialogueSetup(info);
    }

    public override void OnDestroy()
    {
        
    }

}
