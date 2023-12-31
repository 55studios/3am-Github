using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class ControlladorScare : MonoBehaviour
{
    [SerializeField] private List<GameObject> pointMoveCamera = new List<GameObject>();
    [SerializeField] private GameObject camScare;
    [SerializeField] private GameObject camActive;
    [SerializeField] private Canvas panelTvStatic;
    [SerializeField] private int timeWait;
    [SerializeField] private List<string> listMessageNoEndCall = new List<string>();
    [SerializeField] private Animator _animator;
    [SerializeField] private float timeWaitInitChangeCamera;
    [SerializeField] private float timeWaitEndChangeCamera;
    private float time;
    private float timeActivateButton;
    public void ActivateChangeCamAutomatic()
    {
        StartCoroutine(ChangeCamAutomatic());
    }
    public IEnumerator ChangeCamAutomatic()
    {
        time = Random.Range(timeWaitInitChangeCamera, timeWaitEndChangeCamera);
        timeActivateButton = time - 4;
        yield return new WaitForSeconds(timeActivateButton);
        FindObjectOfType<ControllerCall>().ActivateButtonChangeCam(true);
        yield return new WaitForSeconds(time-timeActivateButton);
        UpdateCam();
    }
    public void UpdateCam()
    {
        StopAllCoroutines();
        StartCoroutine(MoveCamWithTime());
    }

    IEnumerator MoveCamWithTime()
    {
        yield return new WaitForSeconds(0.5f);
        FindObjectOfType<ControllerCall>().ActivateButtonChangeCam(false);
        if (!camScare.activeInHierarchy)
        {

            if (pointMoveCamera.Count==0)
            {
                panelTvStatic.gameObject.SetActive(true);
                panelTvStatic.GetComponent<AudioSource>().Play();
                RandomPaneLTV();
                camActive.GetComponent<ControllerCamera>().DesactivateCamera();
                camActive = camScare;
            }
            else
            {
                panelTvStatic.gameObject.SetActive(true);
                panelTvStatic.GetComponent<AudioSource>().Play();
                RandomPaneLTV();
                camActive.GetComponent<ControllerCamera>().DesactivateCamera();
                camActive = pointMoveCamera[Random.Range(0, pointMoveCamera.Count)];
            }
            panelTvStatic.worldCamera = camActive.GetComponent<Camera>();
            camActive.SetActive(true);
            yield return new WaitForSeconds(1f);
            camActive.GetComponent<ControllerCamera>().ActivateCamera(panelTvStatic);
            panelTvStatic.gameObject.SetActive(false);
            panelTvStatic.GetComponent<AudioSource>().Stop();
            pointMoveCamera.Remove(camActive);
            if (camScare.activeInHierarchy)
            {
                yield return new WaitForSeconds(3);
                FindObjectOfType<ControllerPhone>().CallEnd();
            }
        }
        ActivateChangeCamAutomatic();
    }

    private void RandomPaneLTV()
    {
        int randomStatic = Random.Range(0, 3);
        switch (randomStatic)
        {
            case 0:
                _animator.SetBool("State1", true);
                break;
            case 1:
                _animator.SetBool("State2", true);
                break;
            case 3:
                _animator.SetBool("State3", true);
                break;
            default:
                _animator.SetBool("State1", true);
                break;
        }
    }

    public string CanEndTheCall()
    {
        if (camScare.activeInHierarchy)
        {
            return "true";
        }
        else
        {
            int message= Random.Range(0, listMessageNoEndCall.Count);
            return listMessageNoEndCall[message];
        }
    }

    public void CapturePhoto()
    {
        camActive.GetComponent<ControllerCamera>().PhotoCapture();
    }
}
