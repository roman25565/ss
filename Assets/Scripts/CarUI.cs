using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CarUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Button button;
    [SerializeField] private Image _image;
    [SerializeField] private Sprite[] _sprites;
    private int _lvl;

    private int Lvl
    {
        get { return _lvl; }
        set 
        {
            _lvl = value;
            PlayerPrefs.SetInt(LvlKey, _lvl);
        }
    }
    private const string LvlKey = "lvl";
    
    private int _costUpgrade;
    private int CostUpgrade
    {
        get { return _costUpgrade; }
        set 
        {
            _costUpgrade = value;
            text.text = "upgrade cost: " + _costUpgrade + "$";
        }
    }
    void Start()
    {
        Lvl = PlayerPrefs.GetInt(LvlKey, 0) != 0 ? PlayerPrefs.GetInt(LvlKey, 0) : 1;;
#if UNITY_EDITOR
        Lvl = 1;
#endif
        _image.sprite = _sprites[Lvl];
        
        CostUpgrade = 1000;
        for (int i = 1; i < Lvl; i++)
        {
            CostUpgrade *= 10;
        }
    }

    public void Buy()
    {
        GameManager.Instance.Gold -= CostUpgrade;
        CostUpgrade *= 10;
        Lvl++;
        _image.sprite = _sprites[Lvl];
        UpdateButtonInteractable();
    }

    private void UpdateButtonInteractable()
    {
        button.interactable = GameManager.Instance.Gold > CostUpgrade;
    }

    private void OnEnable()
    {
        UpdateButtonInteractable();
    }
}
