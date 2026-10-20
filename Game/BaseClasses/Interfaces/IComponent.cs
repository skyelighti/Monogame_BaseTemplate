using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBase;

public interface IComponent
{
    GameObject owner { get; }
    bool IsActive { get; }
    void Update(GameTime gameTime);
}
