using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraShake : MonoBehaviour
{
    [SerializeField] private float shakeForce;
    [SerializeField] private float time;
    private void Start()
    {
        Onshake();
    }
    

    public void Onshake()
    {
        iTween.ShakePosition(gameObject,
            iTween.Hash(
                "x", 0.05,"y",0.095,
                "time", 500f
            ));
         iTween.ShakeRotation(gameObject,
             iTween.Hash(
                 "x", 0.095,"y",0.05,
                 "time", 500f
             ));
    }
}
