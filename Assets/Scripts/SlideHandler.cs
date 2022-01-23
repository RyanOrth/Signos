using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlideHandler : MonoBehaviour
{
    public Slider slider;

    IEnumerator Start()
    {
        slider.value = 0.0f;
        float value = 0.0f;

        while (value <= 100.0f)
        {
            yield return new WaitForSeconds(0.1f);
            UpdateSlider(value);
            value += 1.0f;
        }

    }

    // void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.E))
    //     {
    //         UpdateSlider(slider.value + 1);
    //     }
    // }

    void UpdateSlider(float value)
    {
        slider.value = value;
    }

}