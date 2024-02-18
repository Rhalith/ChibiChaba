using UnityEngine;

namespace Essentials
{
    public class ResponsiveCamera : MonoBehaviour
    {
        [SerializeField] private Transform levelBounds;

        private void Awake()
        {
            AdjustCameraToFitLevelBounds();
        }

        private void AdjustCameraToFitLevelBounds()
        {
            if (levelBounds == null) return;

            var levelBoundsSize = levelBounds.GetComponent<SpriteRenderer>().bounds.size;

            float orthographicSize = levelBoundsSize.y / 2;

            float horizontalSize = (levelBoundsSize.x / 2) / Camera.main.aspect;

            Camera.main.orthographicSize = Mathf.Max(orthographicSize, horizontalSize);

            CenterCameraOnLevelBounds();
        }

        private void CenterCameraOnLevelBounds()
        {
            if (levelBounds == null) return;

            Vector3 levelBoundsCenter = levelBounds.GetComponent<SpriteRenderer>().bounds.center;
            Camera.main.transform.position = new Vector3(levelBoundsCenter.x, levelBoundsCenter.y, Camera.main.transform.position.z);
        }
    }
}