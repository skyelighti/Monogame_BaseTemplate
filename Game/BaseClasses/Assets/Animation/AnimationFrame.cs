using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBase.Animations;

public class SpriteSheetAnimationFrame : IAnimationFrame
{
    public TextureRegion Region { get; }

    public TimeSpan Duration { get;}
    public SpriteSheetAnimationFrame(TextureRegion region, TimeSpan duration) {
        Region = region;
        Duration = duration;
    
    }
}
