using System.ComponentModel.DataAnnotations;

namespace BattleShips.Model
{
    public class Coordinate
    {
        [Range(0,9)]
        public int X { get; set; }

        [Range(0,9)]
        public int Y { get; set; }
    }
}