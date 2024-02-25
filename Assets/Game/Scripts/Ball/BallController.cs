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
        public float initialSpeed = 40f;

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

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Destroyer"))
            {
                EventBus<ChangeBallListEvent>.Dispatch(new ChangeBallListEvent {IsAdd = false, Ball = this});
                Destroy(gameObject);
            }
            else if (collision.gameObject.CompareTag("Player"))
            {
                Vector3 paddlePos = collision.transform.position;
                Vector2 contactPoint = collision.contacts[0].point;
                float width = collision.collider.bounds.size.x;

                float difference = (contactPoint.x - paddlePos.x) / (width / 2);
                float angle = difference * 75;
                
                Vector2 newDirection = new Vector2(Mathf.Sin(angle * Mathf.Deg2Rad), Mathf.Cos(angle * Mathf.Deg2Rad)).normalized;
                
                rb.velocity = newDirection * initialSpeed;
            }
        }
        private void MultiplyBall(MultiplyBallEvent @event)
        {
            if (GameManager.Instance.BallManager.CanSpawnBall())
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
        }

        public void MoveBall()
        {
            rb.velocity = new Vector2(initialSpeed, initialSpeed);
        }
    }
}