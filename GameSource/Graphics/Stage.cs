using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
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
            startScene.AddPuppet(0, PuppetEnum.Button, new Vector2(viewport.Width / 2, viewport.Height / 5), 0, new((originPuppet) => {
                Globals._testTransition.Play(0.4f, 0.0f, 0.0f);
                // check the current state of the MediaPlayer.
                if (MediaPlayer.State != MediaState.Stopped)
                {
                    MediaPlayer.Stop(); // stop current audio playback if playing or paused.
                }
                MediaPlayer.Volume = 0.08f;
                // Play the selected song reference.
                MediaPlayer.Play(Globals._testSong);
                game.Start(); 
            }), Rectangle.Empty);
            startScene.GetPuppet(0).GetAllClips(ClipCategoryEnum.NameTag).ForEach(clip => clip.SetText("Start"));
            startScene.GetPuppet(0).MakeBoundingRectangle();
            AddScene(START_SCENE_ID, startScene);
        }
        public static Scene AddBattleScene(CardGame game)
        {
            Scene scene = new();
            Viewport viewport = Globals.GraphicsDeviceMan.GraphicsDevice.Viewport;
            Vector2 mapOffset = Vector2.Zero;
            int id = 0;
            id = AddPaperSheetToScene(scene, mapOffset, id);
            id = AddButtonsToScene(scene, id);
            id = AddTerrainToScene(game, scene, mapOffset, id);
            id = AddHeroDeckToScene(game, scene, id);
            AddScene(BATTLE_SCENE_ID, scene);
            return scene;
        }
        private static int AddButtonsToScene(Scene scene, int id)
        {
            // Add Send Order button
            scene.AddPuppet(id++, PuppetEnum.Button, new Vector2(115, 1020), 0, Puppet.EmptyAction, Rectangle.Empty);
            var clipButton = scene.GetPuppet(id - 1).GetFirstClip(ClipCategoryEnum.NameTag);
            clipButton.SetText("Send order");
            var sizeButton = clipButton.getSize();
            clipButton.SetOrigin(new Vector2(sizeButton.X / 2, 10));
            scene.GetPuppet(id - 1).MakeBoundingRectangle();
            return id;
        }
        private static int AddPaperSheetToScene(Scene scene, Vector2 mapOffset, int id)
        {
            // Add PaperSheet puppet (drop zone)
            scene.AddPuppet(id++, PuppetEnum.PaperSheet, mapOffset, 0, originPuppet =>
            {
                if (scene._draggedPuppet != Puppet.EmptyPuppet)
                {
                    Globals._testClick.Play(0.4f, 0.0f, 0.0f);
                    //Drop the picked up puppet onto the papersheet
                    var droppedPuppet = scene._draggedPuppet;
                    droppedPuppet._clipList.ForEach(clip => clip.SetColor(Color.White * 0.5f));
                    droppedPuppet.MakeBoundingRectangle();
                    //Add some pickup action to the dropped puppet so it can be picked up again
                    droppedPuppet._clickAction = origin =>
                    {
                        if (scene._draggedPuppet == Puppet.EmptyPuppet)
                        {
                            Globals._testClick.Play(0.4f, 0.0f, 0.0f);
                            //Remove the picked up puppet so it isn't copied when dropped again
                            var key = scene._puppetList.FirstOrDefault(x => x.Value == origin).Key;
                            scene._puppetList.Remove(key);
                            droppedPuppet._clipList.ForEach(clip => clip.SetColor(Color.White));
                            scene._draggedPuppet = origin;
                        }
                    };
                    var nextId = scene._puppetList.Keys.DefaultIfEmpty(0).Max() + 1;
                    scene._puppetList.Add(nextId, droppedPuppet);
                    scene._draggedPuppet = Puppet.EmptyPuppet;
                }
            }, Rectangle.Empty);

            var paperSheet = scene.GetPuppet(id - 1);
            paperSheet._clickRectangle = new Rectangle(260, 30, 1400, 1020);
            paperSheet._isAcceptingDrops = true;
            return id;
        }
        private static int AddHeroDeckToScene(CardGame game, Scene scene, int id)
        {
            int x = 80, y = 180;
            var puppetMap = new Dictionary<CardTypeEnum, PuppetEnum>
            {
                { CardTypeEnum.HeroMelee, PuppetEnum.HeroMelee },
                { CardTypeEnum.HeroMeleeCard, PuppetEnum.HeroMeleeCard },
                { CardTypeEnum.HeroProjectile, PuppetEnum.HeroProjectile },
                { CardTypeEnum.HeroArtillery, PuppetEnum.HeroArtillery }
            };

            foreach (var cardInDeck in game._hero._cardDeck)
            {
                if (puppetMap.TryGetValue(cardInDeck._cardType, out var puppetType))
                {
                    AddHeroCard(cardInDeck, puppetType, x, y, scene, ref id);
                }
                y += 100;
            }
            return id;
        }
        private static int AddTerrainToScene(CardGame game, Scene scene, Vector2 mapOffset, int id)
        {
            // Terrain mapping
            var terrainMap = new Dictionary<TerrainTypeEnum, PuppetEnum>
            {
                { TerrainTypeEnum.Ditch, PuppetEnum.TerrainDitch },
                { TerrainTypeEnum.Bush, PuppetEnum.TerrainBush },
                { TerrainTypeEnum.Hill, PuppetEnum.TerrainHill }
            };

            foreach (var terrain in game._campaign._battle._map._terrain)
            {
                if (terrainMap.TryGetValue(terrain._terrainType, out var puppetType))
                {
                    scene.AddPuppet(id++, puppetType, new Vector2(terrain._x + 260, terrain._y + 30) + mapOffset, 0, Puppet.EmptyAction, Rectangle.Empty);
                }
            }

            return id;
        }
        private static void AddHeroCard(Card card, PuppetEnum puppetType, int x, int y, Scene scene, ref int id)
        {
            scene.AddPuppet(id++, puppetType, new Vector2(x, y), 0, originPuppet =>
            //Add pickup action to the card so it can be dragged onto the papersheet
            {
                if (scene._draggedPuppet == Puppet.EmptyPuppet)
                {
                    Globals._testClick.Play(0.4f, 0.0f, 0.0f);
                    scene._draggedPuppet = PuppetFactory.CreatePuppet(puppetType, new Vector2(x, y), 0, Puppet.EmptyAction, Rectangle.Empty);
                }
            }, Rectangle.Empty);

            var clip = scene.GetPuppet(id - 1).GetFirstClip(ClipCategoryEnum.NameTag);
            clip.SetText(card._name);
            var size = clip.getSize();
            clip.SetOrigin(new Vector2(size.X / 2, -36));
            scene.GetPuppet(id - 1).MakeBoundingRectangle();
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
