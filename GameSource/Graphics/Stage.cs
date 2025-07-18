﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MinuteBattle.Logic;
using System.Collections.Generic;

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
            scene.AddPuppet(0, PuppetEnum.MapGreatPlain, mapOffset, 0, Puppet.EmptyAction, Rectangle.Empty);
            foreach (var terrain in game._campaign._battle._map._terrain)
            {
                if (terrain._terrainType == TerrainTypeEnum.Ditch)
                {
                    scene.AddPuppet(1, PuppetEnum.TerrainDitch, new Vector2(terrain._x, terrain._y) + mapOffset, 0, Puppet.EmptyAction, Rectangle.Empty);
                }
                else if (terrain._terrainType == TerrainTypeEnum.Bush)
                {
                    scene.AddPuppet(2, PuppetEnum.TerrainBush, new Vector2(terrain._x, terrain._y) + mapOffset, 0, Puppet.EmptyAction, Rectangle.Empty);
                }
                else if (terrain._terrainType == TerrainTypeEnum.Hill)
                {
                    scene.AddPuppet(3, PuppetEnum.TerrainHill, new Vector2(terrain._x, terrain._y) + mapOffset, 0, Puppet.EmptyAction, Rectangle.Empty);
                }
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
