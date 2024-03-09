using EventBus;
using EventBus.Events;
using UnityEngine;

namespace Brick.Runtime
{
    public class BrickController : MonoBehaviour
    {
        private BrickManager _brickManager;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Color color;
        private int _points = 100;
        
        private void Start()
        {
            _brickManager = BrickManager.Instance;
            spriteRenderer.color = color;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Ball"))
            {
                _brickManager.SpawnBooster(this);
                EventBus<BallHitEvent>.Dispatch(new BallHitEvent());
                GetComponent<Collider2D>().enabled = false;
                Destroy(gameObject);
            }
        }
    }
}