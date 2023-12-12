using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControllerCall : MonoBehaviour
{
    [SerializeField] private Image imageContact;
    [SerializeField] private Image backGround;
    [SerializeField] private GameObject camRender;
    [SerializeField] private GameObject panelCall;
    [SerializeField] private GameObject buttonCapture;
    [SerializeField] private TMP_Text textNotEnCall;
    [field: SerializeField] public ContactData Data { get; private set; }
    [SerializeField] private ControllerPhone _controllerPhone;
    [SerializeField] private bool notificationActive;
    [SerializeField] private UnityEvent eventsSound;
    public GameObject ButtonCapture
    {
        get => buttonCapture;
    }
    
    public void SetCall(ContactData contactData, ControllerPhone controllerPhone)
    {
        _controllerPhone = controllerPhone;
        camRender.SetActive(false);
        panelCall.SetActive(true);
        eventsSound.Invoke();
        Data = contactData;
        imageContact.sprite = Data.spriteContact;
        backGround.sprite = Data.spriteContact;
        gameObject.SetActive(true);
        StartCoroutine(controllerPhone.LoadScene(contactData));
    }

    public void ChangeVist()
    {
        camRender.SetActive(false);
        panelCall.SetActive(true);
    }

    public void ChangeVistCam()
    {
        camRender.SetActive(true);
        panelCall.SetActive(false);
    }

    public void activateButton(bool state)
    {
        buttonCapture.SetActive(state);
    }

    public void NotificationNotEndCall(string message)
    {
        if (notificationActive==false)
        {
            notificationActive = true;
            textNotEnCall.color = new Color(255,255,255,1);
            textNotEnCall.text = message;
            StartCoroutine(WaitEndNotification()); 
        }
    }

    IEnumerator WaitEndNotification()
    {
        yield return new WaitForSeconds(3);
        textNotEnCall.color = new Color(255,255,255,0);
        notificationActive = false;
    }
}
