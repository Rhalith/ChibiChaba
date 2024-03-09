using System.Collections.Generic;
using EventBus;
using EventBus.Events;
using UnityEngine;

namespace Brick.Runtime
{
    public class SpawnBreakableBricks : MonoBehaviour
    {
        [SerializeField] private BrickController breakableBrickPrefab;
        [SerializeField] private int width;
        [SerializeField] private int height;
        [SerializeField] private float offset = 1.5f;
        [SerializeField] private Vector2 spawnPosition;
        
        [SerializeField] private List<BrickController> bricks;

        public void GenerateBricks()
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    var brick = Instantiate(breakableBrickPrefab, new Vector3(spawnPosition.x - i * offset, spawnPosition.y + j * offset, 0), Quaternion.identity, transform);
                    bricks.Add(brick);
                }
            }
        }
    }
}