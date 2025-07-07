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
        public static void AddPuppet(int id, int puppetType, Vector2 position, float rotation)
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
        public static void InitScene(Viewport viewport, int brittishPrivateId, int germanPrivateId, int germanMachineGunId)
        {
            Scene.AddPuppet(brittishPrivateId, 0, new Vector2(viewport.Width / 2, viewport.Height / 2), 0);
            Scene.AddPuppet(germanPrivateId, 1, new Vector2(viewport.Width / 3, viewport.Height / 3), MathHelper.Pi / 2);
            Scene.AddPuppet(germanMachineGunId, 2, new Vector2(viewport.Width / 4, viewport.Height / 2 + viewport.Height / 4), MathHelper.Pi / 10);

            Scene.getPuppet(brittishPrivateId).getFirstClip(ClipCategoryEnum.NameTag).SetText("Garreth");
            Scene.getPuppet(germanPrivateId).getFirstClip(ClipCategoryEnum.NameTag).SetText("Heisenberg");
            Scene.getPuppet(germanMachineGunId).getFirstClip(ClipCategoryEnum.NameTag).SetText("Eichmann");
        }
    }
}
