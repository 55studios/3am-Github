using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraShake : MonoBehaviour
{
    [SerializeField] private float shakeForcePositionX;
    [SerializeField] private float shakeForcePositionY;
    [SerializeField] private float shakeForcePositionZ;
    [SerializeField] private float shakeForcePositionRotationX;
    [SerializeField] private float shakeForcePositionRotationY;
    [SerializeField] private float shakeForcePositionRotationZ;
    [SerializeField] private float timeShakePost;
    [SerializeField] private float timeShakeRot;
    private void Start()
    {
        Onshake();
    }
    

    public void Onshake()
    {
        iTween.ShakePosition(gameObject,
            iTween.Hash(
                "x", shakeForcePositionX,"y",shakeForcePositionY,"Z",shakeForcePositionZ,
                "time", timeShakePost
            ));
         iTween.ShakeRotation(gameObject,
             iTween.Hash(
                 "x", shakeForcePositionRotationX,"y",shakeForcePositionRotationY,"Z",shakeForcePositionRotationZ,
                 "time", timeShakeRot
             ));
    }
}
