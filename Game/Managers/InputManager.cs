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

    /// <summary>
    /// Creates a new InputManager.
    /// </summary>
    public InputManager()
    {
        Keyboard = new KeyboardInfo();
        Mouse = new MouseInfo();
    }

    /// <summary>
    /// Updates the state information for the keyboard, mouse, and gamepad inputs.
    /// </summary>
    /// <param name="gameTime">A snapshot of the timing values for the current frame.</param>
    public void Update(GameTime gameTime)
    {
        Keyboard.Update();
        Mouse.Update();
        foreach (var binding in _actionKeys)
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
            }
            else if (!isPressed && wasPressed)
            {
                ActionReleased?.Invoke(binding.Key);
            }
            if (isPressed)
            {
                ActionKeyDown?.Invoke(binding.Key);
            }
        }
        //prevents double firing for input :P
    }

    public void UpdateKeyBindings(GameAction action, List<Keys> newKeys)
    {
        if (_actionKeys.ContainsKey(action))
        {
            _actionKeys[action] = newKeys;
        }
    }


}

