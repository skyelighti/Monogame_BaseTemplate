using GameBase.Assets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using SharpCompress.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using ToolsUtilities;


namespace GameBase.Items;

public class ItemInfo
{

    public ItemType itemType;
    public string name { get; private set; }

    //public string desc { get; private set; }
    public Sprite sprite { get; private set; }
    public string ID { get; private set; }
    public string spriteName { get; }
    public ItemInfo(ItemType type, string n, Sprite s, string id, string spriteName = null)
    {
        itemType = type;
        name = n;
        //desc = d;
        sprite = s;
        ID = id;
        this.spriteName = spriteName;
    }

    public void SetSprite(Sprite sprite)
    {
        if (sprite != null)
            this.sprite = sprite;
    }
    public void SetName(string n)
    {
        if (n != null)
            name = n;
    }

    //public void SetDesc(string desc)
    //{
    //    if (desc != null)
    //        this.desc = desc;
    //}
    public void SetItemType(ItemType itemType)
    {
        this.itemType = itemType;

    }
    // you should be able to load iteminfo from files i think? 

    public static List<ItemInfo> FromFile(ContentManager content, string filename)
    {
        List<ItemInfo> itemDefs = new();
        string filePath = Path.Combine(content.RootDirectory, filename);
        using (Stream stream = TitleContainer.OpenStream(filePath))
        {
            XDocument document = XDocument.Load(stream);
            XElement root = document.Root;
            foreach (XElement itemElement in root.Elements("Item"))
            {
                string name = (string)itemElement.Element("Name");
                string type = (string)itemElement.Element("Type");
                string sprite = (string)itemElement.Element("Sprite");
                string id = (string)itemElement.Element("ID");

                ItemType t = (ItemType)Enum.Parse(typeof(ItemType), type);
                Sprite s = Services.Sprites.CreateSprite(sprite);
                var x = new ItemInfo(t, name, s, id, sprite);
                itemDefs.Add(x);
            }
        }

        return itemDefs;
    }
}
