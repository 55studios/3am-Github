using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ControllerContact : MonoBehaviour
{
    [SerializeField] private Image imageBackGround;
    [SerializeField] private Image imageContact;
    [SerializeField] private TMP_Text nameContact;
    [SerializeField] private GameObject blockRayCast;
    [SerializeField] private GameObject panelButtons;
    [SerializeField] private Image ContactContainer;
    [SerializeField] private UnityEvent eventSound;
    private ContactData _dataContact;
    private ControllerPhone controllerPhone;

    [Header("Color Panel Settings")] 
    [Range(0.0f, 1f)] [SerializeField] private float alphaColorImageContact;
    [SerializeField] private Color32 colorContactContainer;
    [SerializeField] private Color32 colorSelect;
    [SerializeField] private Color32 colorDeselect;
    

    public ContactData DataContact
    {
        set => _dataContact = value;
    }
    public ControllerPhone ControllerPhone
    {
        set => controllerPhone = value;
    }
    public void StartContact()
    {
        imageContact.sprite = _dataContact.spriteContact;
        nameContact.text = _dataContact.nameContact;
        if (!_dataContact.purchasedContact)
        {
            var tempColor = imageContact.color;
            tempColor.a = alphaColorImageContact;
            imageContact.color = tempColor;
            ContactContainer.color = colorContactContainer;
            GetComponent<Image>().color = colorContactContainer;
        }
    }

    public void InitChat()
    {
        controllerPhone.SoundClickButton();
        controllerPhone.InitChat(_dataContact);
    }
    public void InitCall()
    {
        controllerPhone.SoundClickButton();
        controllerPhone.InitCall(_dataContact);
    }

    public void ChangeStateContact()
    {
        eventSound.Invoke();
        if (_dataContact.purchasedContact)
        {
            controllerPhone.ChangeStateContact();
            SelectContact();
        }
        else
        {
            controllerPhone.ChangeStateContact();
            imageBackGround.color = colorSelect;
            blockRayCast.SetActive(true);
            controllerPhone.SetPanelpurchasing(_dataContact);
        }
    }

    public void OpenPanel()
    {
        iTween.MoveTo(
            panelButtons,
            iTween.Hash(
                "position", new Vector3(60,0),
                "looktarget", Camera.main,
                "easeType", iTween.EaseType.easeOutExpo,
                "time", 1f,
                "islocal",true
            )
        );
    }
    public void ClosePanel()
    {
        iTween.MoveTo(
            panelButtons,
            iTween.Hash(
                "position", new Vector3(-40,0),
                "looktarget", Camera.main,
                "easeType", iTween.EaseType.easeOutExpo,
                "time", 1f,
                "islocal",true
            )
        );
    }
    public void SelectContact()
    {
        controllerPhone.SoundSelectContact();
        OpenPanel();
        imageBackGround.color = colorSelect;
        blockRayCast.SetActive(false);
    }
    public void DeselectContact()
    {
        ClosePanel();
        if (!_dataContact.purchasedContact)
        {
            ContactContainer.color = colorContactContainer;
            GetComponent<Image>().color = colorContactContainer;
        }
        else
        {
            imageBackGround.color = colorDeselect;
        }

        blockRayCast.SetActive(true);
    }
}
