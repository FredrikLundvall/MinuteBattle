using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinuteBattle.Graphics
{
    public class Puppet
    {
        public Vector2 _position = Vector2.Zero;
        public float _rotation = 0;
        public TextureAnimation _animation = TextureAnimation.EmptyAnimation;
        public Puppet(TextureAnimation animation)
        {
            _animation = animation;
        }
        public void Draw(GameTime gameTime)
        {
            Globals.StaticSpriteBatch.Draw(_animation.getTexture(), _position, null, Color.White, _rotation, _animation._origin, 0.5f, SpriteEffects.None, 0f);
        }
    }
}
