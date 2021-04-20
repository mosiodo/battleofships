using BattleShips.Application;
using BattleShips.Model;
using NUnit.Framework;

namespace BattleShipsTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test()]
        public void AddNewShip_horizental_Success()
        {
            var result =  Board.Add(new Coordinate{ X = 0,Y = 3}, new Coordinate{X=0,Y=5});
            Assert.IsTrue(result.Valid);
        }
        
        
        [Test]
        public void AddNewShip_horizental_fails_if_add_vertical()
        {
            Board.Add(new Coordinate{ X = 0,Y = 3}, new Coordinate{X=0,Y=5});
            
            var result =  Board.Add(new Coordinate{ X = 2,Y = 8}, new Coordinate{X=6,Y=8});

            Assert.IsFalse(result.Valid);
        }
        
        [Test]
        public void AddNewShip_horizental_is_overlaped_fails()
        {
            Board.Add(new Coordinate{ X = 0,Y = 3}, new Coordinate{X=0,Y=5});
            var result =  Board.Add(new Coordinate{ X = 0,Y = 4}, new Coordinate{X=0,Y=7});

            Assert.IsFalse(result.Valid);
        }
    }
}