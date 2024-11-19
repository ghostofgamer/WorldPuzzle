using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundButton : MonoBehaviour
{
    [SerializeField] private Image _soundButtonImage;
    [SerializeField] private Image _musicButtonImage;
    [SerializeField] private Sprite _soundOn;
    [SerializeField] private Sprite _soundOff;
    [SerializeField] private AudioMixerGroup _audioMixer;

    private void Start()
    {
        if (_soundButtonImage != null)
        {
            if (SoundManager.soundOn == 1)
                _soundButtonImage.sprite = _soundOn;
            else
                _soundButtonImage.sprite = _soundOff;
        }
        else
        {
            if (SoundManager.musicOn == 1)
                _musicButtonImage.sprite = _soundOn;
            else
                _musicButtonImage.sprite = _soundOff;
        }
    }

    public void ToggleMusic()
    {
        if (SoundManager.musicOn == 1)
        {
            _audioMixer.audioMixer.SetFloat("MusicVolume", -80);
            SoundManager.musicOn = 0;
            PlayerPrefs.SetInt("MusicOn", 0);
            PlayerPrefs.Save();
            _musicButtonImage.sprite = _soundOff;
        }

        else
        {
            _audioMixer.audioMixer.SetFloat("MusicVolume", 0);
            SoundManager.musicOn = 1;
            PlayerPrefs.SetInt("MusicOn", 1);
            PlayerPrefs.Save();
            _musicButtonImage.sprite = _soundOn;
        }
    }

    public void ToggleEffect()
    {
        if (SoundManager.soundOn == 1)
        {
            _audioMixer.audioMixer.SetFloat("SoundVolume", -80);
            SoundManager.soundOn = 0;
            PlayerPrefs.SetInt("SoundOn", 0);
            PlayerPrefs.Save();
            _soundButtonImage.sprite = _soundOff;
        }

        else
        {
            _audioMixer.audioMixer.SetFloat("SoundVolume", 0);
            SoundManager.soundOn = 1;
            PlayerPrefs.SetInt("SoundOn", 1);
            PlayerPrefs.Save();
            _soundButtonImage.sprite = _soundOn;
        }


        /*
        _audioMixer.audioMixer.SetFloat("SoundVolume", -80);
        _audioMixer.audioMixer.SetFloat("SoundVolume", 0);*/
    }

    public void ToggleSound()
    {
        if (SoundManager.musicOn == 1)
        {
            SoundManager.musicOn = 0;
            SoundManager.soundOn = 0;
            PlayerPrefs.SetInt("SoundOn", 0);
            PlayerPrefs.SetInt("MusicOn", 0);
            PlayerPrefs.Save();

            _soundButtonImage.sprite = _soundOff;
            /*
            soundOffImageHolder.SetActive(true);
            soundOnImageHolder.SetActive(false);
            */

            SoundManager.Instance.MuteAllSounds();
        }
        else
        {
            SoundManager.musicOn = 1;
            SoundManager.soundOn = 1;
            PlayerPrefs.SetInt("SoundOn", 1);
            PlayerPrefs.SetInt("MusicOn", 1);
            PlayerPrefs.Save();

            _soundButtonImage.sprite = _soundOn;
            /*soundOffImageHolder.SetActive(false);
            soundOnImageHolder.SetActive(true);*/

            SoundManager.Instance.UnmuteAllSounds();
        }
    }
}