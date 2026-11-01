using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBase;

public class InteractableComponent : Component, IInteractable
{

    public bool CanInteract { get; private set; } = true;

    public string ID => owner.name;
    public event Action<GameObject> Interacted;

    public InteractableComponent(GameObject o) : base(o)
    {
    }

    public void Interact(GameObject player)
    {
        if (!activeSelf || !CanInteract)
        {
            return;
        }

        Interacted?.Invoke(player);
    }
    public void SetCanInteract(bool state)
    {
        CanInteract = state;
    }
}
