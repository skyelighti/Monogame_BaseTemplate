using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBase;

public class LifecycleComponent : Component, IUpdatable, IPersistent
{
    float lifecycle;
    public LifecycleComponent(float lifecycle, string instanceId, GameObject owner) : base(owner)
    {
        this.lifecycle = lifecycle;

    }

    public void CaptureState(GameObjectSaveData data)
    {
        data.RemainingLifetime = lifecycle;
    }

    public override void Connect()
    {
        
    }

    public override void OnDestroy()
    {
        owner.ownerScene.RemoveObject(owner);
    }

    public void RestoreState(GameObjectSaveData data)
    {
        lifecycle = (float)data.RemainingLifetime;
    }

    public void Update(GameTime gameTime)
    {
        if (!activeSelf) { return; }
        if (lifecycle >= 0)
        {
            lifecycle -= (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
        else {
            OnDestroy();
        }
    }

}
