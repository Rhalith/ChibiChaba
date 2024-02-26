using Ball;
using EventBus;
using EventBus.Events;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Essentials
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private BallManager ballManager;
        public static GameManager Instance { get; private set; }

        public BallManager BallManager => ballManager;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void GameOver(bool isWin)
        {
            EventBus<StopGameEvent>.Dispatch(new StopGameEvent());
            EventBus<DisplayFinishTextEvent>.Dispatch(new DisplayFinishTextEvent{IsWin = isWin});
        }
        
        public void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}