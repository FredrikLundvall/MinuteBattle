using MinuteBattle.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinuteBattleTests
{
    public class MapTests
    {
        [Test]
        public void WhenMapIsCreated_ThreeDifferentTerainsAreAdded()
        {
            Map map = Map.CreateMap(1000, 1000, new());
            Assert.That(map._terrain.Any(it => it._terrainType == TerrainTypeEnum.Ditch), Is.True);
            Assert.That(map._terrain.Any(it => it._terrainType == TerrainTypeEnum.Bush), Is.True);
            Assert.That(map._terrain.Any(it => it._terrainType == TerrainTypeEnum.Hill), Is.True);
        }
    }
}
