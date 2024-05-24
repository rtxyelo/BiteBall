using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private GameObject _gameOverPanel;

    [SerializeField]
    private TMP_Text _gameOverMoneyText;

    [SerializeField]
    private TMP_Text _gameOverDistanceText;

    [SerializeField]
    private TMP_Text _moneyText;

    [SerializeField]
    private TMP_Text _distanceText;

    [SerializeField]
    private GameObject _arrowScaleFrame;

    [SerializeField]
    private Button _playButton;

    [SerializeField]
    private ArrowBehaviour _arrowBehaviour;

    private Animator _animator;

    private AudioController _audioController;

    private bool _isGameStart = false;

    private readonly string _moneyKey = "Money";

    private readonly string _multiplierKey = "Multiplier";

    private readonly string _levelKey = "Level";
    private readonly string _maxLevelKey = "MaxLevel";

    private readonly string _easyRecordKey = "EasyRecord";
    private readonly string _mediumRecordKey = "MediumRecord";
    private readonly string _hardRecordKey = "HardRecord";

    private readonly List<int> _listOfLvlDiff = new List<int>(3) { 500, 2000, 5000 };

    private float _distance = 0f;

    private float _multiplier = 1.0f;

    private int _currentLvlRec;

    private int _currentLevel;

    private float _hitMultiplier = 1f;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey(_multiplierKey))
            PlayerPrefs.SetFloat(_multiplierKey, 1f);

        _multiplier = PlayerPrefs.GetFloat(_multiplierKey, 1f);

        _currentLevel = PlayerPrefs.GetInt(_levelKey);

        if (_currentLevel == 0)
            _currentLvlRec = PlayerPrefs.GetInt(_easyRecordKey);
        else if (_currentLevel == 1)
            _currentLvlRec = PlayerPrefs.GetInt(_mediumRecordKey);
        else if (_currentLevel == 2)
            _currentLvlRec = PlayerPrefs.GetInt(_hardRecordKey);
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();

        _playButton.onClick.RemoveAllListeners();
        _playButton.onClick.AddListener(() =>
        {
            _hitMultiplier = _arrowBehaviour.TipRotation;

            if (_animator != null)
                _animator.Play("Hit");

            _playButton.interactable = false;
        });

        _audioController = FindObjectOfType<AudioController>();

        _moneyText.text = PlayerPrefs.GetInt(_moneyKey).ToString();
    }

    private void PlayBatSound()
    {
        if (_audioController != null)
            _audioController.PlayBatSound();
    }

    public void GameOver()
    {
        Debug.Log("Game Over!");

        _isGameStart = false;

        if (Mathf.Round(_distance) >= _currentLvlRec)
        {
            if (_currentLevel == 0)
                PlayerPrefs.SetInt(_easyRecordKey, Convert.ToInt32(Mathf.Round(_distance)));
            else if (_currentLevel == 1)
                PlayerPrefs.SetInt(_mediumRecordKey, Convert.ToInt32(Mathf.Round(_distance)));
            else if (_currentLevel == 2)
                PlayerPrefs.SetInt(_hardRecordKey, Convert.ToInt32(Mathf.Round(_distance)));
        }

        if (Mathf.Round(_distance) >= _listOfLvlDiff[_currentLevel] && PlayerPrefs.GetInt(_maxLevelKey) == _currentLevel && _currentLevel != 2)
        {
            PlayerPrefs.SetInt(_maxLevelKey, PlayerPrefs.GetInt(_maxLevelKey) + 1);
        }

        _gameOverPanel.SetActive(true);

        PlayerPrefs.SetInt(_moneyKey, PlayerPrefs.GetInt(_moneyKey) + Convert.ToInt32(Mathf.Round(_distance)) / 10);

        _gameOverMoneyText.text = "+" + Convert.ToInt32(Mathf.Round(_distance)) / 10;

        _gameOverDistanceText.text = Convert.ToInt32(Mathf.Round(_distance)).ToString() + "m";
    }

    public void StartGame()
    {
        Debug.Log("Game Start!");

        _arrowScaleFrame.SetActive(false);
        _distanceText.gameObject.SetActive(true);

        PlayBatSound();

        _isGameStart = true;

        Debug.Log("Multiplier " + _multiplier + " HitMultiplier " + _hitMultiplier);
    }

    private void FixedUpdate()
    {
        if (_isGameStart)
        {
            _distance += Time.fixedDeltaTime * _multiplier * _hitMultiplier * 100;
            _distanceText.text = Mathf.Round(_distance).ToString() + $"m/{_listOfLvlDiff[PlayerPrefs.GetInt(_levelKey)]}m";

            _moneyText.text = (PlayerPrefs.GetInt(_moneyKey) + Convert.ToInt32(Mathf.Round(_distance)) / 10).ToString();
        }
    }
}
