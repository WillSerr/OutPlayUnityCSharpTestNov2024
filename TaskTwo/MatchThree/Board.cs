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
            Move bestMove = new Move();

            // Implement this function
            for (int x = 0; x < GetWidth(); ++x)
            {
                for (int y = 0; y < GetHeight(); ++y)
                {
                    
                    

                    Vector2 position = new Vector2(x, y);

                    Vector2 forward = new Vector2(1, 0);

                    Vector2 up = new Vector2(0, 1);

                    //Vector2 down = new Vector2(0, 1);
                    //for (int i = 0; i < 2; ++i) // check + x and + y
                    //{

                    JewelKind rootJewel = GetJewel(x, y);
                    Vector2 swappedPeicePosition = (position + forward);
                    int moveValue = 0;

                    //Check forward peices within game board
                    if (swappedPeicePosition.X < 0 || swappedPeicePosition.X > GetWidth() || swappedPeicePosition.Y < 0 || swappedPeicePosition.Y > GetHeight())
                    {
                        continue;
                    }

                    //Repeat twice to check both swapped peices contribution to the move value
                    for (int g = 0; g < 2; ++g) {

                        //Check forward peices within game board
                        Vector2 threeSpacesForward = (position + (forward * 3));
                        if (threeSpacesForward.X > 0 && threeSpacesForward.X < GetWidth())
                        {
                            if (threeSpacesForward.Y > 0 && threeSpacesForward.Y < GetHeight())
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

                        //Peices perpendicular to forward
                        JewelKind[] perpPeices = { JewelKind.Empty, JewelKind.Empty, rootJewel, JewelKind.Empty, JewelKind.Empty };

                        for (int i = 0; i < perpPeices.Length; ++i)
                        {
                            Vector2 checkPos = (position + forward + (up * (2 - i)));

                            if (checkPos.X > 0 && checkPos.X < GetWidth())
                            {
                                if (checkPos.Y > 0 && checkPos.Y < GetHeight())
                                {
                                    perpPeices[i] = GetJewel((int)checkPos.X, (int)checkPos.Y);
                                }
                            }
                        }

                        int chain = 0;
                        for (int i = 0; i < perpPeices.Length; ++i)
                        {
                            if (perpPeices[i] == rootJewel)
                            {
                                chain++;
                            }
                            else if (chain < 3)
                            {
                                chain = 0;
                            }
                        }

                        if (chain > 3)
                        {
                            moveValue += chain - 1;
                        }

                        //Add back in the value of the peice swapped being removed
                        if (moveValue > 0)
                        {
                            moveValue += 1;
                        }

                        //Repeat for the jewel being moved from forward into current position
                        position += forward;
                        forward *= -1;
                    }

                    //If the moveValue > bestMoveValue, update bestMove and bestMoveValue
                    if(moveValue > bestMoveValue)
                    {
                        bestMoveValue = moveValue;
                        bestMove.x = x; 
                        bestMove.y = y;

                        //Assuming positive directions are up and right
                        if (forward.X > 0)
                        {
                            bestMove.direction = MoveDirection.Right;
                        }
                        else
                        {
                            bestMove.direction = MoveDirection.Up;
                        }
                    }


                    //Matrix transform the forward, up and down vectors 90 degrees and repeat until all directions(available moves) are checked

                    //}



                }
            }


            return new Move();
        }


    }
}
