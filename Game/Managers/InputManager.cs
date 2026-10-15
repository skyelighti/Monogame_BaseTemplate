using GameBase.Dialogue;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace GameBase.Input;

public partial class InputManager
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

      /// <summary>
    /// Creates a new InputManager.
    /// </summary>
    public InputManager()
    {
        Keyboard = new KeyboardInfo();
        Mouse = new MouseInfo();
        InitActionMaps();
        //partial class - is in gamme

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

