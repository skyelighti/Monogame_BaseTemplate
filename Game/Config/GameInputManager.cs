using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBase.Input;

public partial class InputManager
{
    //everything here is going to change game to game. everything else in input manager should stay the same
    private readonly Dictionary<GameAction, List<Keys>> _actionKeys = new()
    {
        [GameAction.MoveUp] = new List<Keys> { Keys.W, Keys.Up },
        [GameAction.MoveDown] = new List<Keys> { Keys.S, Keys.Down },
        [GameAction.MoveLeft] = new List<Keys> { Keys.A, Keys.Left },
        [GameAction.MoveRight] = new List<Keys> { Keys.D, Keys.Right },
        [GameAction.Interact] = new List<Keys> { Keys.E },
        [GameAction.ToggleInventory] = new List<Keys> { Keys.I },
        [GameAction.Attack] = new List<Keys> { Keys.Space }
        //ALLOW FOR MOUSE CLICKING LATER
    };
    //contents can still change. dict itself cannot.

    private readonly Dictionary<GameAction, List<Keys>> _dialogueKeys = new()
    {
        [GameAction.ProgressDialogue] = new List<Keys> { Keys.Space, Keys.Enter },
        //ALLOW FOR MOUSE CLICKING LATER
    };
    private readonly Dictionary<GameAction, List<Keys>> _inventoryKeys = new()
    {
        [GameAction.ToggleInventory] = new List<Keys> { Keys.I, Keys.Escape }
    };

    readonly Dictionary<InputActionMap, Dictionary<GameAction, List<Keys>>> ActionMap = new();

    public void InitActionMaps() {
        ActionMap[InputActionMap.Gameplay] = _actionKeys;
        ActionMap[InputActionMap.Dialogue] = _dialogueKeys;
        ActionMap[InputActionMap.Inventory] = _inventoryKeys;
    }
}
