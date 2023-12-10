using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CrontrollerChat : MonoBehaviour
{
    [SerializeField] private Image imagesContact;
    [SerializeField] private GameObject prefabMessage;
    [SerializeField] private TMP_InputField inputMessage;
    [SerializeField] private Transform contentMessage;
    [SerializeField] private ContactData _contactData;
    [SerializeField] private ControllerPrefabMessage message;
    [SerializeField] private Image imagesForInstantiate;
    public List<string> longAnswers=new List<string>(); 
    public List<string>  shortAnswers=new List<string>();

    public void SetChat(ContactData contactData)
    {
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
    public void SendMessage()
    {
        message = Instantiate(prefabMessage, contentMessage, false).GetComponent<ControllerPrefabMessage>();
        message.SetMessage(Color.blue,inputMessage.text);
        inputMessage.text = "";
        StartCoroutine(WaitingAnswer());
    }

    IEnumerator WaitingAnswer()
    {
        yield return new WaitForSeconds(0.5f);
        setMessageNPC();
    }
    public void setMessageNPC()
    {
        if (longAnswers.Count > 0||shortAnswers.Count > 0)
        {
            int split = message.Messages.text.Length;
            if (split >= 10)
            {
                if (longAnswers.Count > 0)
                {
                    int ramdon = Random.Range(0, longAnswers.Count);
                    message = Instantiate(prefabMessage, contentMessage, false).GetComponent<ControllerPrefabMessage>();
                    message.SetMessage(Color.black, longAnswers[ramdon]);
                    longAnswers.Remove(longAnswers[ramdon]);
                }
                else
                {
                    Instantiate(imagesForInstantiate, contentMessage, false);
                }
            }

            if (split < 10)
            {
                if (shortAnswers.Count > 0)
                {
                    int ramdon = Random.Range(0, shortAnswers.Count);
                    message = Instantiate(prefabMessage, contentMessage, false).GetComponent<ControllerPrefabMessage>();
                    message.SetMessage(Color.black, shortAnswers[ramdon]);
                    shortAnswers.Remove(shortAnswers[ramdon]);
                }
                else
                {
                    Instantiate(imagesForInstantiate, contentMessage, false);
                }
            }
        }
        else
        {
            Instantiate(imagesForInstantiate, contentMessage, false);
        }
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
