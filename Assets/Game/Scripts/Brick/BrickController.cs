using Essentials;
using UnityEngine;

namespace Brick
{
    public class BrickController : MonoBehaviour
    {
        private int _points = 100;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Ball"))
            {
                GameManager.Instance.BrickDestroyed(_points);
                Destroy(gameObject);
            }
        }
    }
}