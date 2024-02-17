using UnityEngine;

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
    }
}