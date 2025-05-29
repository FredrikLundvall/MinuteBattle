using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinuteBattle.Graphics
{
    public static class TextureDictionary
    {
        internal static Dictionary<TextureEnum, Texture2D> _textureList = new Dictionary<TextureEnum, Texture2D>();
        static TextureDictionary()
        {
            Add(TextureEnum.Empty, null);
        }
        public static void Add(TextureEnum index, Texture2D texture)
        {
            _textureList[index] = texture;
        }
        public static Texture2D Get(TextureEnum index)
        {
            return _textureList[index];
        }
    }
}
