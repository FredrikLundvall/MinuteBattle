using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinuteBattle.Graphics
{
    public class Animation
    {
        public static Animation EmptyAnimation = new Animation(Vector2.Zero, TextureList.EMPTY);
        public Vector2 _origin = Vector2.Zero;
        public int _textureId = 0;
        public Animation(Vector2 origin, int textureId) 
        {
            _origin = origin;
            _textureId = textureId;
        }
        public Texture2D getTexture()
        {
            return TextureList.Get(_textureId);
        }
    }
}
