using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Reflection.Metadata.Ecma335;

namespace MinuteBattle.Graphics
{
    public class TextureAnimation : IClip
    {
        public static TextureAnimation EmptyAnimation = new TextureAnimation(0, TextureEnum.Empty, Vector2.Zero, Vector2.Zero, 0, 1.0f);
        ClipCategoryEnum _clipCategory = ClipCategoryEnum.Unknown;
        public Vector2 _origin = Vector2.Zero;
        public TextureEnum _textureId = TextureEnum.Empty;
        public Vector2 _offsetPosition = Vector2.Zero;
        public float _offsetRotation = 0;
        public float _scale = 1.0f;
        public TextureAnimation(ClipCategoryEnum clipCategory, TextureEnum textureId, Vector2 origin, Vector2 offsetPosition, float offsetRotation, float scale)
        {
            _clipCategory = clipCategory;
            _origin = origin;
            _textureId = textureId;
            _offsetPosition = offsetPosition;
            _offsetRotation = offsetRotation;
            _scale = scale;
        }
        public ClipCategoryEnum GetCategory() { return _clipCategory; } 
        public void Draw(Vector2 parentPosition, float parentRotation, GameTime gameTime)
        {
            Globals.StaticSpriteBatch.Draw(TextureDictionary.Get(_textureId), parentPosition + _offsetPosition, null, Color.White, parentRotation + _offsetRotation, _origin, _scale, SpriteEffects.None, 0f);
        }
        public Vector2 getSize()
        {
            var bounds = TextureDictionary.Get(_textureId).Bounds;
            return new(bounds.Width * _scale, bounds.Height * _scale);
        }
        public Rectangle GetBoundingRectangle(Vector2 parentPosition, float parentRotation)
        {
            Rectangle bounds = TextureDictionary.Get(_textureId).Bounds;
            Rectangle rectangle = new(0, 0, (int)float.Ceiling(bounds.Width * _scale), (int)float.Ceiling(bounds.Height * _scale));
            rectangle.Offset(parentPosition + _offsetPosition - _origin * _scale);
            return rectangle;
        }
        public bool SetText(string text)
        {
            return false;
        }
        public bool SetRotation(float rotation)
        {
            if(this == EmptyAnimation) return false;
            _offsetRotation = rotation;
            return true;
        }
        public bool SetOrigin(Vector2 origin)
        {
            if (this == EmptyAnimation) return false;
            _origin = origin;
            return true;
        }
    }
}
