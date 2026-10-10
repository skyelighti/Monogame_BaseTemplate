using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameBase.Assets;


namespace GameBase.Items;

public enum ItemType
{
    //useful for catagorizing item typing?
    //not sure if redundant, but the core idea is maybe stacking items
}
public class ItemInfo
{

    public ItemType itemType;
    public string name { get; private set; }

    public string desc { get; private set; }
    public Sprite sprite { get; private set; }
    public ItemInfo(ItemType type, string n, string d, Sprite s)
    {
        itemType = type;
        name = n;
        desc = d;
        sprite = s;
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

    public void SetDesc(string desc)
    {
        if (desc != null)
            this.desc = desc;
    }
    public void SetItemType(ItemType itemType)
    {
        this.itemType = itemType;

    }

}
