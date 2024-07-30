using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaptopField : MonoBehaviour
{
    public GameObject field;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit) && hit.transform == transform)
            {
                SetCanvasAndChildrenActive(field, true);
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
