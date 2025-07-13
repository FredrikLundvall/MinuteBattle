using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MinuteBattle.Logic;
using System.Collections.Generic;

namespace MinuteBattle.Graphics
{
    public static class Stage
    {
        const int START_SCENE_ID = 0;
        internal static Dictionary<int, Scene> _sceneList = [];
        internal static void AddScene(int id, Scene scene)
        {
            _sceneList.Add(id, scene);
        }
        internal static Scene GetScene(int id)
        {
            if (_sceneList.ContainsKey(id))
                return _sceneList[id];
            else
                return Scene.EmptyScene;
        }
        public static void AddStartScene(CardGame game, Viewport viewport)
        {
            Scene startScene = new();
            startScene.AddPuppet(0, 3, new Vector2(viewport.Width / 2, viewport.Height / 5), 0, new(() => { game.Start(); }), Rectangle.Empty);
            startScene.GetPuppet(0).GetAllClips(ClipCategoryEnum.NameTag).ForEach(clip => clip.SetText("Start"));
            startScene.GetPuppet(0).MakeBoundingRectangle();
            AddScene(START_SCENE_ID, startScene);
        }
        public static Scene GetCurrentScene(CardGame game)
        {
            if(game._campaign._state == CampaignStateEnum.NotStarted)
            {
                return GetScene(START_SCENE_ID);
            }
            else
            {
                return Scene.EmptyScene;
            }
        }
    }
}
