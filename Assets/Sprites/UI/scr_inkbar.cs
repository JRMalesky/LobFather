using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class scr_inkbar : MonoBehaviour
{
    //Slider 0-5
    public Slider S_slider;

	public bool ChangeInk(float amount)
    {
        bool ret = false;

        if (S_slider.value < S_slider.maxValue && amount > 0)
        {
            S_slider.value += amount;
            ret = true;
        }
        else if (S_slider.value > S_slider.minValue && amount < 0)
        {
            S_slider.value += amount;
            ret = true;
        }

        return ret;
    }
}
