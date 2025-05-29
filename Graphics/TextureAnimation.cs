using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinuteBattle.Graphics
{
    public class TextureAnimation
    {
        public static TextureAnimation EmptyAnimation = new TextureAnimation(Vector2.Zero, TextureEnum.Empty);
        public Vector2 _origin = Vector2.Zero;
        public TextureEnum _textureId = TextureEnum.Empty;
        public TextureAnimation(Vector2 origin, TextureEnum textureId) 
        {
            _origin = origin;
            _textureId = textureId;
        }
        public Texture2D getTexture()
        {
            return TextureDictionary.Get(_textureId);
        }
    }
}
