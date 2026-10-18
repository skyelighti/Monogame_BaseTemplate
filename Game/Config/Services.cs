using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameBase.Input;
using GameBase.Assets;
using GameBase.Dialogue;

namespace GameBase;

public static class Services
{
    //modify as needed
    //holds all the classes for services. 
    public static InputManager Input { get; private set; }
    public static SpriteLibrary Sprites { get; private set; }
    public static PersistenceManager GamePersistence { get; private set; }
    public static DialogueManager Dialogue { get; private set; }
    public static Inventory PlayerInventory { get; private set; }
    public static GameDefinitions GameDef { get; private set; }

    public static void Initalize( SpriteLibrary sprites, GameDefinitions gameD, bool enableItems, bool enableDialogue)
    {
        Input = new InputManager();
        Sprites = sprites;
        GamePersistence = new PersistenceManager();
        GameDef = gameD;
        if (enableItems) {
            PlayerInventory = new Inventory();
            gameD.InitItemList();
            PlayerInventory.OnEnable();
        }
        if (enableDialogue) {
            Dialogue = new DialogueManager(Input);
            gameD.InitDialogueLines();
            gameD.InitNPCDefinitions();
        }

    }

}
