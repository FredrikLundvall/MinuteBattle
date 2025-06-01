using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace MinuteBattle.Graphics
{
    public class Puppet
    {
        public Vector2 _position = Vector2.Zero;
        public float _rotation = 0;
        public TextureAnimation _textureAnimation = TextureAnimation.EmptyAnimation;
        public FontAnimation _fontAnimation = FontAnimation.EmptyAnimation;
        public Puppet(TextureAnimation textureAnimation, FontAnimation fontAnimation)
        {
            _textureAnimation = textureAnimation;
            _fontAnimation = fontAnimation;
        }
        public void Draw(GameTime gameTime)
        {
            _textureAnimation.Draw(_position, _rotation, gameTime);
            _fontAnimation.Draw(_position, gameTime);
        }
    }
}
