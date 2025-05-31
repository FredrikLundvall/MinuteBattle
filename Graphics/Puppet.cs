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
        public Vector2 _textOffset = Vector2.Zero;
        public string _text = "";
        public Color _textColor = Color.White;
        public TextureAnimation _textureAnimation = TextureAnimation.EmptyAnimation;
        public FontAnimation _fontAnimation = FontAnimation.EmptyAnimation;
        public Puppet(TextureAnimation textureAnimation, FontAnimation fontAnimation)
        {
            _textureAnimation = textureAnimation;
            _fontAnimation = fontAnimation;
        }
        public void Draw(GameTime gameTime)
        {
            Globals.StaticSpriteBatch.Draw(_textureAnimation.getTexture(), _position, null, Color.White, _rotation, _textureAnimation._origin, 0.5f, SpriteEffects.None, 0f);
            Globals.StaticSpriteBatch.DrawString(_fontAnimation.getFont(), _text, _position + _textOffset, _textColor, 0, _fontAnimation._origin, 1.0f, SpriteEffects.None, 0.5f);
        }
    }
}
