using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace GameBase.Animations;

public static class SpriteSheetAnimationBuilder
{

    public static void FromFile(ContentManager content, string filename, SpriteLibrary sprites)
    {
        string filePath = Path.Combine(content.RootDirectory, filename);

        using (Stream stream = TitleContainer.OpenStream(filePath))
        {

            using (XmlReader reader = XmlReader.Create(stream))
            {
                XDocument doc = XDocument.Load(reader);
                XElement root = doc.Root;
                //i need to get the player atlas, get a spritesheet name,
                foreach (XElement spriteSheet in root.Elements("SpriteSheet"))
                {
                    TextureAtlas atlas = TextureAtlas.FromFile(content, (string)spriteSheet.Element("AtlasFile"));
                    string sheetName = (string)spriteSheet.Element("Name");
                    SpriteSheet sheet = new SpriteSheet(sheetName, atlas);
                    
                    foreach (XElement animation in spriteSheet.Element("Animations").Elements("Animation")) {
                        List<SpriteSheetAnimationFrame> animationFrames = new();
                        string animationName = (string)animation.Attribute("name");
                        string frameList = (string)animation.Attribute("frames");
                        string[] frameNames = frameList.Split(" ");
                        double duration = (double)animation.Attribute("duration");
                        bool isLooping = (bool)animation.Attribute("looping");
                        bool isPingPong = (bool)animation.Attribute("pingpong");
                        bool isReverse = (bool)animation.Attribute("reverse");
                        foreach (string frame in frameNames) {
                            int index = atlas.GetIndexOfRegion(frame);
                            animationFrames.Add(new SpriteSheetAnimationFrame(index, TimeSpan.FromSeconds(duration)));
                        }
                        SpriteSheetAnimation anim = new SpriteSheetAnimation(animationName, animationFrames,isLooping, isReverse, isPingPong);
                        sheet.AddAnimation(anim);
                    }
                    sprites.AddSpriteSheet(sheetName, sheet);
                }
            }
        }
    }
}
