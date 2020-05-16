using SFML.Graphics;
using SFML.Window;
using Chess.ClientStates;
using Chess.Systems;

namespace Chess
{
   class GameClient
    {
        private const int WindowWidth = 1000;
        private const int WindowHeight = 640;
        private const string WindowTitle = "Chess";
        private static GameClient instance;
        public RenderWindow mainWindow { get; private set; }
        public StateManager stateManager { get; private set; }
        public AssetManager assetManager { get; private set; }

        public GameClient()
        {
            // Create Window
            mainWindow = new RenderWindow(new VideoMode(WindowWidth, WindowHeight), WindowTitle, Styles.Titlebar | Styles.Close);

            // Create State Manager
            stateManager = new StateManager();

            // Create Asset Manager
            assetManager = new AssetManager();

            // Setup Event Handlers
            mainWindow.Closed += (sender, e) => { ((Window)sender).Close(); };
            mainWindow.MouseButtonPressed += HandleMouseButton;
            mainWindow.MouseButtonReleased += HandleMouseButton;
        }
        public static void Init()
        {
            if (instance == null)
            {
                instance = new GameClient();
                instance.stateManager.GoToState(new MenuClientState());
            }
        }
        public static GameClient Instance()
        {
            return instance;
        }
        public void Run()
        {
            while (mainWindow.IsOpen)
            {
                mainWindow.DispatchEvents();
                stateManager.ProcessStateChange();
                stateManager.activeState.Update(Mouse.GetPosition(mainWindow));
                stateManager.activeState.Render();
            }
        }
        private void HandleMouseButton(object sender, MouseButtonEventArgs buttonEventArgs)
        {
            stateManager.activeState.HandleInput(buttonEventArgs);
        }
    }
}
