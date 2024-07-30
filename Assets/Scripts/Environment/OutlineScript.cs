using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Unity.Netcode;

public class OutlineScript : NetworkBehaviour
{
    private Transform highlight;
    private Transform selection;
    private RaycastHit raycastHit;

    [SerializeField] private EventSystem eventSystem;
    public Camera playerCamera;

    void Start()
    {
        if (!IsOwner) // Ensure the script runs only for the local player's camera
        {
            enabled = false;
            return;
        }

        if (playerCamera == null)
        {
            playerCamera = Camera.main; // Fallback to the main camera if no camera is assigned
        }
    }

    void Update()
    {
        // Highlight
        if (highlight != null)
        {
            highlight.gameObject.GetComponent<Outline>().enabled = false;
            highlight = null;
        }

        // Handle 3D objects
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out raycastHit))
        {
            highlight = raycastHit.transform;
            if ((highlight.CompareTag("Card") || highlight.CompareTag("InGameItem") || highlight.CompareTag("Area") || highlight.CompareTag("Project")) && highlight != selection)
            {
                ApplyOutline(highlight);
            }
            else
            {
                highlight = null;
            }
        }

        // Handle UI elements
        /*
        else
        {
            PointerEventData pointerEventData = new PointerEventData(eventSystem)
            {
                position = Input.mousePosition
            };

            List<RaycastResult> results = new List<RaycastResult>();
            uiRaycaster.Raycast(pointerEventData, results);

            foreach (RaycastResult result in results)
            {
                if (result.gameObject.CompareTag("Project"))
                {
                    highlight = result.gameObject.transform;
                    ApplyOutline(highlight);
                    break;
                }
            }
        }
        */
    }

    private void ApplyOutline(Transform target)
    {
        Outline outline = target.gameObject.GetComponent<Outline>();
        if (outline == null)
        {
            outline = target.gameObject.AddComponent<Outline>();
        }
        outline.enabled = true;
        outline.OutlineColor = Color.magenta;
        outline.OutlineWidth = 7.0f;
    }
}
