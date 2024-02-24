using Essentials;
using EventBus;
using EventBus.Events;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Ball
{
    public class BallController : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb;
        public float initialSpeed = 25f;
        private Vector2 _lastVelocity;

        private void Start()
        {
            EventBus<ChangeBallListEvent>.Dispatch(new ChangeBallListEvent {IsAdd = true, Ball = this});
        }

        private void OnEnable()
        {
            EventBus<MultiplyBallEvent>.Subscribe(MultiplyBall);
        }

        private void OnDisable()
        {
            EventBus<MultiplyBallEvent>.Unsubscribe(MultiplyBall);
        }

        private void Update()
        {
            _lastVelocity = rb.velocity;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Destroyer"))
            {
                EventBus<ChangeBallListEvent>.Dispatch(new ChangeBallListEvent {IsAdd = false, Ball = this});
                Destroy(gameObject);
                return;
            }
            var speed = _lastVelocity.magnitude;
            var direction = Vector2.Reflect(_lastVelocity.normalized, collision.contacts[0].normal);
            rb.velocity = direction * Mathf.Max(speed, initialSpeed);
        }
        private void MultiplyBall(MultiplyBallEvent @event)
        {
            for (int i = 0; i < 2; i++)
            {
                var ball = Instantiate(GameManager.Instance.BallManager.BallPrefab, transform.position, Quaternion.identity);
                float randomX = Random.Range(-1.0f, 1.0f);
                float randomY = Random.Range(-1.0f, 1.0f);
                Vector2 randomDirection = new Vector2(randomX, randomY).normalized;
                ball.GetComponent<Rigidbody2D>().velocity = randomDirection * initialSpeed;
                EventBus<ChangeBallListEvent>.Dispatch(new ChangeBallListEvent {IsAdd = true, Ball = ball.GetComponent<BallController>()});
            }
        }

        public void MoveBall()
        {
            rb.velocity = new Vector2(initialSpeed, initialSpeed);
        }
    }
}