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
        public static FontAnimation EmptyAnimation = new FontAnimation(Vector2.Zero, FontEnum.Empty, Vector2.Zero, "", Color.Transparent);
        public Vector2 _textOffset = Vector2.Zero;
        public string _text = "";
        public Color _textColor = Color.White;
        public Vector2 _origin = Vector2.Zero;
        public FontEnum _fontId = FontEnum.Empty;
        public FontAnimation(Vector2 origin, FontEnum fontId, Vector2 textOffset, string text, Color textColor)
        {
            _origin = origin;
            _fontId = fontId;
            _textOffset = textOffset;
            _text = text;
            _textColor = textColor;
        }
        public SpriteFont getFont()
        {
            return FontDictionary.Get(_fontId);
        }
        public void Draw(Vector2 parentPosition, GameTime gameTime)
        {
            Globals.StaticSpriteBatch.DrawString(getFont(), _text, parentPosition + _textOffset, _textColor, 0, _origin, 1.0f, SpriteEffects.None, 0.5f);
        }
    }
}
