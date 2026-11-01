using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBase;

public class AnimationInfo
{
    //(string n, IReadOnlyList<SpriteSheetAnimationFrame> frames, bool isLooping, bool isReversed, bool isPingPong)
    public List<int> framesindx { get; set; } = new();
    public bool isLooping { get; set; }
    public bool isReversed { get; set; }
    public bool isPingPong { get; set; }

}