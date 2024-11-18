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
        public Board(int width, int height)
        {
            boardWidth = width;
            boardHeight = height;
            gameBoard = new JewelKind[width, height];

            Random rand = new Random();

            
            //Populate the game board
            for (int x = 0; x < GetWidth(); ++x)
            {
                for (int y = 0; y < GetHeight(); ++y)
                {
                    bool invalidPlacement = true;

                    //No Matches allowed in initial board
                    while (invalidPlacement)
                    {
                        JewelKind placedJewel = (JewelKind)rand.Next(1,8);
                        gameBoard[x, y] = placedJewel;
                        invalidPlacement = false;

                        if (x - 2 > 0)
                        {
                            if(gameBoard[x -2, y] == placedJewel && gameBoard[x - 1, y] == placedJewel)
                            {
                                invalidPlacement = true;
                            }
                        }
                        if (y - 2 > 0)
                        {
                            if (gameBoard[x, y - 2] == placedJewel && gameBoard[x, y - 1] == placedJewel)
                            {
                                invalidPlacement = true;
                            }
                        }
                    }
                }
            }
        }

        public void DrawBoard()
        {

            //Populate the game board
            for (int y = GetHeight()-1; y >= 0 ; --y)
            {
                string line = "[";
                for (int x = 0; x < GetWidth(); ++x)
                {
                    switch (gameBoard[x, y])
                    {
                        case JewelKind.Empty:
                            line += " ? ";
                            break;
                        case JewelKind.Red:
                            line += " R ";
                            break;
                        case JewelKind.Orange:
                            line += " O ";
                            break;
                        case JewelKind.Yellow:
                            line += " Y ";
                            break;
                        case JewelKind.Green:
                            line += " G ";
                            break;
                        case JewelKind.Blue:
                            line += " B ";
                            break;
                        case JewelKind.Indigo:
                            line += " I ";
                            break;
                        case JewelKind.Violet:
                            line += " V ";
                            break;
                    }
                }
                line += "]";
                Console.WriteLine(line);
            }
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

        public enum MoveDirection
        {
            Up,
            Down,
            Left,
            Right
        }

        public struct Move
        {
            public int x;
            public int y;
            public MoveDirection direction;
        }

        int GetWidth() { return boardWidth; }

        int GetHeight() { return boardHeight; }

        JewelKind GetJewel(int x, int y) { return gameBoard[x,y]; }

        void SetJewel(int x, int y, JewelKind kind) { gameBoard[x, y] = kind; }

        public Move CalculateBestMoveForBoard()
        {
            int bestMoveValue = 0;
            Move bestMove = new Move();

            //Traverse the whole game board
            for (int x = 0; x < GetWidth(); ++x)
            {
                for (int y = 0; y < GetHeight(); ++y)
                {
                    
                    

                    Vector2 position = new Vector2(x, y);

                    Vector2 forward = new Vector2(1, 0);

                    Vector2 up = new Vector2(0, 1);


                    //We are checking the right (+x) and up (+y) moves of every peice 
                    for (int h = 0; h < 2; ++h) 
                    {

                    JewelKind rootJewel = GetJewel(x, y);
                    Vector2 swappedPeicePosition = (position + forward);
                    int moveValue = 0;

                    //Check forward peices within game board
                    if (swappedPeicePosition.X < 0 || swappedPeicePosition.X > GetWidth() || swappedPeicePosition.Y < 0 || swappedPeicePosition.Y > GetHeight())
                    {
                        continue;
                    }

                    //Repeat twice to check both swapped peices' contribution to the move value
                    for (int g = 0; g < 2; ++g) {

                        //Check forward peices within game board
                        Vector2 threeSpacesForward = (position + (forward * 3));
                        if (threeSpacesForward.X >= 0 && threeSpacesForward.X < GetWidth())
                        {
                            if (threeSpacesForward.Y >= 0 && threeSpacesForward.Y < GetHeight())
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
                        JewelKind[] perpPeices = { JewelKind.Empty, JewelKind.Empty, JewelKind.Empty, JewelKind.Empty, JewelKind.Empty };

                        for (int i = 0; i < perpPeices.Length; ++i)
                        {
                            Vector2 checkPos = (position + forward + (up * (2 - i)));

                            if (checkPos.X >= 0 && checkPos.X < GetWidth())
                            {
                                if (checkPos.Y >= 0 && checkPos.Y < GetHeight())
                                {
                                    perpPeices[i] = GetJewel((int)checkPos.X, (int)checkPos.Y);
                                }
                            }
                        }

                        //Make ot work on post swap data
                        perpPeices[2] = rootJewel;

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


                    //Rotate to point towards up vector as we are checking the right and up moves of every peice 
                    Vector2 temp = forward;
                    forward = up;
                    up = -temp;

                    }



                }
            }


            return bestMove;
        }


    }
}
