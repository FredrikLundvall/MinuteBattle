using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MinuteBattle.Logic;
using System.Collections.Generic;

namespace MinuteBattle.Graphics
{
    public static class Stage
    {
        const int START_SCENE_ID = 0;
        internal static Dictionary<int, Scene> _sceneList = new Dictionary<int, Scene>();
        internal static void AddScene(int id, Scene scene)
        {
            _sceneList.Add(id, scene);
        }
        internal static Scene getScene(int id)
        {
            if (_sceneList.ContainsKey(id))
                return _sceneList[id];
            else
                return Scene.EmptyScene;
        }
        public static void AddStartScene(CardGame game, Viewport viewport)
        {
            Scene startScene = new Scene();
            startScene.AddPuppet(0, 3, new Vector2(viewport.Width / 2, viewport.Height / 5), 0, new(() => { game.Start(); }));
            startScene.getPuppet(0).getAllClips(ClipCategoryEnum.NameTag).ForEach(clip => clip.SetText("Start"));
            AddScene(START_SCENE_ID, startScene);
        }
        public static Scene GetCurrentScene(CardGame game)
        {
            if(game._campaign._state == CampaignStateEnum.NotStarted)
            {
                return getScene(START_SCENE_ID);
            }
            else
            {
                return Scene.EmptyScene;
            }
        }
    }
}
