using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Reflection.Metadata.Ecma335;

namespace MinuteBattle.Graphics
{
    public class TextureAnimation : IClip
    {
        public static TextureAnimation EmptyAnimation = new TextureAnimation(0, TextureEnum.Empty, Vector2.Zero, Vector2.Zero, 0);
        ClipCategoryEnum _clipCategory = ClipCategoryEnum.Unknown;
        public Vector2 _origin = Vector2.Zero;
        public TextureEnum _textureId = TextureEnum.Empty;
        public Vector2 _offsetPosition = Vector2.Zero;
        public float _offsetRotation = 0;
        public TextureAnimation(ClipCategoryEnum clipCategory, TextureEnum textureId, Vector2 origin, Vector2 offsetPosition, float offsetRotation)
        {
            _clipCategory = clipCategory;
            _origin = origin;
            _textureId = textureId;
            _offsetPosition = offsetPosition;
            _offsetRotation = offsetRotation;
        }
        public ClipCategoryEnum GetCategory() { return _clipCategory; } 
        public void Draw(Vector2 parentPosition, float parentRotation, GameTime gameTime)
        {
            Globals.StaticSpriteBatch.Draw(TextureDictionary.Get(_textureId), parentPosition + _offsetPosition, null, Color.White, parentRotation + _offsetRotation, _origin, 0.5f, SpriteEffects.None, 0f);
        }
        public Rectangle GetBoundingRectangle(Vector2 parentPosition, float parentRotation)
        {
            Rectangle bounds = TextureDictionary.Get(_textureId).Bounds;
            float scale = 0.5f; // Assuming a scale of 0.5f as in the Draw method
            Rectangle rectangle = new(0, 0, (int)float.Ceiling(bounds.Width * scale), (int)float.Ceiling(bounds.Height * scale));
            rectangle.Offset(parentPosition + _offsetPosition - _origin * scale);
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
    }
}
