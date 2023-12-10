using System;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ListContact", order = 1)]
public class ListDataContact : ScriptableObject
{
    public List<ContactData> listContact = new List<ContactData>();
}

[Serializable]
public class ContactData
{
    public string idContact;
    public string nameContact;
    public Sprite spriteContact;
    public string nameScene;
    public bool purchasedContact;
    public List<string> longAnswers = new List<string>();
    public List<string> shortAnswers = new List<string>();
}
