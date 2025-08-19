using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace MinuteBattle.Graphics
{
    public class Scene
    {
        public static Scene EmptyScene = new();
        internal Dictionary<int, Puppet> _puppetList = [];
        public Puppet _draggedPuppet = Puppet.EmptyPuppet;
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
            var isFocused = false;
            foreach (Puppet puppet in filteredList)
            {
                puppet._isReleased = false;
                if (MouseChecker.IsCurrentlyOverArea(puppet._clickRectangle))
                {
                    puppet._isMouseOver = true;
                }
                else
                {
                    puppet._isMouseOver = false;
                }
                if (MouseChecker.ButtonIsCurrentlyPressed(MouseButtonEnum.LeftButton))
                {
                    if (!puppet._isMouseOver && !puppet._isPressed)
                    {
                        puppet._isPressedOutside = true;
                    }
                    puppet._isPressed = true;
                }
                else
                {
                    if (puppet._isMouseOver && puppet._isPressed && !puppet._isPressedOutside)
                    {
                        puppet._isReleased = true;
                    }
                    puppet._isPressed = false;
                    puppet._isPressedOutside = false;
                }
                if (!(puppet._isPressed && !puppet._isPressedOutside) && puppet._isReleased)
                {
                    //Invoke the puppets click action
                    puppet._clickAction.Invoke();
                }
                isFocused |= puppet._isMouseOver;
            }
            _draggedPuppet._position = MouseChecker.GetCurrentCoord();
            if (isFocused)
            {
                Mouse.SetCursor(TextureDictionary._mouseHand);
            }
            else
            {
                Mouse.SetCursor(TextureDictionary._mouseArrow);
            }            
        }
        public void Draw(GameTime gameTime)
        {
            Globals.GraphicsDeviceMan.GraphicsDevice.Clear(Color.Black);
            Globals.StaticSpriteBatch.Begin();
            foreach (Puppet puppet in _puppetList.Values)
            {
                puppet.Draw(gameTime);
                if (puppet._isMouseOver && puppet._highligthOnMouseOver)
                {
                    if (puppet._isPressed && !puppet._isPressedOutside)
                    {
                        //Draw the puppet as currently pressed
                        Globals.DrawRectangle(puppet._clickRectangle, Color.Cyan);
                    }
                    else
                    {
                        //Draw the puppet as focused
                        Globals.DrawRectangle(puppet._clickRectangle, Color.DarkSlateGray);
                    }
                }
            }
            _draggedPuppet.Draw(gameTime);
            Globals.StaticSpriteBatch.End();
        }
    }
}
