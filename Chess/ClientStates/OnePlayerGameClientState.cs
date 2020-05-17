using Chess.GameLogic;
using Chess.UI;
using SFML.Window;
using SFML.System;
using System;

namespace Chess.ClientStates
{
    class OnePlayerGameClientState : ClientState
    {
        private Game game;
        private BoardUI boardUI;
        private ChessColor humanPlayer = ChessColor.White;
        private ChessAI aiPlayer;

        public override void Init()
        {
            ResetGame();
            game.BoardStateChanged += UpdateBoardUI;
        }
        public void ResetGame()
        {
            game = new Game();
            boardUI = new BoardUI(game.board);
        }
        public override void HandleInput(MouseButtonEventArgs buttonEventArgs)
        {
            if (Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                if (!game.gameOver && game.currentTurn == humanPlayer)
                {
                    CheckForSquareClicked(buttonEventArgs);
                }
            }
        }
        public override void Update(Vector2i mousePosition)
        {
            if (game.currentTurn != humanPlayer)
            {

            }
        }
        public override void Render()
        {
            // Clear Window
            GameClient.Instance().mainWindow.Clear();

            // Draw Board
            boardUI.Draw();

            // Display Window
            GameClient.Instance().mainWindow.Display();
        }
        public void CheckForSquareClicked(MouseButtonEventArgs buttonEventArgs)
        {
            for (int y = 0; y < boardUI.squaresUI.GetLength(0); ++y)
            {
                for (int x = 0; x < boardUI.squaresUI.GetLength(1); ++x)
                {
                    if (boardUI.squaresUI[x,y].IsClicked(buttonEventArgs))
                     {
                        // Clicked Square Is Current Player's
                        if (game.board.pieces[x,y] != null && game.board.pieces[x,y].color == humanPlayer)
                        {
                            // No Selected Piece
                            if (!boardUI.squareIsSelected)
                            {
                                boardUI.SelectSquare(new Position(x, y));
                            }

                            // Reclicked Selected Piece
                            else if (boardUI.selection.x == x && boardUI.selection.y == y)
                            {
                                boardUI.UnselectSquare();
                            }

                            // Clicked On New Piece
                            else 
                            {
                                boardUI.SelectSquare(new Position(x, y));
                            }

                            boardUI.HighlightSquares(game.board);
                        }
                        else if (boardUI.squareIsSelected)
                        {
                            // Check If Clicked Square Is Valid Move For Selected Piece
                            if (game.board.pieces[boardUI.selection.x, boardUI.selection.y].HasMove(new Position(x, y)))
                            {
                                boardUI.UnselectSquare();
                                game.ExecuteMove(game.board.pieces[boardUI.selection.x, boardUI.selection.y].GetMove(new Position(x, y)));
                            }
                        }
                    }
                }
            }
        }
        public void UpdateBoardUI(object sender, EventArgs eventArgs)
        {
            boardUI.Update(game.board);
            boardUI.HighlightSquares(game.board);
        }
    }
}