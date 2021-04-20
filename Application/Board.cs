using System;
using BattleShips.Model;

namespace BattleShips.Application
{
    public class Board
    {
        private static byte[,] _board = new byte[10, 10];
        private static Orientation _orientation = Orientation.None;

        public static void Reset()
        {
            _board = new byte[10, 10];
        }

        public static Result Add(Coordinate start, Coordinate end)
        {
            if (IsValid(start, end) == false)
                return new Result("Coordinate is not valid.");

            if (IsOrientationValid(start, end) == false)
                return new Result($"Ship orientation cannot be changed. Current orientation is {_orientation.ToString()}");
            

            if (start.X == end.X)
            {
                // hori
                for (var i = start.Y; i <= end.Y; i++)
                {
                    _board[start.X, i] = 1;
                }
                _orientation = Orientation.Horizental;
            }

            if (start.Y == end.Y)
            {
                // vertical
                for (int i = start.X; i <= end.X; i++)
                {
                    _board[i, start.Y] = 1;
                }
                _orientation = Orientation.Vertical;
            }

            return new Result();
        }

        public static bool Hit(Coordinate coordinate)
        {
            return _board[coordinate.X, coordinate.Y] == 1;
        }

        private static bool IsOrientationValid(Coordinate start, Coordinate end)
        {
            if (_orientation == Orientation.None)
                return true;

            if (start.X == end.X && start.Y == end.Y)
            {
                return true;
            }

            if (start.X != end.X && start.Y != end.Y)
            {
                return false;
            }

            if (start.X == end.X && _orientation == Orientation.Vertical)
            {
                return false;
            }
            return start.Y != end.Y || _orientation != Orientation.Horizental;
        }

        private static bool IsValid(Coordinate start, Coordinate end)
        {
            if (start.X > 9 || start.Y > 9 || end.X > 9 || end.Y > 9)
                return false;

            if (start.X == end.X)
            {
                // hori
                for (var i = start.Y ; i <= end.Y; i++)
                {
                    if (_board[start.X, i] == 1)
                        return false;
                }
            }

            if (start.Y == end.Y)
            {
                // vertical
                for (int i = start.X; i <= end.X; i++)
                {
                    if (_board[i, start.Y] == 1)
                        return false;
                }
            }

            return true;
        }
    }

    public class Result
    {
        public Result() : this(string.Empty)
        {
        }
        public Result(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
        public string ErrorMessage { get; }

        public bool Valid => string.IsNullOrEmpty(ErrorMessage) == false;
    }

    public enum Orientation
    {
        None,
        Horizental,
        Vertical
    }
}
