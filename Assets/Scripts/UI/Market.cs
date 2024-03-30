using System;
using UnityEngine;

public class Market : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    public GameObject market;
    private float _cofX2;
    private float _cofX3;
    private const string CofX2Kay = "x2";
    private const string CofX3Kay = "x2";
    [SerializeField] private int costX2;
    [SerializeField] private int costX3;
    [SerializeField] private GameObject costX2UpgradeButton;
    [SerializeField] private GameObject costX3UpgradeButton;
    [SerializeField] private GameObject Buttons;
    public void MarketOn()
    {
        market.SetActive(true);
        UpgradeUI();
        Buttons.SetActive(false);
    }

    public void MarketOff()
    {
        market.SetActive(false);
        UpgradeUI();
        Buttons.SetActive(true);
    }

    public void UpgradeCofX2()
    {
        GameManager.Instance.Gold -= costX2;
        _cofX2 += 0.25f;
        UpgradeUI();
    }
    
    public void UpgradeCofX3()
    {
        GameManager.Instance.Gold -= costX3;
        _cofX3 += 0.25f;
        UpgradeUI();
    }
    
    private void UpgradeUI()
    {
        costX2UpgradeButton.SetActive(false);
        if (GameManager.Instance.Gold >= costX2)
        {
            costX2UpgradeButton.SetActive(true);
        }
        costX3UpgradeButton.SetActive(false);
        if (GameManager.Instance.Gold >= costX3)
        {
            costX3UpgradeButton.SetActive(true);
        }
    }
    
    private void Start()
    {
        _cofX2 = PlayerPrefs.GetFloat(CofX2Kay, 1.25f);
        _cofX3 = PlayerPrefs.GetFloat(CofX3Kay, 1.25f);
#if UNITY_EDITOR
        _cofX2 = 1.25f;
        _cofX3 = 1.25f;
#endif
        market.SetActive(false);
    }
}
