using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ControllerPrefabMessage : MonoBehaviour
{
    [SerializeField] private Image backGround;

    [SerializeField] private TMP_Text messages;

    public TMP_Text Messages
    {
        get => messages;
    }

    public void SetMessage(Color32 color32, string message)
    {
        backGround.color = color32;
        messages.text = message;
    }

    public void DestroyMessage()
    {
        Destroy(gameObject);
    }
}
