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
    public static class Scene
    {
        internal static Dictionary<int, Puppet> _puppetList = new Dictionary<int, Puppet>();
        public static void AddPuppet(int id, PuppetEnum puppetType)
        {
            _puppetList.Add(id, PuppetFactory.CreatePuppet(puppetType));
        }
        public static void UpdatePuppet(int id, Vector2 position, float rotation, TextureAnimation textureAnimation, Vector2 textOffset, string text, Color textColor, FontAnimation fontAnimation)
        {
            if (_puppetList.ContainsKey(id))
            {
                _puppetList[id]._position = position;
                _puppetList[id]._rotation = rotation;
                _puppetList[id]._textOffset = textOffset;

                _puppetList[id]._text = text;
                _puppetList[id]._textColor = textColor;
                if (textureAnimation != null)
                {
                    _puppetList[id]._textureAnimation = textureAnimation;
                }
                if (fontAnimation != null)
                {
                    _puppetList[id]._fontAnimation = fontAnimation;
                }
                if (_puppetList[id]._fontAnimation != null)
                {
                    _puppetList[id]._textOffset.X = _puppetList[id]._textOffset.X - (_puppetList[id]._fontAnimation.getFont().MeasureString(text).X / 2);
                }
            }
        }
        public static void Draw(GameTime gameTime)
        {
            foreach (Puppet puppet in _puppetList.Values)
            {
                puppet.Draw(gameTime);
            }
        }
    }
}
