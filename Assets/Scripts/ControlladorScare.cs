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
                int randomStatic = Random.Range(0, 3);
                switch (randomStatic)
                {
                    case 0:
                        _animator.SetBool("State1",true);
                        break;
                    case 1:
                        _animator.SetBool("State2",true);
                        break;
                    case 3:
                        _animator.SetBool("State3",true);
                        break;
                    default:
                        _animator.SetBool("State1",true);
                        break;
                }
                camActive.SetActive(false);
                camActive = camScare;
                panelTvStatic.worldCamera = camActive.GetComponent<Camera>();
                camActive.SetActive(true);
                pointMoveCamera.Remove(camActive);

            }
            else
            {
                panelTvStatic.gameObject.SetActive(true);
                int randomStatic = Random.Range(0, 3);
                switch (randomStatic)
                {
                    case 0:
                        _animator.SetBool("State1",true);
                        break;
                    case 1:
                        _animator.SetBool("State2",true);
                        break;
                    case 3:
                        _animator.SetBool("State3",true);
                        break;
                    default:
                        _animator.SetBool("State1",true);
                        break;
                }
                camActive.SetActive(false);
                camActive = pointMoveCamera[Random.Range(0, pointMoveCamera.Count)];
                camActive.SetActive(true);
                panelTvStatic.worldCamera = camActive.GetComponent<Camera>();
                pointMoveCamera.Remove(camActive);
            }

            yield return new WaitForSeconds(1f);
            panelTvStatic.gameObject.SetActive(false);
            if (camScare.activeInHierarchy)
            {
                yield return new WaitForSeconds(3);
                FindObjectOfType<ControllerPhone>().CallEnd();
            }
        }
        ActivateChangeCamAutomatic();
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
}
