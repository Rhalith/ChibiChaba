using UnityEngine;
using Booster;
using Essentials;
using EventBus;
using EventBus.Events;

namespace Paddle
{
    public class PaddleController : MonoBehaviour
    {
        [SerializeField] private float speed = 10.0f;
        [SerializeField] private float leftBound = -8f;
        [SerializeField] private float rightBound = 8f;

        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                MovePaddle(Input.mousePosition);
            }

            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                MovePaddle(touch.position);
            }
        }

        private void MovePaddle(Vector2 inputPosition)
        {
            Vector2 paddlePosition = Camera.main.ScreenToWorldPoint(inputPosition);

            paddlePosition.y = transform.position.y;
            paddlePosition.x = Mathf.Clamp(paddlePosition.x, leftBound, rightBound);
            transform.position = paddlePosition;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out BoosterController booster))
            {
                ApplyBoosterEffect(booster.Type);
                Destroy(other.gameObject);
            }
        }

        private void ApplyBoosterEffect(BoosterType boosterType)
        {
            switch (boosterType)
            {
                case BoosterType.Increasing:
                    SpawnBalls();
                    break;
                case BoosterType.Multiplier:
                    EventBus<MultiplyBallEvent>.Dispatch(new MultiplyBallEvent());
                    break;
                case BoosterType.Wider:
                    IncreaseWide();
                    break;
            }
        }

        private void IncreaseWide()
        {
            var localScale = transform.localScale;
            localScale = new Vector3(localScale.x * 1.1f, localScale.y, localScale.z);
            transform.localScale = localScale;
        }

        private void SpawnBalls()
        {
            for (int i = 0; i < 3; i++)
            {
                var ball = Instantiate(GameManager.Instance.BallManager.BallPrefab, new Vector3(transform.position.x, transform.position.y + 2, transform.position.z),
                    Quaternion.identity);
                float angle = Random.Range(10, 180);
                Vector2 direction = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
                ball.GetComponent<Rigidbody2D>().velocity = direction * 40f;
                ball.transform.parent = GameManager.Instance.BallManager.transform;
            }
        }
    }
}