using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenResButton : MonoBehaviour
{
    [SerializeField] enum Size { Smallest, Smaller, Small, Large, Larger, Largest };
    [SerializeField] Size screenFit;


    public void SetRes()
    {
        switch (screenFit)
        {
            case Size.Smallest:
                Screen.SetResolution(1024, 576, false);
                break;
            case Size.Smaller:
                Screen.SetResolution(1156, 648, false);
                break;
            case Size.Small:
                Screen.SetResolution(1280, 720, false);
                break;
            case Size.Large:
                Screen.SetResolution(1366, 768, false);
                break;
            case Size.Larger:
                Screen.SetResolution(1600, 900, false);
                break;
            case Size.Largest:
                Screen.SetResolution(1920, 1080, false);
                break;
        }
    }

    public void Window()
    {       
        if (Screen.fullScreen == false)
        {
            Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
        }
        else if (Screen.fullScreen == true)
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
        }
    }
}