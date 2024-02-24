using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace Brick
{
    public class BrickManager : MonoBehaviour
    {
        [SerializeField] private List<GameObject> boosters;
        [SerializeField] private List<BrickController> bricks;
        
        private void Start()
        {
            bricks.ForEach(brick => brick.BrickManager = this);
        }

        public void SpawnBooster(Vector3 position)
        {
            var random = new Random();
            var chance = random.Next(0, 100);
            if (chance > 20) return;
            var randomIndex = random.Next(0, boosters.Count);
            Instantiate(boosters[randomIndex], position, Quaternion.identity);
        }

        public void RemoveBrick(BrickController brickController)
        {
            bricks.Remove(brickController);
            Destroy(brickController.gameObject);
        }
    }
}