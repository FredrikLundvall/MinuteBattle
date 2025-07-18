﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MinuteBattle.Graphics
{
    public class Scene
    {
        public static Scene EmptyScene = new();
        internal Dictionary<int, Puppet> _puppetList = [];
        public void AddPuppet(int id, PuppetEnum puppetType, Vector2 position, float rotation, Action clickAction, Rectangle clickRectangle)
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
                puppet._isReleased = false;
                if (MouseChecker.IsCurrentlyOverArea(puppet._clickRectangle))
                {
                    puppet._isFocused = true;
                }
                else
                {
                    puppet._isFocused = false;
                }
                if (MouseChecker.ButtonIsCurrentlyPressed(MouseButtonEnum.LeftButton))
                {
                    if (!puppet._isFocused && !puppet._isPressed)
                    {
                        puppet._isPressedOutside = true;
                    }
                    puppet._isPressed = true;
                }
                else
                {
                    if (puppet._isFocused && puppet._isPressed && !puppet._isPressedOutside)
                    {
                        puppet._isReleased = true;
                    }
                    puppet._isPressed = false;
                    puppet._isPressedOutside = false;
                }
            }
        }
        public void Draw(GameTime gameTime)
        {
            Globals.GraphicsDeviceMan.GraphicsDevice.Clear(Color.Black);
            Globals.StaticSpriteBatch.Begin();
            foreach (Puppet puppet in _puppetList.Values)
            {
                puppet.Draw(gameTime);
                if (puppet._isFocused)
                {
                    if (puppet._isPressed && !puppet._isPressedOutside)
                    {
                        //Draw the puppet as currently pressed
                        Globals.DrawRectangle(puppet._clickRectangle, Color.Cyan);
                    }
                    else if (puppet._isReleased)
                    {
                        //Invoke the puppets click action
                        puppet._clickAction.Invoke();
                    }
                    else
                    {
                        //Draw the puppet as focused
                        Globals.DrawRectangle(puppet._clickRectangle, Color.DarkSlateGray);
                    }
                }
            }
            Globals.StaticSpriteBatch.End();
        }
    }
}
