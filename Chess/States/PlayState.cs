using Chess.GameLogic;
using Chess.UI;
using Chess.Systems;
using SFML.Window;
using SFML.System;
using SFML.Graphics;

namespace Chess.States
{
    class PlayState : State
    {
        private const int ButtonWidth = 60;
        private const int ButtonHeight = 35;

        private Game _game;
        private BoardUI _boardUI;
        private UIContainer _gameUI;
        private MoveHistory moveHistory;

        public override void Init()
        {
            InitUI();
            ResetGame();
        }
        public void ResetGame()
        {
            _game = new Game();
            _boardUI = new BoardUI(_game.Board);
            moveHistory.Reset();
            _game.GameEvent += UpdateGameState;
        }
        private void InitUI()
        {
            // Initialize Root Object
            _gameUI = new UIContainer(new Vector2f(GlobalConstants.BoardLength * GlobalConstants.SquareSize, 0),
                                      new Vector2f(GlobalConstants.WindowWidth - GlobalConstants.BoardLength * GlobalConstants.SquareSize, 
                                      GlobalConstants.SquareSize * GlobalConstants.BoardLength), new Color(215, 215, 215));
            
            // Move History
            moveHistory = new MoveHistory(new Vector2f(_gameUI.Position.X, _gameUI.Position.Y + GlobalConstants.SquareSize), 
                                        new Vector2f(_gameUI.Shape.Size.X, _gameUI.Shape.Size.Y - (GlobalConstants.SquareSize * 2)));
            // Quit Button
            GraphicButton quitButton = new GraphicButton(
                    new Vector2f(_gameUI.Position.X + _gameUI.Shape.Size.X - ButtonWidth, _gameUI.Position.Y + _gameUI.Shape.Size.Y - ButtonHeight),
                    new Vector2f(ButtonWidth, ButtonHeight), new Color(100, 100, 100), new Color(150, 150, 150));
            quitButton.SetGraphics(Application.Instance().AssetManager.Textures[TextureID.ReturnIdle],
                                   Application.Instance().AssetManager.Textures[TextureID.ReturnHover]);
            quitButton.SetCommand(new GoToStateCommand(new MenuState()));
            quitButton.SetBorder(1, Color.Black);

            // New Game Button
            GraphicButton newGameButton = new GraphicButton(
                    new Vector2f(_gameUI.Position.X + _gameUI.Shape.Size.X - (ButtonWidth * 2), _gameUI.Position.Y + _gameUI.Shape.Size.Y - ButtonHeight),
                    new Vector2f(ButtonWidth, ButtonHeight), new Color(100, 100, 100), new Color(150, 150, 150));
            newGameButton.SetGraphics(Application.Instance().AssetManager.Textures[TextureID.NewGameIdle], 
                                      Application.Instance().AssetManager.Textures[TextureID.NewGameHover]);
            newGameButton.SetCommand(new ResetGameCommand(this));
            newGameButton.SetBorder(1, Color.Black);

            // Add Elements
            _gameUI.AddElement(moveHistory);
            _gameUI.AddElement(quitButton);
            _gameUI.AddElement(newGameButton);
        }
        public override void HandleInput(MouseButtonEventArgs buttonEventArgs)
        {
            if (Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                CheckForSquareClicked(buttonEventArgs);

                foreach (UIElement element in _gameUI.Elements)
                {
                    if (element.Contains(buttonEventArgs.X, buttonEventArgs.Y))
                    {
                        element.OnClick(buttonEventArgs.X, buttonEventArgs.Y);
                    }
                }
            }
        }
        public override void HandleInput(MouseWheelScrollEventArgs wheelEventArgs)
        {
            foreach (UIElement element in _gameUI.Elements)
            {
                if (element.Contains(wheelEventArgs.X, wheelEventArgs.Y))
                    {
                        element.OnScroll(wheelEventArgs.Delta);
                    }
            }
        }

