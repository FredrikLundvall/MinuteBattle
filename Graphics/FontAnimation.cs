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
    public class FontAnimation: IClip
    {
        public static FontAnimation EmptyAnimation = new FontAnimation(ClipCategoryEnum.Unknown, FontEnum.Empty, Vector2.Zero, Vector2.Zero, 0, true, "", Color.Transparent);
        ClipCategoryEnum _clipCategory = ClipCategoryEnum.Unknown;
        public FontEnum _fontId = FontEnum.Empty;
        public Vector2 _origin = Vector2.Zero;
        public Vector2 _offsetPosition = Vector2.Zero;
        public float _offsetRotation = 0;
        public bool _useParentRotation = true;
        public string _text = "";
        public Color _textColor = Color.White;
        public FontAnimation(ClipCategoryEnum clipCategory, FontEnum fontId, Vector2 origin, Vector2 offsetPosition, float offsetRotation, bool useParentRotation, string text, Color textColor)
        {
            _clipCategory = clipCategory;
            _fontId = fontId;
            _origin = origin;
            _offsetPosition = offsetPosition;
            _offsetRotation = offsetRotation;
            _useParentRotation = useParentRotation;
            _text = text;
            _textColor = textColor;
        }
        public Vector2 getTextSize()
        {
            return FontDictionary.Get(_fontId).MeasureString(_text);
        }
        public void Draw(Vector2 parentPosition, float parentRotation, GameTime gameTime)
        {
            Globals.StaticSpriteBatch.DrawString(FontDictionary.Get(_fontId), _text, parentPosition + _offsetPosition, _textColor, (_useParentRotation)? parentRotation : 0 + _offsetRotation, _origin, 1.0f, SpriteEffects.None, 0.5f);
        }
        public ClipCategoryEnum GetCategory()
        {
            return _clipCategory;
        }
        public bool SetText(string text)
        {
            if (this == EmptyAnimation) return false;
            _text = text;
            return true;
        }
        public bool SetRotation(float rotation)
        {
            if (this == EmptyAnimation) return false;
            _offsetRotation = rotation;
            return true;
        }
    }
}
