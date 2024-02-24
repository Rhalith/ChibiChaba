﻿using System;
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
        private bool _videoChangedCompletely;

        private void OnEnable()
        {
            EventBus<BallHitEvent>.Subscribe(ResetTime);
            EventBus<ChangeVideoCompletelyEvent>.Subscribe(ChangeVideoCompletely);
        }

        private void OnDisable()
        {
            EventBus<BallHitEvent>.Unsubscribe(ResetTime);
            EventBus<ChangeVideoCompletelyEvent>.Unsubscribe(ChangeVideoCompletely);
        }

        private void Start()
        {
            ChangeVideo(false);
        }

        private void Update()
        {
            if(_videoChangedCompletely) return;
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
        
        
        private void ChangeVideoCompletely(ChangeVideoCompletelyEvent @event)
        {
            _videoChangedCompletely = true;
        }
    }
}