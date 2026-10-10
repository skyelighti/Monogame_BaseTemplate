using Microsoft.Xna.Framework.Content;
using RenderingLibrary.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameBase;

namespace GameBase.Assets;
public class SpriteLibrary
{
    private Dictionary<string, TextureRegion> _regions;
    private Dictionary<string, Animation> _animations;
    private ContentLoader _contentLoader;
    public SpriteLibrary(ContentManager content)
    {
        _animations = new Dictionary<string, Animation>();
        _regions = new Dictionary<string, TextureRegion>();
        _contentLoader = new ContentLoader(content, this);
    }
    public void AddRegion(string name, TextureRegion region)
    {
        _regions.Add(name, region);
    }
    public TextureRegion GetRegion(string name)
    {
        return _regions[name];
    }
    public bool RemoveRegion(string name)
    {
        return _regions.Remove(name);
    }
    public void AddAnimation(string animationName, Animation animation)
    {
        _animations.Add(animationName, animation);
    }
    public Animation GetAnimation(string animationName)
    {
        return _animations[animationName];
    }
    public bool RemoveAnimation(string animationName)
    {
        return _animations.Remove(animationName);
    }
    /// <summary>
    /// Creates a new animated sprite using the animation from this texture atlas with the specified name.
    /// </summary>
    /// <param name="animationName">The name of the animation to use.</param>
    /// <returns>A new AnimatedSprite using the animation with the specified name.</returns>
    public AnimatedSprite CreateAnimatedSprite(string animationName)
    {
        Animation animation = GetAnimation(animationName);
        return new AnimatedSprite(animation);
    }
    public Sprite CreateSprite(string regionName)
    {
        TextureRegion region = GetRegion(regionName);
        return new Sprite(region);
    }
    public void AddAtlas(string atlasName, TextureAtlas atlas)
    {
        foreach (var entry in atlas.Regions)
        {
            string spriteName = $"{atlasName}.{entry.Key}";
            AddRegion(spriteName, entry.Value);
        }
    }
}
