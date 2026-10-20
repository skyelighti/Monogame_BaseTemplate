using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  GameBase.Animations;

public class SpriteSheet
{
    Dictionary<string, SpriteSheetAnimation> _animations = new();
    string Name;
    TextureAtlas textureAtlas;

    public SpriteSheet(string name, TextureAtlas atlas) {
        textureAtlas = atlas;
        Name = name;
    }
    public void AddAnimation(SpriteSheetAnimation anim) {
        _animations.Add(anim.name, anim);
    }
    public SpriteSheetAnimation GetAnimation(string name) {
        return _animations[name];
    }
    public Sprite CreateSprite(int regionIndex)
    {
        return new Sprite(textureAtlas.GetTextureRegion(regionIndex));
    }
    public TextureRegion GetTextureRegion(int regionIndex)
    {
        return textureAtlas.GetTextureRegion(regionIndex);
    }
}
