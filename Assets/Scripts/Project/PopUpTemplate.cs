using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpTemplate : MonoBehaviour
{
    [SerializeField] private Canvas popUp;

    void Start()
    {
        ClosePopup();
    }

    public void ClosePopup()
    {
        popUp.gameObject.SetActive(false);
    }
}
