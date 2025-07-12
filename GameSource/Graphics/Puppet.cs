using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MinuteBattle.Graphics
{
    public class Puppet
    {
        public static Puppet EmptyPuppet = new Puppet(Vector2.Zero, 0, EmptyAction);
        public Vector2 _position = Vector2.Zero;
        public float _rotation = 0;
        public List<IClip> _clipList = [];
        public Action _clickAction = EmptyAction;
        public static Action EmptyAction = new(() => { });
        public Puppet(Vector2 position, float rotation, Action clickAction)
        {
            _position = position;
            _rotation = rotation;
            _clickAction = clickAction;
        }
        public void AddClip(IClip clip)
        {
            _clipList.Add(clip);
        }
        public IClip getFirstClip(ClipCategoryEnum clipCategory)
        {
            foreach (IClip clip in _clipList)
            {
                if(clip.GetCategory() == clipCategory) return clip; 
            }
            return TextureAnimation.EmptyAnimation;
        }
        public List<IClip> getAllClips(ClipCategoryEnum clipCategory)
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
    }
}
