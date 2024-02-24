using UnityEngine;

namespace VideoScripts
{
    public class VideoPlayerPositioner : MonoBehaviour
    {
        private void Start()
        {
            PositionVideoPlayer();
        }

        private void PositionVideoPlayer()
        {
            Vector3 bottomCenter = new Vector3(0.5f, 0.0f, Camera.main.nearClipPlane);
            
            Vector3 worldPosition = Camera.main.ViewportToWorldPoint(bottomCenter);
        
            worldPosition.y += transform.lossyScale.y / 2;
            transform.position = new Vector3(worldPosition.x, worldPosition.y, transform.position.z);
        }
    }
}