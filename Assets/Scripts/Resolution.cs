using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Resolution : MonoBehaviour
{
    public Toggle toggle;

    private void Start()
    {
        toggle.isOn = Screen.fullScreen;
    }
    public void fulscr()
    {
        bool togglevalue = toggle.isOn;
        if (togglevalue)
        {
            Screen.SetResolution(1920, 1080, true);
        }
        else
        {
            Screen.SetResolution(1280, 720, false);
        }
    }
}
