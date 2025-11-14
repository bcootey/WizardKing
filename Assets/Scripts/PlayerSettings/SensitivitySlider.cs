using UnityEngine;
using UnityEngine.UI;

public class SensitivitySlider : MonoBehaviour
{
    public Slider slider;

    void Start()
    {
        slider.value = SensitivityManager.Sensitivity;
        slider.onValueChanged.AddListener(SetSensitivity);
    }

    void SetSensitivity(float value)
    {
        SensitivityManager.Sensitivity = value;
    }
}