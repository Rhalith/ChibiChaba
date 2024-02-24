using System;
using EventBus;
using EventBus.Events;
using UnityEngine;
using UnityEngine.Video;

namespace VideoScripts
{
    public class VideoChanger : MonoBehaviour
    {
        [SerializeField] private VideoPlayer videoPlayer;
        [SerializeField] private VideoClip happyVideo;
        [SerializeField] private VideoClip sadVideo;


        private float _secondsPassed = 0.2f;
        private bool _isHappy;

        private void OnEnable()
        {
            EventBus<BallHitEvent>.Subscribe(ResetTime);
        }

        private void OnDisable()
        {
            EventBus<BallHitEvent>.Unsubscribe(ResetTime);
        }

        private void Start()
        {
            ChangeVideo(false);
        }

        private void Update()
        {
            _secondsPassed += Time.deltaTime;
            if (_secondsPassed > 0.15f)
            {
                if(_isHappy) ChangeVideo(false);
            }
            else
            {
                if(!_isHappy) ChangeVideo(true);
            }
        }

        private void ResetTime(BallHitEvent @event)
        {
            _secondsPassed = 0;
        }

        private void ChangeVideo(bool isHappy)
        {
            _isHappy = isHappy;
            videoPlayer.clip = isHappy ? happyVideo : sadVideo;
            videoPlayer.frame = 10;
        }
    }
}