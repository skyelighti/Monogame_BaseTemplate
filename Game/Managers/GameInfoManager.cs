using BaseTemplate.Game.BaseClasses;
using BaseTemplate.Game.BaseClasses.Items;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseTemplate.Game.Managers
{
    public class GameInfoManager
    {
        public Inventory playerInventory { get; } = new();
        public GameInfoManager(ContentManager content)
        {
        }
        public void InitItemList()
        {
            //populate inventorylist items
        }
    }
}
