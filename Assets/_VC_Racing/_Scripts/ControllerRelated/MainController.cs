using UnityEngine;

namespace _VC_Racing._Scripts.ControllerRelated
{
    public enum GameState
    {
        None,
        LevelStart,
        Running,
        LevelComplete,
        LevelFail
    }
    public class MainController : MonoBehaviour
    {
        public static MainController instance;
        
        [SerializeField] private GameState _gameState;
        public static event System.Action<GameState, GameState> GameStateChanged;
        public GameState GameState
        {
            get
            {
                return _gameState;
            }
            private set
            {
                if (value != _gameState)
                {
                    GameState oldState = _gameState;
                    _gameState = value;

                    if (GameStateChanged != null)
                        GameStateChanged(_gameState, oldState);
                }
            }
        }

        private void Awake()
        {
            instance = this;
        }
        
        void Start()
        {
            CreateGame();
        }
        
        public void CreateGame()
        {
            //GameState = GameState.LevelStart;
        }

        public void StartGame()
        {
            GameState = GameState.LevelStart;
        }
        public void SetActionType(GameState curState)
        {
            GameState = curState;
        }
    }
}