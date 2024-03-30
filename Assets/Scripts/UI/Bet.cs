using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Bet : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI sliderText;
    [HideInInspector] public float value;
    public void Init(float min, float max)
    {
        slider.minValue = min;
        slider.maxValue = max;
        slider.onValueChanged.AddListener((v) =>
        {
            sliderText.text = v.ToString("0.00");
            value = v;
        });
    }
}