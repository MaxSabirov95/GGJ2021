﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformFall : MonoBehaviour
{
    Rigidbody2D platformRB;
    [SerializeField] float fallMultiplier;

    private void Start()
    {
        platformRB = GetComponent<Rigidbody2D>();
        GetComponentInChildren<VinePlatform>().OnVineDestroyed += Fall;
    }

    void Fall()
    {
        platformRB.isKinematic = false;
        platformRB.velocity += Physics2D.gravity * (fallMultiplier - 1) * Time.fixedDeltaTime;
    }
}
