using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseTemplate.Game.BaseClasses.Interfaces
{
    public interface IPixelCollidable : ICollidable
    {
        //sprite becomes neccesary if we are testing for pixel perfect collision
        Sprite sprite { get; }

    }


}