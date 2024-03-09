using System.Collections.Generic;
using Essentials;
using EventBus.Events;
using EventBus;
using UnityEngine;
using Random = System.Random;

namespace Brick.Runtime
{
    public class BrickManager : MonoBehaviour
    {
        [SerializeField] private List<GameObject> boosters;
        [SerializeField] private List<BrickController> bricks;
        public static BrickManager Instance { get; private set; }
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
        public void SpawnBooster(BrickController brickController)
        {
            ChangeBrickList(brickController);
            var random = new Random();
            var chance = random.Next(0, 100);
            if (chance > 10) return;
            var randomIndex = random.Next(0, boosters.Count);
            Instantiate(boosters[randomIndex], brickController.transform.position, Quaternion.identity);
        }

        private void ChangeBrickList(BrickController brickController)
        {
            if (bricks.Contains(brickController))
            {
                bricks.Remove(brickController);
            }
            if (bricks.Count <= 0)
            {
                GameManager.Instance.GameOver(true);
            }
        }
    }
}