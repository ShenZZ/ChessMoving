namespace ChessMoving.Test
{
    using ChessLib;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    [TestClass]
    public class TestAnswerFixture
    {
        // TODO: add additional tests for your answer
        [TestMethod]
        public void TestKnightFromInsideBoard()
        {
            var pos = new Position(3, 3);
            var knight = new Knight(pos);

            var moves = knight.ValidMovesFor(null).ToArray();

            Assert.IsNotNull(moves);
            Assert.AreEqual(8, moves.Length);

            foreach (var move in moves)
            {
                switch (Math.Abs(move.X - pos.X))
                {
                    case 1:
                        Assert.AreEqual(2, Math.Abs(move.Y - pos.Y));
                        break;
                    case 2:
                        Assert.AreEqual(1, Math.Abs(move.Y - pos.Y));
                        break;
                    default:
                        Assert.Fail();
                        break;
                }
            }
        }

        [TestMethod]
        public void TestKnightFromCorner()
        {
            var pos = new Position(1, 1);
            var knight = new Knight(pos);

            var moves = new HashSet<Position>(knight.ValidMovesFor(null));

            Assert.IsNotNull(moves);
            Assert.AreEqual(2, moves.Count);

            var possibles = new[] { new Position(2, 3), new Position(3, 2) };

            foreach (var possible in possibles)
            {
                Assert.IsTrue(moves.Contains(possible));
            }
        }

        [TestMethod]
        public void TestPawnFromInsideBoard()
        {
            var pos = new Position(4, 4);
            var pawn = new Pawn(pos);

            var moves = pawn.ValidMovesFor(null).ToArray();

            Assert.IsNotNull(moves);
            Assert.AreEqual(8, moves.Length);

            foreach (var move in moves)
            {
                if (move.X == pos.X)
                {
                    var len = Math.Abs(move.Y - pos.Y);
                    if (len != 1 && len != 2) Assert.Fail();
                }
                else if (move.Y == pos.Y)
                {
                    var len = Math.Abs(move.X - pos.X);
                    if (len != 1 && len != 2) Assert.Fail();
                }
                else
                {
                    Assert.Fail();
                }
            }

            pos = moves[0];
            pawn.Move(pos);
            moves = pawn.ValidMovesFor(null).ToArray();

            Assert.IsNotNull(moves);
            Assert.AreEqual(4, moves.Length);

            foreach (var move in moves)
            {
                if (move.X == pos.X)
                {
                    Assert.AreEqual(1, Math.Abs(move.Y - pos.Y));
                }
                else if (move.Y == pos.Y)
                {
                    Assert.AreEqual(1, Math.Abs(move.X - pos.X));
                }
                else
                {
                    Assert.Fail();
                }
            }
        }

        [TestMethod]
        public void TestPawnFromCorner()
        {
            var pos = new Position(1, 1);
            var pawn = new Pawn(pos);

            var moves = new HashSet<Position>(pawn.ValidMovesFor(null));

            Assert.IsNotNull(moves);
            Assert.AreEqual(4, moves.Count);

            var positions = new[] { new Position(1, 2), new Position(1, 3), new Position(2, 1), new Position(3, 1) };

            foreach (var possible in positions)
            {
                Assert.IsTrue(moves.Contains(possible));
            }
        }

        [TestMethod]
        public void TestBishopFromInsideBoard()
        {
            var pos = new Position(3, 3);
            var bishop = new Bishop(pos);

            var moves = bishop.ValidMovesFor(null).ToArray();

            Assert.IsNotNull(moves);
            Assert.AreEqual(11, moves.Length);

            foreach (var move in moves)
            {
                Assert.AreEqual(Math.Abs(move.X - pos.X), Math.Abs(move.Y - pos.Y));
            }
        }

        [TestMethod]
        public void TestBishopFromCorner()
        {
            var pos = new Position(1, 1);
            var bishop = new Bishop(pos);

            var moves = new HashSet<Position>(bishop.ValidMovesFor(null));

            Assert.IsNotNull(moves);
            Assert.AreEqual(7, moves.Count);

            var possibles = new[] { new Position(3, 3), new Position(7, 7) };

            foreach (var possible in possibles)
            {
                Assert.IsTrue(moves.Contains(possible));
            }
        }

        [TestMethod]
        public void TestQueenFromInsideBoard()
        {
            var pos = new Position(3, 3);
            var queen = new Queen(pos);

            var moves = queen.ValidMovesFor(null).ToArray();

            Assert.IsNotNull(moves);
            Assert.AreEqual(25, moves.Length);

            foreach (var move in moves)
            {
                if (pos.X == move.X)
                {
                    Assert.AreNotEqual(pos.Y, move.Y);
                }
                else if (pos.Y == move.Y)
                {
                    Assert.AreNotEqual(pos.X, move.X);
                }
                else
                {
                    Assert.AreEqual(Math.Abs(move.X - pos.X), Math.Abs(move.Y - pos.Y));
                }
            }
        }

        [TestMethod]
        public void TestQueenFromCorner()
        {
            var pos = new Position(1, 1);
            var queen = new Queen(pos);

            var moves = new HashSet<Position>(queen.ValidMovesFor(null));

            Assert.IsNotNull(moves);
            Assert.AreEqual(21, moves.Count);

            var possibles = new[] { new Position(3, 3), new Position(7, 7), new Position(7, 1), new Position(1, 8) };

            foreach (var possible in possibles)
            {
                Assert.IsTrue(moves.Contains(possible));
            }
        }
    }
}