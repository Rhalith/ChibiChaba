using UnityEngine;
using Booster;
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
                    EventBus<SpawnBallEvent>.Dispatch(new SpawnBallEvent{BallCount = 3});
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
    }
}