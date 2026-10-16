using GameBase.Assets;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBase;

/// <summary>
/// Loads all outside content
/// </summary>
public partial class ContentLoader
{
    //modify as needed
    public ContentLoader(ContentManager content, SpriteLibrary sprites)
    {
        //initalize all textures here. 
        Init(content, sprites);

    }
    public void Init(ContentManager content, SpriteLibrary sprites) {
        //initalize any constants like maybe a placeholder texture?
        GameSpecificInit(content, sprites);
    }
    partial void GameSpecificInit(ContentManager content, SpriteLibrary sprites);
}
