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
    public static GameInfoManager GameInfo { get; private set; }
    public static DialogueManager Dialogue { get; private set; }

    public static void Initalize(InputManager input, SpriteLibrary sprites, GameInfoManager gameI, DialogueManager dManager)
    {
        Input = input;
        Sprites = sprites;
        GameInfo = gameI;
        Dialogue = dManager;
    }

}
