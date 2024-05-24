using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField]
    private AudioSource _gameMusicSource;

    [SerializeField]
    private AudioSource _batSoundSource;

    [SerializeField]
    private AudioSource _asseptSound;

    [SerializeField]
    private AudioSource _declineSound;

    private readonly string _volumeKey = "Volume";

    public static AudioController Instance;

    public float Volume;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }    

        Instance = this;
        //Debug.Log("Instance create! " + Instance);
        
        DontDestroyOnLoad(gameObject);
    }
    private void OnDisable()
    {
        PlayerPrefs.SetFloat(_volumeKey, Volume);
    }
    public void Initialize()
    {
        if (!PlayerPrefs.HasKey(_volumeKey))
            PlayerPrefs.SetFloat(_volumeKey, 0f);

        Volume = PlayerPrefs.GetFloat(_volumeKey);
    }
    public void ChangeVolume(float newVolume)
    {
        Volume = newVolume;
        PlayerPrefs.SetFloat(_volumeKey, Volume);
        
        if (_gameMusicSource != null)
            _gameMusicSource.volume = Volume;
        if (_batSoundSource != null)
            _batSoundSource.volume = Volume;
        if (_asseptSound != null)
            _asseptSound.volume = Volume;
        if (_declineSound != null)
            _declineSound.volume = Volume;
    }

    public void PlayBatSound()
    {
        if (_batSoundSource != null)
        {
            _batSoundSource.volume = Volume;
            _batSoundSource.Play();
        }
    }

    public void PlayAsseptSound()
    {
        if (_asseptSound != null)
        {
            _asseptSound.volume = Volume;
            _asseptSound.Play();
        }
    }

    public void PlayDeclineSound()
    {
        if (_declineSound != null)
        {
            _declineSound.volume = Volume;
            _declineSound.Play();
        }
    }
}
