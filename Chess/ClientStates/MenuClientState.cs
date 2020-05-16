using Chess.UI;
using Chess.Systems;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

namespace Chess.ClientStates
{
    class MenuClientState : ClientState
    {
        private const int ButtonWidth = 400;
        private const int ButtonHeight = 50;
        private Button[] menuButtons;
        private Sprite background;

        public override void Init()
        {
            InitBackground();
            InitButtons();
        }
        private void InitBackground()
        {
            background = new Sprite(GameClient.Instance().assetManager.textures[TextureID.MenuBackground]);
        }
        private void InitButtons()
        {
            Font menuFont = new Font(GameClient.Instance().assetManager.fonts[FontID.MenuFont]);

            // One Player
            Button onePlayer = CreateMenuButton(menuFont, "ONE PLAYER", 0);
            onePlayer.Disable();

            // Two Player
            Button twoPlayer = CreateMenuButton(menuFont, "TWO PLAYER", 1);
            twoPlayer.SetCommand(new GoToStateCommand(new TwoPlayerGameClientState()));

            // Online
            Button online = CreateMenuButton(menuFont, "ONLINE", 2);
            online.Disable();

            // Options
            Button options = CreateMenuButton(menuFont, "OPTIONS", 3);
            options.Disable();

            // Exit
            Button exit = CreateMenuButton(menuFont, "EXIT", 4);
            exit.SetCommand(new ExitProgramCommand());

            // Buttons Array
            menuButtons = new Button[] { onePlayer, twoPlayer, online, options, exit };
        }
        private Button CreateMenuButton(Font font, string text, int yPositionModifier)
        {
            Button button = new Button(font, text, 50, Color.Black, Color.White, new Color(200, 200, 200), new Vector2f(
                (GameClient.Instance().mainWindow.Size.X / 2.0f) - (ButtonWidth / 2.0f),
                (GameClient.Instance().mainWindow.Size.Y / 6.0f) - (ButtonHeight / 6.0f) + ((GameClient.Instance().mainWindow.Size.Y / 6.0f) * yPositionModifier)),
                new Vector2f(ButtonWidth, ButtonHeight));
            button.SetOutLine(1, Color.Black);

            return button;
        }
        public override void HandleInput(MouseButtonEventArgs buttonEventArgs)
        {
            if (Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                foreach (Button button in menuButtons)
                {
                    if (button.Contains(buttonEventArgs.X, buttonEventArgs.Y))
                    {
                        button.Click();
                    }
                }
            }
        }
        public override void Update(Vector2i mousePosition)
        {
            foreach (Button button in menuButtons)
            {
                if (button.Contains(mousePosition.X, mousePosition.Y))
                {
                    button.Hover();
                }
                else 
                {
                    button.Idle();
                }
            }
        }
        public override void Render()
        {
            // Clear Window
            GameClient.Instance().mainWindow.Clear();

            // Draw Background Image
            GameClient.Instance().mainWindow.Draw(background);

            // Draw Buttons
            foreach (Button button in menuButtons)
            {
                button.Draw();
            }
            
            // Display Window
            GameClient.Instance().mainWindow.Display();
        }
    }
}