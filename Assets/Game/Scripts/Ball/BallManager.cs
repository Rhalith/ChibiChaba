using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ball
{
    public class BallManager : MonoBehaviour
    {
        [SerializeField] private GameObject ballPrefab;
        [SerializeField] private List<BallController> balls;

        public GameObject BallPrefab => ballPrefab;

        private void Start()
        {
            balls[0].MoveBall();
        }
    }
}