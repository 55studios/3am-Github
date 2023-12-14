using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaitPoint : MonoBehaviour
{
    [SerializeField] private TMP_Text point;
    private void Start()
    {
        point = GetComponent<TMP_Text>();
    }

    public void Waiting1()
    {
        point = GetComponent<TMP_Text>();
        point.text = ".";
    }
    public void Waiting2()
    {
        point.text = "..";
    }
    public void Waiting3()
    {
        point.text = "...";
    }
    public void Waiting4()
    {
        point.text = "0";
    }
}
