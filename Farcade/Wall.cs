using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.Farcade
{
    public class Wall
    {
        private List<string> availableColors = new List<string> { "Red", "Blue", "Green", "Yellow" };

        public string Color { get; set; }

        public Wall(string color)
        {
            if(!availableColors.Contains(color)) throw new Exception("Color not available");
            Color = color;
        }
    }
}
