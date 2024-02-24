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
        [SerializeField] private int maxBalls = 500;
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

            CheckBallCount();
        }

        private void CheckBallCount()
        {
            if (balls.Count > 35)
            {
                EventBus<ChangeVideoCompletelyEvent>.Dispatch(new ChangeVideoCompletelyEvent());
            }
        }

        public bool CanSpawnBall()
        {
            if (balls.Count > maxBalls) return false;
            return true;
        }
    }
}