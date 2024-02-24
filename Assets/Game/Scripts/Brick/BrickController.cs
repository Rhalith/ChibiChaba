using Essentials;
using EventBus;
using EventBus.Events;
using UnityEngine;

namespace Brick
{
    public class BrickController : MonoBehaviour
    {
        [SerializeField] private BrickManager brickManager;
        private int _points = 100;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Ball"))
            {
                brickManager.SpawnBooster(transform.position);
                EventBus<BallHitEvent>.Dispatch(new BallHitEvent());
                Destroy(gameObject);
            }
        }
    }
}