using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinuteBattle.Logic
{
    public class Terrain
    {
        public int _x;
        public int _y;
        public int _size;
        public TerrainTypeEnum _terrainType;
        public Terrain(int x, int y, int size, TerrainTypeEnum terrainType)
        {
            _x = x;
            _y = y;
            _size = size;
            _terrainType = terrainType;
        }
    }
}
