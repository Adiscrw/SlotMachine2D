using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class SliderChange : MonoBehaviour
{
    public UnityEngine.UI.Slider betSlider;
    public TMP_Text betValue;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        betSlider.onValueChanged.AddListener(UpdateValue);
        UpdateValue(betSlider.value);
    }

    // Update is called once per frame
    void UpdateValue(float value)
    {
        if (GlogalData.TotalScore > value)
        {
            GlogalData.Bet = (int)value;
            betValue.text = GlogalData.Bet.ToString();
        }
    }
}
