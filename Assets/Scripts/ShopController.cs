using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    [SerializeField]
    private int _multipliersMaxCount = 20;

    [SerializeField]
    private Button _batButton;

    [SerializeField]
    private Button _ballButton;

    [SerializeField]
    private TMP_Text _moneyText;

    private AudioController _audioController;

    private readonly string _multiplierKey = "Multiplier";

    private readonly string _multiplierCountKey = "MultiplierCount";

    private readonly string _moneyKey = "Money";

    private float _multiplier = 1f;

    private int _multiplierCount = 0;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey(_multiplierKey))
            PlayerPrefs.SetFloat(_multiplierKey, 1.0f);

        if (!PlayerPrefs.HasKey(_multiplierCountKey))
            PlayerPrefs.SetInt(_multiplierCountKey, 0);

        if (!PlayerPrefs.HasKey(_moneyKey))
            PlayerPrefs.SetInt(_moneyKey, 0);

        _multiplier = PlayerPrefs.GetFloat(_multiplierKey);

        _multiplierCount = PlayerPrefs.GetInt(_multiplierCountKey);

        _batButton.onClick.RemoveAllListeners();
        _batButton.onClick.AddListener(() => {
            BuyItem(250, 0.65f);
        });

        _ballButton.onClick.RemoveAllListeners();
        _ballButton.onClick.AddListener(() => {
            BuyItem(500, 0.85f);
        });

        _moneyText.text = PlayerPrefs.GetInt(_moneyKey).ToString();
    }

    private void Start()
    {
        _audioController = FindObjectOfType<AudioController>();
    }

    private void BuyItem(int itemCost, float multVal)
    {
        _multiplier = PlayerPrefs.GetFloat(_multiplierKey);
        _multiplierCount = PlayerPrefs.GetInt(_multiplierCountKey);
        int money = PlayerPrefs.GetInt(_moneyKey);

        if (_multiplierCount <= _multipliersMaxCount && money >= itemCost)
        {
            money = Convert.ToInt32(Mathf.Clamp(money - itemCost, 0f, Mathf.Infinity));
            PlayerPrefs.SetInt(_moneyKey, money);
            _moneyText.text = money.ToString();

            Debug.Log("Money count " + PlayerPrefs.GetInt(_moneyKey));

            _multiplierCount++;
            PlayerPrefs.SetInt(_multiplierCountKey, _multiplierCount);
            
            Debug.Log("Shop multiplier count " + _multiplierCount);

            _multiplier += multVal;
            PlayerPrefs.SetFloat(_multiplierKey, _multiplier);

            Debug.Log("Shop multiplier value " +  _multiplier);

            _audioController.PlayAsseptSound();
        }
        else
            _audioController.PlayDeclineSound();
    }
}
