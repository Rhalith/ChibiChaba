using Essentials;
using EventBus;
using EventBus.Events;
using UnityEngine;

namespace Brick
{
    public class BrickController : MonoBehaviour
    {
        private BrickManager _brickManager;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Color color;
        private int _points = 100;

        public BrickManager BrickManager
        {
            get => _brickManager;
            set => _brickManager = value;
        }

        private void Start()
        {
            spriteRenderer.color = color;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Ball"))
            {
                _brickManager.SpawnBooster(transform.position);
                EventBus<BallHitEvent>.Dispatch(new BallHitEvent());
                _brickManager.RemoveBrick(this);
            }
        }
    }
}