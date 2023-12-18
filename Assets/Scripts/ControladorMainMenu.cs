using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorMainMenu : MonoBehaviour
{
    [SerializeField] private CanvasGroup loadPanel;
    [SerializeField] private ControllerPhone _controllerPhone;
    [SerializeField] private float timeWaitScreenLoad;
    [SerializeField] private AudioSource audioSource;

    private void Start()
    {
        StartCoroutine(InitialLoad());
    }

    IEnumerator InitialLoad()
    {
        yield return _controllerPhone.Factory();
        yield return new WaitForSeconds(timeWaitScreenLoad);
        while (loadPanel.alpha>0.01f)
        {
            loadPanel.alpha -= 0.01f;
            audioSource.volume -= 0.01f;
            yield return new WaitForSeconds(0.01f);
        }
        loadPanel.gameObject.SetActive(false);
    }
}
