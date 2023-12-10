using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControllerCall : MonoBehaviour
{
    [SerializeField]private Image imageContact;
    [SerializeField]private Image backGround;
    [SerializeField]private GameObject camRender;
    [SerializeField] private GameObject panelCall;
    [field: SerializeField] public ContactData Data { get; private set; }

    [SerializeField] private ControllerPhone _controllerPhone;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCall(ContactData contactData,ControllerPhone controllerPhone)
    {
        _controllerPhone=controllerPhone;
        camRender.SetActive(false);
        panelCall.SetActive(true);
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


    
}
