using Chess.UI;
using Chess.Systems;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

namespace Chess.States
{
    class MenuState : State
    {
        private const int ButtonWidth = 400;
        private const int ButtonHeight = 50;
        private UIContainer _menuUI;

        public override void Init()
        {
            _menuUI = new UIContainer();
            InitBackground();
            InitButtons();
        }
        private void InitBackground()
        {
            Graphic background = new Graphic(Application.Instance().AssetManager.Textures[TextureID.MenuBackground]);
            _menuUI.AddElement(background);
        }
        private void InitButtons()
        {
            Font menuFont = new Font(Application.Instance().AssetManager.Fonts[FontID.MenuFont]);;

            // Play Game
            TextButton playGame = CreateMenuButton(menuFont, "PLAY GAME", -1);
            playGame.SetCommand(new GoToStateCommand(new PlayState()));

            // Exit
            TextButton exit = CreateMenuButton(menuFont, "EXIT", 1);
            exit.SetCommand(new ExitProgramCommand());

            // Add Buttons
            _menuUI.AddElement(playGame);
            _menuUI.AddElement(exit);
        }
        private TextButton CreateMenuButton(Font font, string text, int yOffsetModifier)
        {
            TextButton button = new TextButton(new Vector2f(
                (Application.Instance().MainWindow.Size.X / 2.0f) - (ButtonWidth / 2.0f),
                (Application.Instance().MainWindow.Size.Y / 2.0f) - (ButtonHeight / 2.0f) + (yOffsetModifier * ButtonHeight)),
            new Vector2f(ButtonWidth, ButtonHeight), Color.White, new Color(200, 200, 200));

            button.SetText(font, text, 50, Color.Black);
            button.SetBorder(1, Color.Black);

            return button;
        }
        public override void HandleInput(MouseButtonEventArgs buttonEventArgs)
        {
            if (Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                if (_menuUI.Contains(buttonEventArgs.X, buttonEventArgs.Y))
                    {
                        _menuUI.OnClick(buttonEventArgs.X, buttonEventArgs.Y);
                    }
            }
        }
        public override void HandleInput(MouseWheelScrollEventArgs wheelEventArgs)
        {
            
        }
        public override void Update(Vector2i mousePosition)
        {
            if (_menuUI.Contains(mousePosition.X, mousePosition.Y))
            {
                _menuUI.OnHover(mousePosition.X, mousePosition.Y);
            }
            else 
            {
                _menuUI.OnIdle(mousePosition.X, mousePosition.Y);
            }
        }
        public override void Render()
        {
            // Clear Window
            Application.Instance().MainWindow.Clear();

            // Draw UI
            _menuUI.Draw();
            
            // Display Window
            Application.Instance().MainWindow.Display();
        }
    }
}