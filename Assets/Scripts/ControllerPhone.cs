using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ControllerPhone : MonoBehaviour
{
    [SerializeField] private GameObject prefabContac;
    [SerializeField] private ListDataContact _listDataContact;
    [SerializeField] private Transform contentContact;
    [SerializeField] private CrontrollerChat chat;
    [SerializeField] private ControllerCall call;
    [SerializeField] private ControllerPurchasing Purchasing;
    [SerializeField] private float timeForLoadSceneScare;
    [SerializeField] private float randomInitTimeForLoadSceneScare;
    [SerializeField] private float randomEndTimeForLoadSceneScare;
    [SerializeField] private UnityEvent eventsSound;

    [SerializeField] private AudioSource _audioSource;

    [SerializeField] private AudioClip clipSelectContact;
    [SerializeField] private AudioClip clipButton;
    [SerializeField] private AudioClip clipCall;
    [SerializeField] private AudioClip clipCallEnd;
    // Start is called before the first frame update
    void Start()
    {
        //FactoryContact();
    }

    public void FactoryContact()
    {
        StartCoroutine(Factory());
    }

    public IEnumerator Factory()
    {
        foreach (var contact in _listDataContact.listContact)
        {
            GameObject cont = Instantiate(prefabContac, contentContact, false);
            cont.GetComponent<ControllerContact>().DataContact = contact;
            cont.GetComponent<ControllerContact>().StartContact();
            cont.GetComponent<ControllerContact>().ControllerPhone=this;
        }

        yield return new WaitForSeconds(0.01f);
    }
    public void InitChat(ContactData contactData)
    {

        chat.SetChat(contactData);
    }
    public void InitCall(ContactData contactData)
    {
        
        call.SetCall(contactData,this);
        SoundInitCall();
        call.gameObject.SetActive(true);
    }
    public IEnumerator LoadScene(ContactData contactData,UnityEvent callback=null)
    {
        int callEndTime = Random.Range(0, 10);
        if (callEndTime==1)
        {
            StartCoroutine(WaitCallEnd());
        }
        else
        {
            timeForLoadSceneScare = Random.Range(randomInitTimeForLoadSceneScare, randomEndTimeForLoadSceneScare);
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(contactData.nameScene, LoadSceneMode.Additive);
            yield return new WaitWhile(() => !asyncLoad.isDone);
            yield return new WaitForSeconds(timeForLoadSceneScare);
            _audioSource.Stop();
            call.ChangeVistCam();
            ControlladorScare controller= FindObjectOfType<ControlladorScare>();
            call.ButtonCapture.GetComponent<Button>().onClick.RemoveAllListeners();
            call.ButtonCapture.GetComponent<Button>().onClick.AddListener(controller.UpdateCam);
            controller.UpdateCam();
            call.activateButton(true);
        }

    }

    IEnumerator WaitCallEnd()
    {
        yield return new WaitForSeconds(2);
        CallEnd();
    }
    public void CallEnd()
    {
        SoundCallEnd();
        ControlladorScare controlladorScare=FindObjectOfType<ControlladorScare>();
        if (controlladorScare!=null)
        {
            string message = controlladorScare.CanEndTheCall();
            if (message=="true")
            {
                eventsSound.Invoke();
                call.activateButton(false);
                call.ChangeVist();
                SoundCallEnd();
                StartCoroutine(DeleteScenAdditive());
            }
            else
            {
                call.NotificationNotEndCall(message);
            }
        }

        else
        {
            eventsSound.Invoke();
            call.activateButton(false);
            call.ChangeVist();
            SoundCallEnd();
        }
    }
    public IEnumerator DeleteScenAdditive()
    {
        AsyncOperation asyncLoad = SceneManager.UnloadSceneAsync(call._contactData.nameScene);
        yield return new WaitWhile(() => !asyncLoad.isDone);
        yield return new WaitForSeconds(5f);
    }

    public void ChangeStateContact()
    {
        foreach (var contact in contentContact.GetComponentsInChildren<ControllerContact>())
        {
            contact.DeselectContact();
        }
    }
    public void SetPanelpurchasing(ContactData contactData)
    {
        Purchasing.StartPurchasing(contactData);
    }

    public void SoundSelectContact()
    {
        _audioSource.clip = clipSelectContact;
        _audioSource.Play();
    }
    public void SoundClickButton()
    {
        _audioSource.clip = clipButton;
        _audioSource.Play();
    }
    public void SoundInitCall()
    {
        _audioSource.clip = clipCall;
        _audioSource.Play();
    }
    public void SoundCallEnd()
    {
        _audioSource.clip = clipCallEnd;
        _audioSource.Play();
    }
}
