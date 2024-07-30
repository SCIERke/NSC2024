using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Field : MonoBehaviour
{
    public GameObject field;

    void Start()
    {
        //field.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit) && hit.transform == transform)
            {
                SetCanvasAndChildrenActive(field, false);
            }
        }
    }

    private void SetCanvasAndChildrenActive(GameObject obj, bool isActive)
    {
        obj.SetActive(isActive);
        foreach (Transform child in obj.transform)
        {
            SetCanvasAndChildrenActive(child.gameObject, isActive);
        }
    }
}
