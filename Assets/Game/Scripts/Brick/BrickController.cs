using System;
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

        private void Awake()
        {
            _brickManager = BrickManager.Instance;
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
            }
        }
    }
}