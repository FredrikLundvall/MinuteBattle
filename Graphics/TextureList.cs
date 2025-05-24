using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinuteBattle.Graphics
{
    public static class TextureList
    {
        public const int EMPTY = 0;
        public const int BRIT_SOLDIER = 1;
        public const int GERM_SOLDIER = 2;
        internal static Dictionary<int, Texture2D> _textureList = new Dictionary<int, Texture2D>();
        public static void Add(int index, Texture2D texture)
        {
            _textureList[index] = texture;
        }
        public static Texture2D Get(int index)
        {
            return _textureList[index];
        }
    }
}
