using SFML.Graphics;
using SFML.Window;
using Chess.States;
using Chess.Systems;

namespace Chess
{
   class Application
    {
        private const string WindowTitle = "Chess";
        private static Application _instance;
        public RenderWindow MainWindow { get; private set; }
        public StateManager StateManager { get; private set; }
        public AssetManager AssetManager { get; private set; }

        public Application()
        {
            // Create Window
            MainWindow = new RenderWindow(new VideoMode(GlobalConstants.WindowWidth, GlobalConstants.WindowHeight), WindowTitle, Styles.Titlebar | Styles.Close);

            // Create State Manager
            StateManager = new StateManager();

            // Create Asset Manager
            AssetManager = new AssetManager();

            // Setup Event Handlers
            MainWindow.Closed += (sender, e) => { ((Window)sender).Close(); };
            MainWindow.MouseButtonPressed += HandleMouseButton;
            MainWindow.MouseButtonReleased += HandleMouseButton;
            MainWindow.MouseWheelScrolled += HandleMouseWheelScroll;
        }
        public static void Init()
        {
            if (_instance == null)
            {
                _instance = new Application();
                _instance.StateManager.GoToState(new MenuState());
            }
        }
        public static Application Instance()
        {
            return _instance;
        }
        public void Run()
        {
            while (MainWindow.IsOpen)
            {
                MainWindow.DispatchEvents();
                StateManager.ProcessStateChange();
                StateManager.activeState.Update(Mouse.GetPosition(MainWindow));
                StateManager.activeState.Render();
            }
        }
        private void HandleMouseButton(object sender, MouseButtonEventArgs buttonEventArgs)
        {
            StateManager.activeState.HandleInput(buttonEventArgs);
        }
        private void HandleMouseWheelScroll(object sender, MouseWheelScrollEventArgs wheelEventArgs)
        {
            StateManager.activeState.HandleInput(wheelEventArgs);
        }
    }
}
