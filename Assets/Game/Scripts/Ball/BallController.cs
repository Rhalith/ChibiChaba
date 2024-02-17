using UnityEngine;

namespace Ball
{
    public class BallController : MonoBehaviour
    {
        public float initialSpeed = 25f;
        private Rigidbody2D rb;
        private Vector2 lastVelocity;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(initialSpeed, initialSpeed);
        }

        private void Update()
        {
            lastVelocity = rb.velocity;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            var speed = lastVelocity.magnitude;
            var direction = Vector2.Reflect(lastVelocity.normalized, collision.contacts[0].normal);
            rb.velocity = direction * Mathf.Max(speed, initialSpeed);
        }
    }
}