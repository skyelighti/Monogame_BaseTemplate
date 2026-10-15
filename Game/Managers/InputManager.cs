using BaseTemplate.Game.Config;
using GameBase.Dialogue;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace GameBase.Input;

public class InputManager
{
    /// <summary>
    /// Gets the state information of keyboard input.
    /// </summary>
    public KeyboardInfo Keyboard { get; private set; }

    /// <summary>
    /// Gets the state information of mouse input.
    /// </summary>
    public MouseInfo Mouse { get; private set; }
    public event Action<GameAction> ActionPressed;
    public event Action<GameAction> ActionReleased;
    public event Action<GameAction> ActionKeyDown;
    public InputActionMap inputActionMap { get; private set; } = InputActionMap.Gameplay;
    public event Action<InputActionMap> OnInputMapChange;

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

    /// <summary>
    /// Creates a new InputManager.
    /// </summary>
    public InputManager()
    {
        Keyboard = new KeyboardInfo();
        Mouse = new MouseInfo();
        ActionMap[InputActionMap.Gameplay] = _actionKeys;
        ActionMap[InputActionMap.Dialogue] = _dialogueKeys;
        ActionMap[InputActionMap.Inventory] = _inventoryKeys;

    }

    public void UpdateActionMap(InputActionMap map)
    {
        inputActionMap = map;
        OnInputMapChange?.Invoke(map);
    }
    /// <summary>
    /// Updates the state information for the keyboard, mouse, and gamepad inputs.
    /// </summary>
    /// <param name="gameTime">A snapshot of the timing values for the current frame.</param>
    public void Update(GameTime gameTime)
    {
        Keyboard.Update();
        Mouse.Update();
        var startmap = inputActionMap;
        foreach (var binding in ActionMap[startmap])
        {
            bool wasPressed = false;
            bool isPressed = false;
            foreach (var key in binding.Value)
            {
                if (Keyboard.PreviousState.IsKeyDown(key))
                {
                    wasPressed = true;
                }
                if (Keyboard.CurrentState.IsKeyDown(key))
                {
                    isPressed = true;
                }
            }
            if (isPressed && !wasPressed)
            {
                ActionPressed?.Invoke(binding.Key);
                if (inputActionMap != startmap)
                {
                    return;
                }
            }
            else if (!isPressed && wasPressed)
            {
                ActionReleased?.Invoke(binding.Key);
                if (inputActionMap != startmap)
                {
                    return;
                }
            }
            if (isPressed)
            {
                ActionKeyDown?.Invoke(binding.Key);
                if (inputActionMap != startmap)
                {
                    return;
                }
            }
        }
        //prevents double firing for input :P
    }

    public void UpdateKeyBindings(InputActionMap map, GameAction action, List<Keys> newKeys)
    {
        if (ActionMap[map].ContainsKey(action))
        {
            ActionMap[map][action] = new List<Keys>(newKeys);
        }
    }


}

