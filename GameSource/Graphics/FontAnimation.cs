using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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
        public Color _color = Color.White;
        public FontAnimation(ClipCategoryEnum clipCategory, FontEnum fontId, Vector2 origin, Vector2 offsetPosition, float offsetRotation, bool useParentRotation, string text, Color color)
        {
            _clipCategory = clipCategory;
            _fontId = fontId;
            _origin = origin;
            _offsetPosition = offsetPosition;
            _offsetRotation = offsetRotation;
            _useParentRotation = useParentRotation;
            _text = text;
            _color = color;
        }
        public Vector2 getSize()
        {
            return FontDictionary.Get(_fontId).MeasureString(_text);
        }
        public void Draw(Vector2 parentPosition, float parentRotation, GameTime gameTime)
        {
            Globals.StaticSpriteBatch.DrawString(FontDictionary.Get(_fontId), _text, parentPosition + _offsetPosition, _color, (_useParentRotation)? parentRotation : 0 + _offsetRotation, _origin, 1.0f, SpriteEffects.None, 0.5f);
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
        public bool SetOrigin(Vector2 origin)
        {
            if (this == EmptyAnimation) return false;
            _origin = origin;
            return true;
        }
        public Rectangle GetBoundingRectangle(Vector2 parentPosition, float parentRotation)
        {
            Rectangle rectangle = new();
            rectangle.Size = getSize().ToPoint();
            rectangle.Offset(parentPosition + _offsetPosition - _origin);
            return rectangle;
        }
        public bool SetColor(Color color)
        {
            if (this == EmptyAnimation) return false;
            _color = color;
            return true;
        }
    }
}
