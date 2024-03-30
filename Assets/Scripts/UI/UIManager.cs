using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _menu; 
    [SerializeField] private GameObject _settings; 
    [SerializeField] private GameObject _game;

    private void Start()
    {
        GoToMenu();
    }

    public void GoToMenu()
    {
        DeActiveAll();
        _menu.SetActive(true);
    }
    public void GoToSettings()
    {
        DeActiveAll();
        _settings.SetActive(true);
    }
    public void GoToGame()
    {
        DeActiveAll();
        _game.SetActive(true);
    }

    private void DeActiveAll()
    {
        _menu.SetActive(false);
        _settings.SetActive(false);
        _game.SetActive(false);
    }
}