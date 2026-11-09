using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBase;

public class PlayerMovementComponent : MovementComponent
{
    public PlayerMovementComponent(float speed, GameObject o) : base(speed, o)
    {
        Services.Input.ActionKeyDown += OnKeyDown;
        Services.Input.ActionReleased += OnKeyReleased;
    }


    public void OnKeyDown(GameAction action)
    {
        //i want this to happen evey update
        switch (action)
        {
            case (GameAction.MoveRight):
                right = true;
                break;
            case (GameAction.MoveLeft):
                left = true;
                break;
            case (GameAction.MoveUp):
                up = true;
                break;
            case (GameAction.MoveDown):
                down = true;
                break;
        }
    }

    public void OnKeyReleased(GameAction action)
    {
        switch (action)
        {
            case (GameAction.MoveRight):
                right = false;
                break;
            case (GameAction.MoveLeft):
                left = false;
                break;
            case (GameAction.MoveUp):
                up = false;
                break;
            case (GameAction.MoveDown):
                down = false;  
                break;
        }
    }

    public override void OnDestroy() {
        Services.Input.ActionKeyDown -= OnKeyDown;
        Services.Input.ActionReleased -= OnKeyReleased;
        //make this an override eventually. all components should implement ondestroy
    }

    public override void Connect()
    {
    }
}
