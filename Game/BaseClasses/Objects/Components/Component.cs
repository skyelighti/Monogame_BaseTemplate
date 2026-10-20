using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBase;

public abstract class Component : IComponent
{
    public GameObject owner { get; protected set; }

    public bool IsActive => enabled && owner.IsActive;
    protected bool enabled = true;
    protected Component(GameObject o)
    {
        owner = o;
    }

    public virtual void Update(GameTime gameTime)
    {
        
    }
    public virtual bool SetActive(bool state) {
        enabled = state;
        return IsActive;
    }
}
