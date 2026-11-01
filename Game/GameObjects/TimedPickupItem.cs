using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBase;

public class TimedPickupItem : PickupItem
{
    float lifecycle;
    public TimedPickupItem(float lifecycle, ItemInfo info, string instanceId, Scene owner) : base(info, instanceId, owner)
    {
        this.lifecycle = lifecycle;

    }

    public override void Update(GameTime gameTime)
    {
        if (!activeSelf) { return; }
        if (lifecycle >= 0)
        {
            lifecycle -= (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
        else {
            ownerScene.RemoveObject(this);
        }
        base.Update(gameTime);
    }
}
