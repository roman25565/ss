using TMPro;
using UnityEngine;

public class GoldUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    
    public void SetUI(int gold)
    {
        text.text = gold.ToString();
    }
}