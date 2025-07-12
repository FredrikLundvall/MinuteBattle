using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MinuteBattle.Graphics
{
    public static class FontDictionary
    {
        internal static Dictionary<FontEnum, SpriteFont> _fontList = new Dictionary<FontEnum, SpriteFont>();
        static FontDictionary()
        {
            Add(FontEnum.Empty, null);
        }
        public static void Add(FontEnum index, SpriteFont font)
        {
            _fontList[index] = font;
        }
        public static SpriteFont Get(FontEnum index)
        {
            return _fontList[index];
        }
    }
}
