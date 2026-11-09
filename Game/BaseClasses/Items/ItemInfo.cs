using GameBase.Assets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using SharpCompress.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    public Sprite sprite { get; private set; }
    public string ID { get; private set; }
    public string spriteName { get; }
    public List<IItemProperty> ItemProperties { get; private set; } = new();
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


    public void AddProperty(IItemProperty p)
    {
        if (!ItemProperties.Contains(p))
        {
            ItemProperties.Add(p);
        }
        else
        {
            Debug.WriteLine("component owner is not correct, or already added.");
        }
    }
    public T GetProperty<T>() where T : class
    {
        foreach (IItemProperty property in ItemProperties)
        {
            if (property is T match)
            {
                return match;
            }
        }

        return null;
    }
    public List<T> GetProperties<T>() where T : class
    {
        List<T> p = new();
        foreach (IItemProperty property in ItemProperties)
        {
            if (property is T match)
            {
                p.Add(match);
            }
        }

        return p;
    }

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
                XElement icon = itemElement.Element("Icon");
                string sprite = (string)itemElement.Element("Sprite");
                string id = (string)itemElement.Element("ID");

                ItemType t = (ItemType)Enum.Parse(typeof(ItemType), type);
                Sprite s;

                if (sprite != null)
                {
                    s = Services.Sprites.CreateSprite(sprite);
                }
                else
                {
                    string sheetName = (string)icon.Attribute("sheet");
                    int frame = (int)icon.Attribute("frame");

                    s = Services.Sprites.GetSpriteSheet(sheetName).CreateSprite(frame);
                }
                var x = new ItemInfo(t, name, s, id, sprite);

                XElement propertiesElement = itemElement.Element("Properties");


                //this adds any additional properties to the iteminfo that gets loaded in
                if (propertiesElement != null)
                {
                    XElement descElement = propertiesElement.Element("Description");

                    if (descElement != null)
                    {
                        string desc = (string)descElement.Element("description");
                        List<string> idList = new();
                        if (descElement.Element("DialogueIDs") is XElement dialogueIDs)
                        {
                            foreach (XElement ID in dialogueIDs.Elements("ID"))
                            {
                                idList.Add(((string)ID).Trim());
                            }
                        }
                        x.AddProperty(new DescriptionProperty(idList, desc));
                    }

                    XElement currencyElement = propertiesElement.Element("Currency");

                    if (currencyElement != null)
                    {
                        int amount = (int)currencyElement.Attribute("amount");
                        x.AddProperty(new CurrencyInfo(amount));
                    }

                    XElement weaponElement = propertiesElement.Element("Weapon");

                    if (weaponElement != null)
                    {
                        int amount = (int)weaponElement.Attribute("amount");
                        x.AddProperty(new WeaponInfo(amount));
                    }
                }

                itemDefs.Add(x);
            }
        }

        return itemDefs;
    }
}
