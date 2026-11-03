using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBase;

public class TimedPickupItem : PickupItem, IUpdatable, IPersistent
{
    float lifecycle;
    public TimedPickupItem(float lifecycle, ItemInfo info, string instanceId, GameObject owner) : base(owner, info, instanceId)
    {
        this.lifecycle = lifecycle;

    }

    public void CaptureState(GameObjectSaveData data)
    {
        data.RemainingLifetime = lifecycle;
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
            owner.ownerScene.RemoveObject(owner);
        }
    }

}
