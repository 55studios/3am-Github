using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ControllerCall : MonoBehaviour
{
    [SerializeField] private Image imageContact;
    [SerializeField] private Image backGround;
    [SerializeField] private GameObject camRender;
    [SerializeField] private GameObject panelCall;
    [SerializeField] private GameObject buttonCapture;
    [SerializeField] private TMP_Text textTotalCapture;
    [SerializeField] private int captures;
    [SerializeField] private int capturesTotal;
    [SerializeField] private GameObject buttonChangeCamera;
    [SerializeField] private TMP_Text textNotEnCall;
    [SerializeField] private Button buttonEndCall; 
    public ContactData _contactData { get; private set; }
    [SerializeField] private bool notificationActive;
    [SerializeField] private float timeCallWait;
    [SerializeField] private TMP_Text messageCall;
    [SerializeField] private GameObject animationPoint;
    [SerializeField] private UnityEvent eventsSound;

    public GameObject ButtonCapture
    {
        get => buttonChangeCamera;
    }

    public void ActivateButtonChangeCam(bool state)
    {
        buttonChangeCamera.GetComponent<Button>().interactable = state;
    }
    public void SetCall(ContactData contactData, ControllerPhone controllerPhone)
    {
        camRender.SetActive(false);
        panelCall.SetActive(true);
        eventsSound.Invoke();
        _contactData = contactData;
        imageContact.sprite = _contactData.spriteContact;
        backGround.sprite = _contactData.spriteContact;
        gameObject.SetActive(true);
        messageCall.text = _contactData.nameContact;
        animationPoint.SetActive(true);
        StartCoroutine(controllerPhone.LoadScene(contactData));
    }

    public void ChangeVist()
    {
        camRender.SetActive(false);
        panelCall.SetActive(true);
        EndCallMessaje();
        StartCoroutine(WaitEndCall());
    }

    IEnumerator WaitEndCall()
    {
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);
    }
    public void ChangeVistCam()
    {
        camRender.SetActive(true);
        panelCall.SetActive(false);
    }

    public void activateButton(bool state)
    {
        buttonChangeCamera.SetActive(state);
        buttonCapture.SetActive(state);
        buttonEndCall.interactable = state;
        textTotalCapture.gameObject.SetActive(state);
        textTotalCapture.text = captures + "/" + capturesTotal;
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

    public void EndCallMessaje()
    {
        messageCall.text = "Call End";
        animationPoint.SetActive(false);
    }

    public void CapturePhoto()
    {
        if (captures<capturesTotal)
        {
            captures++;
            textTotalCapture.text = captures + "/" + capturesTotal;
        }
    }
}
