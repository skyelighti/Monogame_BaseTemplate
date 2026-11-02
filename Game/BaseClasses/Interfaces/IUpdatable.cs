using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBase
{
    public interface IUpdatable
    {
        public void Update(GameTime gameTime);
    }
}
