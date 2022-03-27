using ChessLib;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChessMoving
{
    public class ComplexGame
    {
        private readonly Random _rnd = new Random();
        private List<IPiece> Pieces { get; set; }

        public void Setup()
        {
            // TODO: Set up the state of the game here
            var knight = new Knight(new Position(1, 1));
            var pawn = new Pawn(new Position(2, 1));
            var bishop = new Bishop(new Position(3, 1));
            var queen = new Queen(new Position(4, 1));

            this.Pieces = new List<IPiece>()
            {
                knight,
                pawn,
                bishop,
                queen
            };
        }

        public void Play(int moves)
        {
            // TODO: Play the game moves here
            for (var move = 1; move <= moves; move++)
            {
                var piece = this.Pieces[_rnd.Next(this.Pieces.Count)];
                var occupied = this.Pieces.Select(p => p.Current);

                var possibleMoves = piece.ValidMovesFor(occupied).ToArray();
                var pos = possibleMoves[_rnd.Next(possibleMoves.Length)];
                piece.Move(pos);

                Console.WriteLine("{0}: {1} position is {2}", move, piece.Name, pos);
            }
        }
    }

    public interface IPiece
    {
        public string Name { get; }
        public Position Current { get; }
        public IEnumerable<Position> ValidMovesFor(IEnumerable<Position> occupied);
        public void Move(Position target);
    }

    public abstract class BasePiece : IPiece
    {
        public BasePiece(Position start)
        {
            this.Current = start;
        }

        public virtual string Name => this.GetType().Name;

        public Position Current { get; protected set; }

        public abstract IEnumerable<Position> ValidMovesFor(IEnumerable<Position> occupied);

        public virtual void Move(Position target)
        {
            this.Current = target;
        }

        protected bool ValidBoardBoundaries(int x, int y)
        {
            return x >= 1 && x <= 8 && y >= 1 && y <= 8;
        }
    }

    public class Knight : BasePiece
    {
        public Knight(Position start) : base(start)
        {
            this._knight = new KnightMove();
        }

        private readonly KnightMove _knight;

        public override IEnumerable<Position> ValidMovesFor(IEnumerable<Position> occupied)
        {
            var possibleMoves = this._knight.ValidMovesFor(this.Current).ToArray();

            foreach (var pos in possibleMoves)
            {
                if (occupied != null && occupied.Any(p => p.Equals(pos))) continue;

                yield return pos;
            }
        }
    }

    public class Pawn : BasePiece
    {
        public Pawn(Position start) : base(start)
        {
        }

        private bool _isFirst = true;
        public static readonly int[,] Moves = new[,] { { 1, 0 }, { -1, 0 }, { 0, 1 }, { 0, -1 }, { 2, 0 }, { -2, 0 }, { 0, 2 }, { 0, -2 } };

        public override IEnumerable<Position> ValidMovesFor(IEnumerable<Position> occupied)
        {
            var len = _isFirst ? 8 : 4;
            for (var i = 0; i < len; i++)
            {
                var newX = this.Current.X + Moves[i, 0];
                var newY = this.Current.Y + Moves[i, 1];

                if (this.ValidBoardBoundaries(newX, newY) == false) continue;

                if (occupied != null && occupied.Any(p => p.X == newX && p.Y == newY)) continue;

                yield return new Position(newX, newY);
            }
        }

        public override void Move(Position target)
        {
            this._isFirst = false;
            base.Move(target);
        }
    }

    public class Bishop : BasePiece
    {
        public Bishop(Position start) : base(start)
        {
        }

        //4个方向
        public static readonly int[,] Moves = new[,] { { 1, 1 }, { 1, -1 }, { -1, 1 }, { -1, -1 } };

        public override IEnumerable<Position> ValidMovesFor(IEnumerable<Position> occupied)
        {
            for (var i = 0; i <= Moves.GetUpperBound(0); i++)
            {
                for (var j = 1; j < 8; j++)
                {
                    var newX = this.Current.X + Moves[i, 0] * j;
                    var newY = this.Current.Y + Moves[i, 1] * j;

                    if (this.ValidBoardBoundaries(newX, newY) == false) continue;

                    if (occupied != null && occupied.Any(p => p.X == newX && p.Y == newY)) continue;

                    yield return new Position(newX, newY);
                }
            }
        }
    }

    public class Queen : BasePiece
    {
        public Queen(Position start) : base(start)
        {
        }

        //8个方向
        public static readonly int[,] Moves = new[,] { { 1, 1 }, { 1, -1 }, { -1, 1 }, { -1, -1 }, { 1, 0 }, { -1, 0 }, { 0, 1 }, { 0, -1 } };

        public override IEnumerable<Position> ValidMovesFor(IEnumerable<Position> occupied)
        {
            for (var i = 0; i <= Moves.GetUpperBound(0); i++)
            {
                for (var j = 1; j < 8; j++)
                {
                    var newX = this.Current.X + Moves[i, 0] * j;
                    var newY = this.Current.Y + Moves[i, 1] * j;

                    if (this.ValidBoardBoundaries(newX, newY) == false) continue;

                    if (occupied != null && occupied.Any(p => p.X == newX && p.Y == newY)) continue;

                    yield return new Position(newX, newY);
                }
            }
        }
    }


}
