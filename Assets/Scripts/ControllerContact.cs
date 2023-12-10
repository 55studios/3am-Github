using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ControllerContact : MonoBehaviour
{
    [SerializeField] private Image imageBackGround;
    [SerializeField] private Image imageContact;
    [SerializeField] private TMP_Text nameContact;
    [SerializeField] private GameObject blockRayCast;
    [SerializeField] private Color32 colorSelect;
    [SerializeField] private Color32 colorDeselect;
    private ContactData _dataContact;
    private ControllerPhone controllerPhone;


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
    }

    public void InitChat()
    {
        controllerPhone.InitChat(_dataContact);
    }
    public void InitCall()
    {
        controllerPhone.InitCall(_dataContact);
    }

    public void ChangeStateContact()
    {
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

    public void SelectContact()
    {
        imageBackGround.color = colorSelect;
        blockRayCast.SetActive(false);
    }
    public void DeselectContact()
    {
        imageBackGround.color = colorDeselect;
        blockRayCast.SetActive(true);
    }
}
