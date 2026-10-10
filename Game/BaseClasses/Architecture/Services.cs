using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameBase.Input;
using GameBase.Assets;

namespace GameBase;

public static class Services
{
    //holds all the classes for services. 
    public static InputManager Input { get; private set; }
    public static SpriteLibrary Sprites { get; private set; }
    public static GameInfoManager GameInfo { get; private set; }

    public static void Initalize(InputManager input, SpriteLibrary sprites, GameInfoManager gameI)
    {
        Input = input;
        Sprites = sprites;
        GameInfo = gameI;
    }

}
