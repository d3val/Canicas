using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ForceBar : MonoBehaviour
{
    private bool up = true;
    [SerializeField] float increaseingSpeed = 1;
    private bool stop = false;
    private Slider slider;

    private void OnEnable()
    {
        slider = GetComponent<Slider>();
        slider.value = slider.minValue;
        stop = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!stop)
        {
            CheckBarValue();
            if (up)
            {
                IncreaseValue();
            }
            else
            {
                DecreaseValue();
            }
        }
    }

    private void IncreaseValue()
    {
        slider.value += Time.deltaTime * increaseingSpeed;
    }

    private void DecreaseValue()
    {
        slider.value -= Time.deltaTime * increaseingSpeed;
    }

    private void CheckBarValue()
    {
        if (slider.value >= slider.maxValue)
        {
            up = false;
        }
        else if (slider.value <= slider.minValue)
        {
            up = true;
        }
    }

    public float GetSliderValue()
    {
        return slider.value;
    }

    public void Stop()
    {
        stop = true;
    }

}
