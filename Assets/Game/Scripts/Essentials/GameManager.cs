using System.Collections.Generic;
using Ball;
using Brick;
using TMPro;
using UnityEngine;

namespace Essentials
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private BallManager ballManager;


        private int _totalBricks;
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
    }
}