using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControllerPhone : MonoBehaviour
{
    [SerializeField] private GameObject prefabContac;
    [SerializeField] private ListDataContact _listDataContact;
    [SerializeField] private Transform contentContact;
    [SerializeField] private CrontrollerChat chat;
    [SerializeField] private ControllerCall call;
    [SerializeField] private ControllerPurchasing Purchasing;

    [SerializeField] private UnityEvent eventsSound;
    // Start is called before the first frame update
    void Start()
    {
        FactoryContact();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FactoryContact()
    {
        foreach (var contact in _listDataContact.listContact)
        {
            GameObject cont = Instantiate(prefabContac, contentContact, false);
            cont.GetComponent<ControllerContact>().DataContact = contact;
            cont.GetComponent<ControllerContact>().StartContact();
            cont.GetComponent<ControllerContact>().ControllerPhone=this;
        }
    }

    public void InitChat(ContactData contactData)
    {
        chat.SetChat(contactData);
    }
    public void InitCall(ContactData contactData)
    {
        call.SetCall(contactData,this);
        call.gameObject.SetActive(true);
    }
    public IEnumerator LoadScene(ContactData contactData,UnityEvent callback=null)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(contactData.nameScene, LoadSceneMode.Additive);
        yield return new WaitWhile(() => !asyncLoad.isDone);
        yield return new WaitForSeconds(5f);
        call.ChangeVistCam();
        ControlladorScare controller= FindObjectOfType<ControlladorScare>();
        call.ButtonCapture.GetComponent<Button>().onClick.RemoveAllListeners();
        call.ButtonCapture.GetComponent<Button>().onClick.AddListener(controller.UpdateCam);
        controller.UpdateCam();
        call.activateButton(true);
    }

    public void CallEnd()
    {
        string message = FindObjectOfType<ControlladorScare>().CanEndTheCall();
        if (message=="true")
        {
            eventsSound.Invoke();
            call.activateButton(false);
            call.ChangeVist();
            StartCoroutine(DeleteScenAdditive());
        }
        else
        {
            call.NotificationNotEndCall(message);
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
}