        public override void Update(Vector2i mousePosition)
        {
            if (_gameUI.Contains(mousePosition.X, mousePosition.Y))
            {
                _gameUI.OnHover(mousePosition.X, mousePosition.Y);
            }
            else 
            {
                _gameUI.OnIdle(mousePosition.X, mousePosition.Y);
            }
        }
        public override void Render()
        {
            // Clear Window
            Application.Instance().MainWindow.Clear();

            // Draw UI
            _boardUI.Draw();
            _gameUI.Draw();

            // Display Window
            Application.Instance().MainWindow.Display();
        }
        public void CheckForSquareClicked(MouseButtonEventArgs buttonEventArgs)
        {
            for (int y = 0; y < _boardUI.SquareUI.GetLength(0); ++y)
            {
                for (int x = 0; x < _boardUI.SquareUI.GetLength(1); ++x)
                {
                    if (_boardUI.SquareUI[x,y].IsClicked(buttonEventArgs))
                     {
                        // Clicked Square Is Current Player's
                        if (_game.Board.GetPieceAt(x,y) != null && _game.Board.GetPieceAt(x,y).Color == _game.CurrentPlayer)
                        {
                            // No Selected Piece
                            if (!_boardUI.SquareIsSelected)
                            {
                                _boardUI.SelectSquare(new Position(x, y));
                            }

                            // Reclicked Selected Piece
                            else if (_boardUI.Selection.X == x && _boardUI.Selection.Y == y)
                            {
                                _boardUI.UnselectSquare();
                            }

                            // Clicked On New Piece
                            else 
                            {
                                _boardUI.SelectSquare(new Position(x, y));
                            }

                            _boardUI.HighlightSquares(_game.Board);
                        }
                        else if (_boardUI.SquareIsSelected)
                        {
                            // Check If Clicked Square Is Valid Move For Selected Piece
                            if (_game.Board.GetPieceAt(_boardUI.Selection.X,_boardUI.Selection.Y).HasMove(new Position(x, y)))
                            {
                                _boardUI.UnselectSquare();
                                _game.ExecuteMove(_game.Board.GetPieceAt(_boardUI.Selection.X,_boardUI.Selection.Y).GetMove(new Position(x, y)));
                            }
                        }
                    }
                }
            }
        }
        private void UpdateBoardUI()
        {
            _boardUI.Update(_game.Board);
            _boardUI.HighlightSquares(_game.Board);
        }
        public void UpdateGameState(object sender, GameEventArgs eventArgs)
        {
            switch (eventArgs.State)
            {
                case GameState.WhiteTurn:
                    moveHistory.AddText(false, _game.PastMoves.Peek().ToString());
                    _game.IncrementTurn();
                    break;
                case GameState.BlackTurn:
                    moveHistory.AddText(true, _game.TurnCounter + ".  " + _game.PastMoves.Peek().ToString());
                    break;
                case GameState.WhiteInCheck:
                    moveHistory.AddText(false, _game.PastMoves.Peek().ToString() + "+");
                    _game.IncrementTurn();
                    break;
                case GameState.BlackInCheck:
                    moveHistory.AddText(true, _game.TurnCounter + ".  " + _game.PastMoves.Peek().ToString() + "+");
                    break;
                case GameState.WhiteMated:
                    moveHistory.AddText(false, _game.PastMoves.Peek().ToString() + "#");
                    moveHistory.AddText(true, "    0-1");
                    break;
                case GameState.BlackMated:
                    moveHistory.AddText(true, _game.TurnCounter + ".  " + _game.PastMoves.Peek().ToString() + "#");
                    moveHistory.AddText(false, "1-0");
                    break;
                case GameState.WhiteStalemate:
                    moveHistory.AddText(false, "0.5-0.5");
                    break;
                case GameState.BlackStalemate:
                    moveHistory.AddText(true, "    0.5-0.5");
                    break;
            }

            UpdateBoardUI();
        }
    }
}