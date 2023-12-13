using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ControllerPurchasing : MonoBehaviour
{
    public TMP_Text nameContact;
    public ContactData data;
    // Start is called before the first frame update
    public void StartPurchasing(ContactData contactData)
    {
        data = contactData;
        nameContact.text = "Quieres comprar a "+data.nameContact;
        gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
