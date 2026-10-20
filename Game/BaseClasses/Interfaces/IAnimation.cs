using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBase.Animations;

public interface IAnimation
{
    string name { get; }
    IReadOnlyList<IAnimationFrame> Frames { get; }
    int FrameCount { get; }
    bool IsLooping { get; }
    bool IsReversed { get; }
    bool IsPingPong { get; }
}
