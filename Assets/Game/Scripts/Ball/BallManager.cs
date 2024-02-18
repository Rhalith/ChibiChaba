using EventBus;
using EventBus.Events;
using UnityEngine;

namespace Ball
{
    public class BallManager : MonoBehaviour
    {
        private void OnEnable()
        {
            EventBus<SpawnBallEvent>.Subscribe(SpawnBalls);
            EventBus<MultiplyBallEvent>.Subscribe(MultiplyBalls);
        }
        
        private void OnDisable()
        {
            EventBus<SpawnBallEvent>.Unsubscribe(SpawnBalls);
            EventBus<MultiplyBallEvent>.Unsubscribe(MultiplyBalls);
        }

        private void SpawnBalls(SpawnBallEvent @event)
        {
            
        }

        private void MultiplyBalls(MultiplyBallEvent @event)
        {
            
        }
    }
}