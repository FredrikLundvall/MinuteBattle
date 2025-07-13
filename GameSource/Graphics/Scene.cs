using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MinuteBattle.Graphics
{
    public class Scene
    {
        public static Scene EmptyScene = new();
        internal Dictionary<int, Puppet> _puppetList = [];
        public void AddPuppet(int id, int puppetType, Vector2 position, float rotation, Action clickAction, Rectangle clickRectangle)
        {
            _puppetList.Add(id, PuppetFactory.CreatePuppet(puppetType, position, rotation, clickAction, clickRectangle));
        }
        public Puppet GetPuppet(int id)
        {
            if (_puppetList.ContainsKey(id))
                return _puppetList[id];
            else
                return Puppet.EmptyPuppet;
        }
        public void Update(GameTime gameTime)
        {
            var filteredList = _puppetList
                .Where(puppet => puppet.Value._clickAction != Puppet.EmptyAction && puppet.Value._clickRectangle != Rectangle.Empty)
                .Select(puppet => puppet.Value)
                .ToList();
            foreach (Puppet puppet in filteredList)
            {
                if(MouseChecker.IsCurrentlyOverArea(puppet._clickRectangle) && MouseChecker.ButtonIsCurrentlyPressed(MouseButtonEnum.LeftButton))
                {
                    puppet._clickAction.Invoke();
                }
            }
        }
        public void Draw(GameTime gameTime)
        {
            Globals.StaticSpriteBatch.Begin();
            foreach (Puppet puppet in _puppetList.Values)
            {
                puppet.Draw(gameTime);
                Globals.DrawRectangle( puppet._clickRectangle, Color.Cyan);
            }
            Globals.StaticSpriteBatch.End();
        }
    }
}
