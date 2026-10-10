using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBase.Items;

public class Inventory
{
    List<ItemInfo> items = new();
    public static Dictionary<string, ItemInfo> itemList = new();
    //stores item definitions
    //convert to dict later - add dupes?
    public Action<ItemInfo> OnItemAdded;
    public Inventory()
    {

    }
    public ItemInfo GetItem(ItemInfo info)
    {
        return items.Contains(info) ? info : null;
    }
    public ItemInfo GetItemFromDict(string info)
    {
        return itemList[info];
    }
    public void AddItem(ItemInfo info)
    {
        if (!items.Contains(info))
        {
            items.Add(info);
            OnItemAdded?.Invoke(info);
        }
    }
    public void ResetInventory()
    {
        items.Clear();
    }
    public ItemInfo GetItem(string name)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].name == name)
            {
                return items[i];
            }
        }
        return null;
    }
    public void RemoveItem(ItemInfo info)
    {
        items.Remove(info);
    }
}
