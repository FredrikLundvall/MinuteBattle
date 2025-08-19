using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MinuteBattle.Graphics
{
    public class Puppet
    {
        public static Puppet EmptyPuppet = new Puppet(Vector2.Zero, 0, EmptyAction, Rectangle.Empty);
        public Vector2 _position = Vector2.Zero;
        public float _rotation = 0;
        public List<IClip> _clipList = [];
        public Action _clickAction = EmptyAction;
        public static Action EmptyAction = new(() => { });
        public Rectangle _clickRectangle = Rectangle.Empty;
        public bool _isMouseOver = false;
        public bool _isPressed = false;
        public bool _isPressedOutside = false;
        public bool _isReleased = false;
        public bool _highligthOnMouseOver = true;
        public bool _isAcceptingDrops = false;

        public Puppet(Vector2 position, float rotation, Action clickAction, Rectangle clickRectangle)
        {
            _position = position;
            _rotation = rotation;
            _clickAction = clickAction;
            _clickRectangle = clickRectangle;
        }
        public void AddClip(IClip clip)
        {
            _clipList.Add(clip);
        }
        public IClip GetFirstClip(ClipCategoryEnum clipCategory)
        {
            foreach (IClip clip in _clipList)
            {
                if(clip.GetCategory() == clipCategory) return clip; 
            }
            return TextureAnimation.EmptyAnimation;
        }
        public List<IClip> GetAllClips(ClipCategoryEnum clipCategory)
        {
            var filteredList = _clipList.Where(clip => clip.GetCategory() == clipCategory).ToList();
            if (filteredList.Count > 0)
                return filteredList;
            return [TextureAnimation.EmptyAnimation];
        }
        public void Draw(GameTime gameTime)
        {
            foreach (IClip clip in _clipList)
            {
                clip.Draw(_position, _rotation, gameTime);
            }
        }
        public void MakeBoundingRectangle()
        {
            if (_clipList.Count == 0)
            {
                _clickRectangle = Rectangle.Empty;
                return;
            }
            Rectangle boundingRectangle = Rectangle.Empty;
            foreach (IClip clip in _clipList)
            {
                Rectangle clipRectangle = clip.GetBoundingRectangle(_position, _rotation);
                if (!clipRectangle.IsEmpty)
                {
                    if (!boundingRectangle.IsEmpty)
                    {
                        // Combine the current clip's rectangle with the bounding rectangle
                        boundingRectangle = Rectangle.Union(boundingRectangle, clipRectangle);
                    }
                    else
                    {
                        boundingRectangle = clipRectangle;
                    }
                }
            }
            _clickRectangle = boundingRectangle;
        }
    }
}
