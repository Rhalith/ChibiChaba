using System.Collections.Generic;
using Brick;
using TMPro;
using UnityEngine;

namespace Essentials
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private int score = 0;
        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private List<BrickController> bricks;


        private int _totalBricks;
        public static GameManager Instance { get; private set; }

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

        private void Start()
        {
            _totalBricks = bricks.Count;
            UpdateScoreText();
        }

        public void BrickDestroyed(int points)
        {
            score += points;
            _totalBricks--;
            UpdateScoreText();

            if (_totalBricks <= 0)
            {
                Debug.Log("All bricks destroyed! Load next level or restart.");
            }
        }

        private void UpdateScoreText()
        {
            if (scoreText != null)
            {
                scoreText.text = "Score: " + score;
            }
        }
    }
}