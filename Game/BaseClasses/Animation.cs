using System;
using System.Collections.Generic;

namespace BaseTemplate.Game.BaseClasses;

public class Animation
{
    public List<TextureRegion> Frames { get; set; }
    public TimeSpan Delay { get; set; }
    public Animation()
    {
        Frames = new List<TextureRegion>();
        Delay = TimeSpan.FromMilliseconds(100);
    }
    public Animation(List<TextureRegion> frames, int fps)
    {
        Frames = frames;
        Delay = TimeSpan.FromSeconds(1.0f / fps);
    }

}