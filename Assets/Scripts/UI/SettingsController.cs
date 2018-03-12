using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class SettingsController : MonoBehaviour
    {
        private Settings _settings;
        private Boolean _settingsSetted;

        [Inject]
        public void Construct(Settings settings)
        {
            _settings = settings;
        }

        private void Update()
        {
            if (_settingsSetted) 
                return;
            
            MusicSlider.value = _settings.BaseMusicVolume;
            SFXSlider.value = _settings.BaseSFXVolume;
            OnMusicVolumeChange(_settings.BaseMusicVolume);
            OnSFXVolumeChange(_settings.BaseSFXVolume);
            _settingsSetted = true;
        }

        [Inject(Id = "MusicSlider")] public Slider MusicSlider;
        [Inject(Id = "SFXSlider")] public Slider SFXSlider;

        public void OnMusicVolumeChange(float value)
        {
            if (_settings?.MusicGroup != null)
            {
                _settings.BaseMusicVolume = value;
                _settings.MusicGroup.audioMixer.SetFloat("MusicVolume", value * 75 - 60);
            }
        }

        public void OnSFXVolumeChange(float value)
        {
            if (_settings?.SFXGroup != null)
            {
                _settings.BaseSFXVolume = value;
                _settings.SFXGroup.audioMixer.SetFloat("SFXVolume", value * 75 - 60);
            }
        }

        [Serializable]
        public class Settings
        {
            public AudioMixerGroup MusicGroup;
            public float BaseMusicVolume = 0.7f;
            public AudioMixerGroup SFXGroup;
            public float BaseSFXVolume = 0.7f;
        }
    }
}