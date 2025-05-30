using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinuteBattle.Graphics
{
    public static class Scene
    {
        internal static Dictionary<int, Puppet> _puppetList = new Dictionary<int, Puppet>();
        public static void AddPuppet(int id, PuppetEnum puppetType)
        {
            _puppetList.Add(id, PuppetFactory.CreatePuppet(puppetType));
        }
        public static void UpdatePuppet(int id, Vector2 position, float rotation, TextureAnimation animation)
        {
            if (_puppetList.ContainsKey(id))
            {
                _puppetList[id]._position = position;
                _puppetList[id]._rotation = rotation;
                if (animation != null)
                {
                    _puppetList[id]._animation = animation;
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
