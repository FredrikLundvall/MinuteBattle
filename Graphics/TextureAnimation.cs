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
        public static TextureAnimation EmptyAnimation = new TextureAnimation(Vector2.Zero, TextureEnum.Empty, Vector2.Zero, 0);
        public Vector2 _origin = Vector2.Zero;
        public TextureEnum _textureId = TextureEnum.Empty;
        public Vector2 _offsetPosition = Vector2.Zero;
        public float _offsetRotation = 0;
        public TextureAnimation(Vector2 origin, TextureEnum textureId, Vector2 offsetPosition, float offsetRotation)
        {
            _origin = origin;
            _textureId = textureId;
            _offsetPosition = offsetPosition;
            _offsetRotation = offsetRotation;
        }
        public Texture2D getTexture()
        {
            return TextureDictionary.Get(_textureId);
        }
        public void Draw(Vector2 parentPosition, float parentRotation, GameTime gameTime)
        {
            Globals.StaticSpriteBatch.Draw(getTexture(), parentPosition + _offsetPosition, null, Color.White, parentRotation + _offsetRotation, _origin, 0.5f, SpriteEffects.None, 0f);
        }
    }
}
