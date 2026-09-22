using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public interface IInteractable
{
    public bool CanInteract { get; }
    public void Interact(GameObject player);
}
