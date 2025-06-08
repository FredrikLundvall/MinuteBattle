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
        public static void AddPuppet(int id, PuppetEnum puppetType, Vector2 position, float rotation)
        {
            _puppetList.Add(id, PuppetFactory.CreatePuppet(puppetType, position, rotation));
        }
        public static Puppet getPuppet(int id)
        {
            if (_puppetList.ContainsKey(id))
                return _puppetList[id];
            else
                return Puppet.EmptyPuppet;
        }
        public static void Draw(GameTime gameTime)
        {
            Globals.StaticSpriteBatch.Begin();
            foreach (Puppet puppet in _puppetList.Values)
            {
                puppet.Draw(gameTime);
            }
            Globals.StaticSpriteBatch.End();
        }
    }
}
