using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace MatchThree
{
    public class Board
    {
        Board(int width, int height) 
        {
            boardWidth = width;
            boardHeight = height;
            gameBoard = new JewelKind[width, height];
        }


        private int boardWidth;

        private int boardHeight;

        private JewelKind[,] gameBoard;

        enum JewelKind
        {
            Empty,
            Red,
            Orange,
            Yellow,
            Green,
            Blue,
            Indigo,
            Violet
        }

        enum MoveDirection
        {
            Up,
            Down,
            Left,
            Right
        }

        struct Move
        {
            public int x;
            public int y;
            public MoveDirection direction;
        }

        int GetWidth() { return boardWidth; }

        int GetHeight() { return boardHeight; }

        JewelKind GetJewel(int x, int y) { return gameBoard[x,y]; }

        void SetJewel(int x, int y, JewelKind kind) { gameBoard[x, y] = kind; }

        Move CalculateBestMoveForBoard()
        {
            int bestMoveValue = 0;
            Move bestMove;

            // Implement this function
            for (int x = 0; x < GetWidth(); ++x)
            {
                for (int y = 0; y < GetHeight(); ++y)
                {
                    JewelKind rootJewel = GetJewel(x, y);
                    int moveValue = 0;

                    Vector2 position = new Vector2(x, y);

                    Vector2 forward = new Vector2(1, 0);

                    Vector2 up = new Vector2(0, -1);

                    Vector2 down = new Vector2(0, 1);
                    //for (int i = 0; i < 4; ++i)
                    //{

                    //Check forward peices within game board
                    if ((position + (forward*3)).X > 0 && (position + (forward * 3)).X < GetWidth())
                    {
                        if ((position + (forward * 3)).Y > 0 && (position + (forward * 3)).Y < GetHeight())
                        {

                            if (GetJewel((int)(position + (forward * 3)).X, (int)(position + (forward * 3)).Y) == rootJewel)
                            {
                                if (GetJewel((int)(position + (forward * 2)).X, (int)(position + (forward * 2)).Y) == rootJewel)
                                {
                                    moveValue += 2;
                                }
                            }

                        }
                    }

                    //Check upwards peices within game board
                    if ((position + forward + (up * 2)).X > 0 && (position + forward + (up * 2)).X < GetWidth())
                    {
                        if ((position + forward + (up * 2)).Y > 0 && (position + forward + (up * 2)).Y < GetHeight())
                        {

                            if (GetJewel((int)(position + forward + (up * 2)).X, (int)(position + forward + (up * 2)).Y) == rootJewel)
                            {
                                if (GetJewel((int)(position + forward + up).X, (int)(position + forward + up).Y) == rootJewel)
                                {
                                    moveValue += 2;
                                }
                            }

                        }
                    }

                    //Check upwards peices within game board
                    if ((position + forward + (down * 2)).X > 0 && (position + forward + (down * 2)).X < GetWidth())
                    {
                        if ((position + forward + (down * 2)).Y > 0 && (position + forward + (down * 2)).Y < GetHeight())
                        {

                            if (GetJewel((int)(position + forward + (down * 2)).X, (int)(position + forward + (down * 2)).Y) == rootJewel)
                            {
                                if (GetJewel((int)(position + forward + down).X, (int)(position + forward + down).Y) == rootJewel)
                                {
                                    moveValue += 2;
                                }
                            }

                        }
                    }

                    //Repeat for the jewel being moved from forward into current position

                    //If the moveValue > bestMoveValue, update bestMove and bestMoveValue

                    //Matrix transform the forward, up and down vectors 90 degrees and repeat until all directions(available moves) are checked

                    //}



                }
            }


            return new Move();
        }


    }
}
