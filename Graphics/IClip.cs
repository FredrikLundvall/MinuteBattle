using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinuteBattle.Graphics
{
    public interface IClip
    {
        void Draw(Vector2 parentPosition, float parentRotation, GameTime gameTime);
        ClipCategoryEnum GetCategory();
        bool SetText(string text);
        bool SetRotation(float rotation);
    }
}
