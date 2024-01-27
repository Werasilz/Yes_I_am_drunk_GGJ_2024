using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWindow : MonoBehaviour
{
    public string windowName;
    public bool isOpen;

    public void Open()
    {
        isOpen = true;
        gameObject.SetActive(isOpen);
    }

    public void Close()
    {
        isOpen = false;
        gameObject.SetActive(isOpen);
    }
}
