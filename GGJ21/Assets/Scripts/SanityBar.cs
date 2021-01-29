﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SanityBar : MonoBehaviour
{
    public Slider slider;
    
    public void SetMaxSenity(float senity)
    {
        slider.maxValue = senity;
        slider.value = senity;
    }

    public void SetSenity(float senity)
    {
        slider.value = senity;
    }
}
