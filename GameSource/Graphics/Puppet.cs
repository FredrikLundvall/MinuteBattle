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
    public class Puppet
    {
        public static Puppet EmptyPuppet = new Puppet(Vector2.Zero, 0);
        public Vector2 _position = Vector2.Zero;
        public float _rotation = 0;
        public List<IClip> _clipList = [];
        public Puppet(Vector2 position, float rotation)
        {
            _position = position;
            _rotation = rotation;
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
