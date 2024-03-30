using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] private GameObject _slots; 
    [SerializeField] private GameObject _cliker;
    [SerializeField] private GameObject _car;
    [SerializeField] private Button _slotsB; 
    [SerializeField] private Button _clikerB;
    [SerializeField] private Button _carB;
    
    public void ToCar()
    {
        DeactiveAllGO();
        ActiveAllButtons();
        _car.SetActive(true);
        _carB.interactable = false;
    }
    public void ToSlots()
    {
        DeactiveAllGO();
        ActiveAllButtons();
        _slots.SetActive(true);
        _slotsB.interactable = false;
    }
    public void ToCliker()
    {
        DeactiveAllGO();
        ActiveAllButtons();
        _cliker.SetActive(true);
        _clikerB.interactable = false;
    }

    private void DeactiveAllGO()
    {
        _slots.SetActive(false);
        _cliker.SetActive(false);
        _car.SetActive(false);
    }

    private void ActiveAllButtons()
    {
        _slotsB.interactable = true;
        _clikerB.interactable = true;
        _carB.interactable = true;
    }
}