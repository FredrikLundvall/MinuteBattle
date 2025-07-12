using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace MinuteBattle.Graphics
{
    public class Scene
    {
        public static Scene EmptyScene = new Scene();
        internal Dictionary<int, Puppet> _puppetList = new Dictionary<int, Puppet>();
        public void AddPuppet(int id, int puppetType, Vector2 position, float rotation, Action clickAction)
        {
            _puppetList.Add(id, PuppetFactory.CreatePuppet(puppetType, position, rotation, clickAction));
        }
        public Puppet getPuppet(int id)
        {
            if (_puppetList.ContainsKey(id))
                return _puppetList[id];
            else
                return Puppet.EmptyPuppet;
        }
        public void Draw(GameTime gameTime)
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
