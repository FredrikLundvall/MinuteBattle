using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinuteBattle.Graphics
{
    public class FontAnimation
    {
        public static FontAnimation EmptyAnimation = new FontAnimation(Vector2.Zero, FontEnum.Empty);
        public Vector2 _origin = Vector2.Zero;
        public FontEnum _fontId = FontEnum.Empty;
        public FontAnimation(Vector2 origin, FontEnum fontId)
        {
            _origin = origin;
            _fontId = fontId;
        }
        public SpriteFont getFont()
        {
            return FontDictionary.Get(_fontId);
        }
    }
}
