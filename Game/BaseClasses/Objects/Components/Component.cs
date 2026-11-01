using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBase;

public abstract class Component : IComponent, IObject
{
    public GameObject owner { get; protected set; }

    public bool activeSelf => enabled && owner.IsActive;
    protected bool enabled = true;
    protected Component(GameObject o)
    {
        owner = o;
    }

    public virtual void Update(GameTime gameTime)
    {
        
    }
    public virtual void SetActive(bool state) {
        enabled = state;
    }
    public virtual void OnDestroy() { 
        //all subscription cleanup belongs here. 
    }
}
