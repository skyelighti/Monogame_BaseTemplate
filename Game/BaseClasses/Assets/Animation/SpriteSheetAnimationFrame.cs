using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBase.Animations;

public class SpriteSheetAnimationFrame : IAnimationFrame
{
    public int AtlasFrame { get;}

    public TimeSpan Duration { get;}
    public SpriteSheetAnimationFrame(int index, TimeSpan duration) {
        AtlasFrame = index;
        Duration = duration;
    
    }
}
