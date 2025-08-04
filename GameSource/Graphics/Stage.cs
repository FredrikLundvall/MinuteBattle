using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MinuteBattle.Logic;
using System.Collections.Generic;
using System.Linq;

namespace MinuteBattle.Graphics
{ 
    public static class Stage
    {
        const int START_SCENE_ID = 0;
        const int BATTLE_SCENE_ID = 1;
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
        public static void AddStartScene(CardGame game)
        {
            Scene startScene = new();
            Viewport viewport = Globals.GraphicsDeviceMan.GraphicsDevice.Viewport;
            startScene.AddPuppet(0, PuppetEnum.StartButton, new Vector2(viewport.Width / 2, viewport.Height / 5), 0, new(() => { game.Start(); }), Rectangle.Empty);
            startScene.GetPuppet(0).GetAllClips(ClipCategoryEnum.NameTag).ForEach(clip => clip.SetText("Start"));
            startScene.GetPuppet(0).MakeBoundingRectangle();
            AddScene(START_SCENE_ID, startScene);
        }
        public static Scene AddBattleScene(CardGame game)
        {
            Scene scene = new();
            Viewport viewport = Globals.GraphicsDeviceMan.GraphicsDevice.Viewport;
            Vector2 mapOffset = new Vector2(160, 140);
            int id = 0;
            scene.AddPuppet(id++, PuppetEnum.MapGreatPlain, mapOffset, 0, new(() => {
                //Drop the puppet beeing dragged (if there is one)
                if (scene._draggedPuppet != Puppet.EmptyPuppet)
                {
                    var nexId = scene._puppetList.Keys.Max() + 1;
                    scene._puppetList.Add(nexId, scene._draggedPuppet);
                    scene._draggedPuppet = Puppet.EmptyPuppet;
                }
            }), Rectangle.Empty);
            scene.GetPuppet(id - 1).MakeBoundingRectangle();
            foreach (var terrain in game._campaign._battle._map._terrain)
            {
                if (terrain._terrainType == TerrainTypeEnum.Ditch)
                {
                    scene.AddPuppet(id++, PuppetEnum.TerrainDitch, new Vector2(terrain._x, terrain._y) + mapOffset, 0, Puppet.EmptyAction, Rectangle.Empty);
                }
                else if (terrain._terrainType == TerrainTypeEnum.Bush)
                {
                    scene.AddPuppet(id++, PuppetEnum.TerrainBush, new Vector2(terrain._x, terrain._y) + mapOffset, 0, Puppet.EmptyAction, Rectangle.Empty);
                }
                else if (terrain._terrainType == TerrainTypeEnum.Hill)
                {
                    scene.AddPuppet(id++, PuppetEnum.TerrainHill, new Vector2(terrain._x, terrain._y) + mapOffset, 0, Puppet.EmptyAction, Rectangle.Empty);
                }
            }
            int x = 80;
            int y = 180;
            foreach (var cardInDeck in game._hero._cardDeck)
            {
                if (cardInDeck._cardType == CardTypeEnum.HeroMelee)
                {
                    scene.AddPuppet(id++, PuppetEnum.HeroMelee, new Vector2(x, y), 0, new(() => {
                        //Add a puppet to the scene as beeing dragged following the mouse, to represent the card
                        if(scene._draggedPuppet == Puppet.EmptyPuppet)
                        {
                            scene._draggedPuppet = PuppetFactory.CreatePuppet(PuppetEnum.HeroMelee, new Vector2(x, y), 0, Puppet.EmptyAction, Rectangle.Empty);
                        }
                    }), Rectangle.Empty);
                    var clip = scene.GetPuppet(id - 1).GetFirstClip(ClipCategoryEnum.NameTag);
                    clip.SetText(cardInDeck._name);
                    var size = clip.getSize();
                    clip.SetOrigin(new Vector2(size.X / 2, -16));
                    scene.GetPuppet(id - 1).MakeBoundingRectangle();
                }
                y += 70;
            }
            AddScene(BATTLE_SCENE_ID, scene);
            return scene;
        }
        public static Scene GetCurrentScene(CardGame game)
        {
            if(game._campaign._state == CampaignStateEnum.NotStarted)
            {
                return GetScene(START_SCENE_ID);
            }
            else if (game._campaign._state == CampaignStateEnum.Battle)
            {
                Scene scene = GetScene(BATTLE_SCENE_ID); 
                if (scene == Scene.EmptyScene)
                    scene = AddBattleScene(game);
                return GetScene(BATTLE_SCENE_ID);
            }
            else
            {
                return Scene.EmptyScene;
            }
        }
    }
}
