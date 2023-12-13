using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    public Color32 colorMessageNpc;
    public Color32 colorMessagePlayer;
    public bool npcWritting;
    public int countMessage;
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
    public void SendMessage()
    {
        message = Instantiate(prefabMessagePlayer, contentMessage, false).GetComponent<ControllerPrefabMessage>();
        message.SetMessage(colorMessagePlayer,inputMessage.text);
        inputMessage.text = "";
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

    IEnumerator WaitNpcWrittin()
    {
        yield return new WaitForSeconds(0.5f);
        float timeWait = Random.Range(1,3);
        float time = 0;
        int counPoint = 0;
        string onePoint = ".";
        string TwoPoint = "..";
        string threePoint = "...";
        while (time<timeWait)
        {
            if (counPoint==0)
            {
                npcAnimationWriting.text = _contactData.nameContact + onePoint;
                counPoint++;
            }
            else if (counPoint==1)
            {
                npcAnimationWriting.text = _contactData.nameContact + TwoPoint;
                counPoint++;
            }
            else
            {
                npcAnimationWriting.text = _contactData.nameContact + threePoint;
                counPoint=0;
            }
            yield return new WaitForSeconds(0.80f);
            time += 0.1f;
        }
        npcAnimationWriting.text = " ";
        setMessageNPC();
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
