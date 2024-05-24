using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundController : MonoBehaviour
{
    [SerializeField]
    private List<Sprite> _lvlBacks = new List<Sprite>(3);

    [SerializeField]
    private Image _lvlBack;

    private readonly string _levelKey = "Level";

    private int _currentLevel;

    private void Awake()
    {
        _currentLevel = PlayerPrefs.GetInt(_levelKey);
        
        switch (_currentLevel)
        {
            case 0:
                _lvlBack.sprite = _lvlBacks[0];
                break;
            case 1:
                _lvlBack.sprite = _lvlBacks[1];
                break;
            case 2:
                _lvlBack.sprite = _lvlBacks[2];
                break;
            default:
                _lvlBack.sprite = _lvlBacks[0];
                break;
        }
    }
}
