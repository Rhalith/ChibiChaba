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
                float angle = Random.Range(0, 360);
                Vector2 direction = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
                ball.GetComponent<Rigidbody2D>().velocity = direction * initialSpeed;
                ball.transform.parent = GameManager.Instance.BallManager.transform;
            }
        }

        public void MoveBall()
        {
            rb.velocity = new Vector2(initialSpeed, initialSpeed);
        }
    }
}