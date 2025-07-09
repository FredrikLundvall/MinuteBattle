using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinuteBattle.Graphics
{
    public interface IResolution
    {
        Vector2 ScreenToGameCoord(Vector2 screenCoord);
    }
}
