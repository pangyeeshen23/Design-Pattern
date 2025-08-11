using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.Farcade
{
    public class House
    {
        private List<Wall> _walls;

        public House()
        {
            _walls = new List<Wall>();
        }

        /// This is an example of a facade pattern because we simplied a process of a building a level
        public void BuildALevel()
        {
            _walls.Add(new Wall("Red"));
            _walls.Add(new Wall("Blue"));
            _walls.Add(new Wall("Brown"));
            _walls.Add(new Wall("Green"));

        }

        public void BuildWall(string color)
        {
            _walls.Add(new Wall(color));
        }

        public IEnumerable<Wall> GetWalls() => _walls;
    }
}
