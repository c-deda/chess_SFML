using Chess.GameLogic.Pieces;
using Chess.GameLogic;
using Chess.Systems;
using SFML.Graphics;

namespace Chess.UI
{
    class BoardUI
    {
        public SquareUI[,] squaresUI { get; private set; }
        private Sprite boardBackground;
        public bool squareIsSelected { get; private set; }
        public Position selection { get; private set; }

        public BoardUI(Board board)
        {
            // Board Texture
            boardBackground = new Sprite(GameClient.Instance().assetManager.textures[TextureID.BoardBackground]);

            // Board Squares
            squaresUI = new SquareUI[GlobalConstants.BoardLength,GlobalConstants.BoardLength];

            for (int y = 0; y < GlobalConstants.BoardLength; ++y)
            {
                for (int x = 0; x < GlobalConstants.BoardLength; ++x)
                {
                    if (y % 2 == 0)
                    {
                        // Dark Squares
                        if (x % 2 == 0)
                        {
                            squaresUI[x,y] = new SquareUI(new Position(x, y), board.pieces[x,y], true);
                        }
                        // Light Squares
                        else
                        {
                            squaresUI[x,y] = new SquareUI(new Position(x, y), board.pieces[x,y], false);
                        }
                    }
                    else 
                    {
                        // Light Squares
                        if (x % 2 == 0)
                        {
                            squaresUI[x,y] = new SquareUI(new Position(x, y), board.pieces[x,y], false);
                        }
                        // Dark Squares
                        else
                        {
                            squaresUI[x,y] = new SquareUI(new Position(x, y), board.pieces[x,y], true);
                        }
                    }
                }
            }

            MirrorBoard();
        }
        public void MirrorBoard()
        {
            for (int y = 0; y < GlobalConstants.BoardLength; ++y)
            {
                for (int x = 0; x < GlobalConstants.BoardLength; ++x)
                {
                    squaresUI[x,y].MirrorSquare(new Position(x, (GlobalConstants.BoardLength - 1) - y));
                }
            }
        }
        public void HighlightSquares(Board board)
        {
            for (int y = 0; y < GlobalConstants.BoardLength; ++y)
            {
                for (int x = 0; x < GlobalConstants.BoardLength; ++x)
                {
                    if (!squareIsSelected)
                    {
                        squaresUI[x,y].Unselect();
                    }
                    else if (selection.x == x && selection.y == y ||
                             board.pieces[selection.x, selection.y].HasMove(new Position(x, y)))
                    {
                        squaresUI[x,y].Select();
                    }
                    else
                    {
                        squaresUI[x,y].Unselect();
                    }
                }
            }
        }
        public void SelectSquare(Position position)
        {
            squareIsSelected = true;
            selection = position;
        }
        public void UnselectSquare()
        {
            squareIsSelected = false;
        }
        public void Draw()
        {
            // Background
            GameClient.Instance().mainWindow.Draw(boardBackground);

            // Squares
            for (int y = 0; y < GlobalConstants.BoardLength; ++y)
            {
                for (int x = 0; x < GlobalConstants.BoardLength; ++x)
                {
                    squaresUI[x,y].Draw();
                }
            }
        }
        public void Update(Board board)
        {
            for (int y = 0; y < GlobalConstants.BoardLength; ++y)
            {
                for (int x = 0; x < GlobalConstants.BoardLength; ++x)
                {
                    squaresUI[x,y].UpdateSprite(board.pieces[x,y]);
                }
            }
        }
    }
}