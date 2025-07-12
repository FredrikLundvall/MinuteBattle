using System;
using System.Collections.Generic;

namespace MinuteBattle.Logic
{
    public class Map
    {
        public int _width;
        public int _height;
        public List<Terrain> _terrain = [];

        public static Map CreateMap(int width, int height, Random rnd)
        {
            Map map = new();
            map._width = width;
            map._height = height;
            map.Add(new(rnd.Next(0, width), rnd.Next(0, height), rnd.Next(1, 10), TerrainTypeEnum.Ditch));
            map.Add(new(rnd.Next(0, width), rnd.Next(0, height), rnd.Next(1, 10), TerrainTypeEnum.Hill));
            map.Add(new(rnd.Next(0, width), rnd.Next(0, height), rnd.Next(1, 10), TerrainTypeEnum.Bush));
            return map;
        }
        public void Add(Terrain terrain)
        {
            _terrain.Add(terrain);
        }
    }
}
