using EventBus;
using System.Collections.Generic;
using EventBus.Events;
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

        private void OnEnable()
        {
            EventBus<ChangeBallListEvent>.Subscribe(ChangeBallList);
        }
        
        private void OnDisable()
        {
            EventBus<ChangeBallListEvent>.Unsubscribe(ChangeBallList);
        }

        private void ChangeBallList(ChangeBallListEvent @event)
        {
            if (@event.IsAdd)
            {
                if (!balls.Contains(@event.Ball))
                {
                    balls.Add(@event.Ball);
                }
            }
            else
            {
                if (balls.Contains(@event.Ball))
                {
                    balls.Remove(@event.Ball);
                }
            }
            CheckBalls();
        }

        private void CheckBalls()
        {
            if (balls.Count <= 250) return;
            for (var i = balls.Count - 1; i > 250; i--)
            {
                Destroy(balls[i]);
                balls.RemoveAt(i);
            }
        }
    }
}