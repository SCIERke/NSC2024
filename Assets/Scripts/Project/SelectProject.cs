using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectProject : MonoBehaviour
{
    private StatPlayer statPlayer;
    public GameObject HolohramYesNo;

    void Start()
    {
        //HologramCalculator.gameObject.SetActive(false);
        HolohramYesNo.SetActive(false);
        statPlayer = FindObjectOfType<StatPlayer>();
    }

    public Camera uiCamera; // Reference to the camera that renders the UI

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) // Check for mouse click
        {
            // Perform the raycast
            PointerEventData pointerEventData = new PointerEventData(EventSystem.current)
            {
                position = Input.mousePosition
            };

            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerEventData, results);

            int i = 0;
            foreach (RaycastResult result in results)
            {
                if (result.gameObject.CompareTag("IgnoreRaycast"))
                {
                    continue;
                }
                if (i == 0)
                {
                    i++;
                    continue;
                }

                if (result.gameObject.CompareTag("Project") && result.gameObject.name.ToString() == "BG")
                {
                    if (result.gameObject.transform.parent.transform == transform)
                    {
                        HolohramYesNo.SetActive(true);
                        Debug.Log(transform.name);
                        statPlayer.selectedProject = transform.gameObject;
                    }
                    // Debug.Log("Hit " + result.gameObject.transform.parent.name);
                }
                i++;
            }
        }
    }

    
}
