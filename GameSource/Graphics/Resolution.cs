using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinuteBattle.Graphics
{
    class Resolution : IResolution
    {
        Vector2 IResolution.ScreenToGameCoord(Vector2 screenCoord)
        {
            return screenCoord;
        }
    }
}
