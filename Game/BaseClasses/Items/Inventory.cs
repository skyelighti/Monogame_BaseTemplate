using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBase.Items;

public class Inventory
{
    public List<ItemInfo> items { get; private set; } = new();
    public static Dictionary<string, ItemInfo> itemList = new();
    //stores item definitions
    //convert to dict later - add dupes?
    public Action OnInvUpdate;
    InventoryUI invUI;
    public Inventory()
    {
        invUI = new InventoryUI(this);
    }
    public void InventoryToggle(GameAction action)
    {
        if (action == GameAction.ToggleInventory)
        {
            if (Services.Input.inputActionMap == InputActionMap.Gameplay)
            {
                //open inventory
                invUI.SetActive(true);
                Services.Input.UpdateActionMap(InputActionMap.Inventory);
            }
            else if (Services.Input.inputActionMap == InputActionMap.Inventory)
            {
                //close inventory
                invUI.SetActive(false);
                Services.Input.UpdateActionMap(InputActionMap.Gameplay);
            }
        }
    }
    public void OnEnable()
    {
        Services.Input.ActionPressed += InventoryToggle;
    }
    public void AddAllItemInfo(List<ItemInfo> infos)
    {
        foreach (ItemInfo info in infos)
        {
            itemList[info.ID] = info;
        }
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
            OnInvUpdate?.Invoke();
        }
    }
    public void ResetInventory()
    {
        items.Clear();
        OnInvUpdate?.Invoke();
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
        OnInvUpdate?.Invoke();
    }
}
