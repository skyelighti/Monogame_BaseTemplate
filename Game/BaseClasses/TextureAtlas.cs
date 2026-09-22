using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using RenderingLibrary.Graphics;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Linq;


namespace MonoGameLibrary.Graphics;

public class TextureAtlas
{
    private Dictionary<string, TextureRegion> _regions;

    public Texture2D Texture { get; set; }
    private Dictionary<string, Animation> _animations;
    bool[] AlphaMask;

    public TextureAtlas()
    {
        _regions = new Dictionary<string, TextureRegion>();
        _animations = new Dictionary<string, Animation>();
    }

    public TextureAtlas(Texture2D texture)
    {
        Texture = texture;
        _regions = new Dictionary<string, TextureRegion>();
        _animations = new Dictionary<string, Animation>();
        CheckAlpha();
    }

    public void AddRegion(string name, int x, int y, int width, int height)
    {
        TextureRegion region = new TextureRegion(Texture, x, y, width, height);
        region.alphaMask = GetAlphaMask(x, y, width, height);
        _regions.Add(name, region);
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
    public TextureRegion GetRegion(string name)
    {
        return _regions[name];
    }
    public bool RemoveRegion(string name)
    {
        return _regions.Remove(name);
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

    public void Clear()
    {
        _regions.Clear();
    }
    public bool[] GetAlphaMask(int x, int y, int width, int height)
    {
        bool[] AlphaArea = new bool[width * height];
        Rectangle rect = new Rectangle(x, y, width, height);
        for (int i = 0; i < rect.Height; i++)
        {
            for (int j = 0; j < rect.Width; j++)
            {
                int sheetX = rect.X + j;
                int sheetY = rect.Y + i;

                int sheetIdx = sheetX + (sheetY * Texture.Width);
                int resultIdx = j + (i * rect.Width);

                AlphaArea[resultIdx] = AlphaMask[sheetIdx];
            }
        }
        return AlphaArea;

    }

    public void CheckAlpha()
    {
        Color[] pixels = new Color[Texture.Width * Texture.Height];
        AlphaMask = new bool[Texture.Width * Texture.Height];
        //get data takes a 1d array
        Texture.GetData<Color>(pixels);
        for (int i = 0; i < pixels.Length; i++)
        {
            if (pixels[i].A == 0)
            {
                AlphaMask[i] = false;
                //is transparent
            }
            else
            {
                AlphaMask[i] = true;
            }
        }
    }
    public Sprite CreateSprite(string regionName)
    {
        TextureRegion region = GetRegion(regionName);
        return new Sprite(region);
    }

}
