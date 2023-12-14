using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CrontrollerChat : MonoBehaviour
{
    [SerializeField] private Image imagesContact;
    [SerializeField] private GameObject prefabMessageNPC;
    [SerializeField] private GameObject prefabMessagePlayer;
    [SerializeField] private TMP_InputField inputMessage;
    [SerializeField] private Transform contentMessage;
    [SerializeField] private ContactData _contactData;
    [SerializeField] private ControllerPrefabMessage message;
    [SerializeField] private Image imagesForInstantiate;
    public List<string> longAnswers=new List<string>(); 
    public List<string>  shortAnswers=new List<string>();
    public TMP_Text npcAnimationWriting;
    public GameObject npcWaitAnimation;

    public bool npcWritting;
    public int countMessage;
    [Header("Config time Chat")]

    [SerializeField] private  float timeRamdonInitNPCWriting;
    [SerializeField] private  float timeRamdonEndNPCWriting;
    [SerializeField] private  float timeWaitNPCWriting;
    [SerializeField] private  float timeScale;
    
    [Header("Config Color")]
    public Color32 colorMessageNpc;
    public Color32 colorMessagePlayer;
    public void SetChat(ContactData contactData)
    {
        countMessage = 0;
        _contactData = contactData;
        imagesContact.sprite = _contactData.spriteContact;
        foreach (var VARIABLE in contactData.longAnswers)
        {
            longAnswers.Add(VARIABLE);
        }
        foreach (var VARIABLE in contactData.shortAnswers)
        {
            shortAnswers.Add(VARIABLE);
        }
        gameObject.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (!string.IsNullOrEmpty(inputMessage.text))
            {
                SendMessage();
            }
        }
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            if (!string.IsNullOrEmpty(inputMessage.text))
            {
                SendMessage();
            }
        }
    }

    public void SendMessage()
    {
        message = Instantiate(prefabMessagePlayer, contentMessage, false).GetComponent<ControllerPrefabMessage>();
        message.SetMessage(colorMessagePlayer,inputMessage.text);
        inputMessage.text = "";
        inputMessage.ActivateInputField();
        WaitingAnswer();
    }

    void WaitingAnswer()
    {
        if (npcWritting==true)
        {
            return;
        }
        npcWritting = true;
        StartCoroutine(WaitNpcWrittin());
        
    }
    IEnumerator WaitNpcWrittin()
    {
        yield return new WaitForSeconds(timeWaitNPCWriting);
        float timeWait = Random.Range(timeRamdonInitNPCWriting,timeRamdonEndNPCWriting);
        npcAnimationWriting.text = _contactData.nameContact;
        npcWaitAnimation.SetActive(true);
        float time = 0;
        int counPoint = 0;
        while (time<timeWait)
        {
            yield return new WaitForSeconds(0.05f);
            time += timeScale;
        }
        npcAnimationWriting.text = " ";
        npcWaitAnimation.SetActive(false);
        setMessageNPC();
    }
   //IEnumerator RutineName
    public void setMessageNPC()
    {

        if (longAnswers.Count > 0||shortAnswers.Count > 0)
        {
            int split = message.Messages.text.Length;
            if (split >= 6)
            {
                if (longAnswers.Count > 4)
                {
                    int ramdon = Random.Range(0, longAnswers.Count);
                    message = Instantiate(prefabMessageNPC, contentMessage, false).GetComponent<ControllerPrefabMessage>();
                    message.SetMessage(colorMessageNpc, longAnswers[ramdon]);
                    longAnswers.Remove(longAnswers[ramdon]);
                }
                else
                {
                   Image imagescare= Instantiate(imagesForInstantiate, contentMessage, false);
                   imagescare.sprite = _contactData.spriteScare[Random.Range(0, _contactData.spriteScare.Count)];
                }
            }
            if (split < 6)
            {
                if (shortAnswers.Count > 4)
                {
                    int ramdon = Random.Range(0, shortAnswers.Count);
                    message = Instantiate(prefabMessageNPC, contentMessage, false).GetComponent<ControllerPrefabMessage>();
                    message.SetMessage(Color.black, shortAnswers[ramdon]);
                    shortAnswers.Remove(shortAnswers[ramdon]);
                }
                else
                {
                    Image imagescare= Instantiate(imagesForInstantiate, contentMessage, false);
                    imagescare.sprite = _contactData.spriteScare[Random.Range(0, _contactData.spriteScare.Count)];
                }
            }

            if (countMessage>6)
            {
                int inten = Random.Range(0, 2);
                if(inten==1)
                {
                    Image imagescare= Instantiate(imagesForInstantiate, contentMessage, false);
                    imagescare.sprite = _contactData.spriteScare[Random.Range(0, _contactData.spriteScare.Count)];
                }
            }
        }
        else
        {
            npcWritting = true;
            Image imagescare= Instantiate(imagesForInstantiate, contentMessage, false);
            imagescare.sprite = _contactData.spriteScare[Random.Range(0, _contactData.spriteScare.Count)];
        }
        npcWritting = false;
        countMessage++;
        
    }

    
    public void CloseChat()
    {
        _contactData = null;
        longAnswers.Clear();
        shortAnswers.Clear();
        foreach (var message in contentMessage.transform.GetComponentsInChildren<ControllerPrefabMessage>())
        {
            message.DestroyMessage();
        }
        gameObject.SetActive(false);
    }
}
