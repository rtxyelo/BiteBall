using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    private readonly string _levelKey = "Level";
    private readonly string _maxLevelKey = "MaxLevel";
    
    private readonly string _easyRecordKey = "EasyRecord";
    private readonly string _mediumRecordKey = "MediumRecord";
    private readonly string _hardRecordKey = "HardRecord";

    private readonly List<int> _listOfLvlDiff = new List<int>(3) { 500, 2000, 5000 };

    [SerializeField]
    private List<TMP_Text> _listOfRecords = new List<TMP_Text>(3);

    [SerializeField]
    private Button _mediumButton;

    [SerializeField]
    private Button _hardButton;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey(_levelKey))
            PlayerPrefs.SetInt(_levelKey, 0);

        if (!PlayerPrefs.HasKey(_maxLevelKey))
            PlayerPrefs.SetInt(_maxLevelKey, 0);

        if (!PlayerPrefs.HasKey(_easyRecordKey))
            PlayerPrefs.SetInt(_easyRecordKey, 0);

        if (!PlayerPrefs.HasKey(_mediumRecordKey))
            PlayerPrefs.SetInt(_mediumRecordKey, 0);

        if (!PlayerPrefs.HasKey(_hardRecordKey))
            PlayerPrefs.SetInt(_hardRecordKey, 0);
    }

    private void Start()
    {
        int maxLevel = PlayerPrefs.GetInt(_maxLevelKey);
        
        if (maxLevel == 0)
        {
            _mediumButton.interactable = false;
            _hardButton.interactable = false;
        }
        else if (maxLevel == 1) 
        {
            _mediumButton.interactable = true;
            _hardButton.interactable = false;
        }
        else
        {
            _mediumButton.interactable = true;
            _hardButton.interactable = true;
        }

        List<int> _currRecords = new List<int>(3) { PlayerPrefs.GetInt(_easyRecordKey), PlayerPrefs.GetInt(_mediumRecordKey), PlayerPrefs.GetInt(_hardRecordKey) };

        for (int i = 0; i < _listOfRecords.Count; i++)
        {
            _listOfRecords[i].text = $"{_currRecords[i]} / {_listOfLvlDiff[i]}m";
        }
    }

    public void SetLevel(int lvl)
    {
        int maxLevel = PlayerPrefs.GetInt(_maxLevelKey);

        if (lvl <= maxLevel)
        {
            PlayerPrefs.SetInt(_levelKey, lvl);

            SceneController.LoadSceneByName("Game");
        }
    }
}
