using System;
using UnityEngine;

namespace Booster
{
    public class BoosterController : MonoBehaviour
    {
        [SerializeField] private BoosterType boosterType;
        [SerializeField] private float speed = 10.0f;
        public BoosterType Type => boosterType;

        private void OnEnable()
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(0, -speed);
        }
    }
}