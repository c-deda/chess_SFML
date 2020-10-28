using Chess.GameLogic;
using Chess.Systems;
using SFML.Graphics;

namespace Chess.UI
{
    class BoardUI
    {
        public SquareUI[,] SquareUI { get; private set; }
        private Sprite _boardBackground;
        private bool _mirrored;
        public bool SquareIsSelected { get; private set; }
        public Position Selection { get; private set; }

        public BoardUI(Board board)
        {
            // Board Texture
            _boardBackground = new Sprite(Application.Instance().AssetManager.Textures[TextureID.BoardBackground]);

            // Board Squares
            SquareUI = new SquareUI[GlobalConstants.BoardLength,GlobalConstants.BoardLength];

            for (int y = 0; y < GlobalConstants.BoardLength; ++y)
            {
                for (int x = 0; x < GlobalConstants.BoardLength; ++x)
                {
                    if (y % 2 == 0)
                    {
                        // Dark Squares
                        if (x % 2 == 0)
                        {
                            SquareUI[x,y] = new SquareUI(new Position(x, y), board.GetPieceAt(x,y), true);
                        }
                        // Light Squares
                        else
                        {
                            SquareUI[x,y] = new SquareUI(new Position(x, y), board.GetPieceAt(x,y), false);
                        }
                    }
                    else 
                    {
                        // Light Squares
                        if (x % 2 == 0)
                        {
                            SquareUI[x,y] = new SquareUI(new Position(x, y), board.GetPieceAt(x,y), false);
                        }
                        // Dark Squares
                        else
                        {
                            SquareUI[x,y] = new SquareUI(new Position(x, y), board.GetPieceAt(x,y), true);
                        }
                    }
                }
            }
            // Board Is Always Mirrored To Start
            _mirrored = true;
            MirrorBoard();
        }
        public void MirrorBoard()
        {
            for (int y = 0; y < GlobalConstants.BoardLength; ++y)
            {
                for (int x = 0; x < GlobalConstants.BoardLength; ++x)
                {
                    if (_mirrored)
                    {
                        SquareUI[x,y].MirrorSquare(new Position(x, (GlobalConstants.BoardLength - 1) - y));
                    }
                    else
                    {
                        SquareUI[x,y].MirrorSquare(new Position(x, y));
                    }
                }
            }

            _mirrored = !_mirrored;
        }
        public void HighlightSquares(Board board)
        {
            for (int y = 0; y < GlobalConstants.BoardLength; ++y)
            {
                for (int x = 0; x < GlobalConstants.BoardLength; ++x)
                {
                    if (!SquareIsSelected)
                    {
                        SquareUI[x,y].Unselect();
                    }
                    else if (Selection.X == x && Selection.Y == y ||
                             board.GetPieceAt(Selection.X, Selection.Y).HasMove(new Position(x, y)))
                    {
                        SquareUI[x,y].Select();
                    }
                    else
                    {
                        SquareUI[x,y].Unselect();
                    }
                }
            }
        }
        public void SelectSquare(Position position)
        {
            SquareIsSelected = true;
            Selection = position;
        }
        public void UnselectSquare()
        {
            SquareIsSelected = false;
        }
        public void Draw()
        {
            // Background
            Application.Instance().MainWindow.Draw(_boardBackground);

            // Squares
            for (int y = 0; y < GlobalConstants.BoardLength; ++y)
            {
                for (int x = 0; x < GlobalConstants.BoardLength; ++x)
                {
                    SquareUI[x,y].Draw();
                }
            }
        }
        public void Update(Board board)
        {
            for (int y = 0; y < GlobalConstants.BoardLength; ++y)
            {
                for (int x = 0; x < GlobalConstants.BoardLength; ++x)
                {
                    SquareUI[x,y].UpdateSprite(board.GetPieceAt(x,y));
                }
            }
        }
    }
}