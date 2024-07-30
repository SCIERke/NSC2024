using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickToseePlayer4 : NetworkBehaviour, IPointerClickHandler
{
    private SpectatorManager spectatorManager;

    public override void OnNetworkSpawn()
    {
        if (!IsOwner) return;
        spectatorManager = FindObjectOfType<SpectatorManager>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clicked on: " + gameObject.name + " (Local)");

        if (spectatorManager != null)
        {
            HandleClickPlayer4();
        }
        else
        {
            Debug.LogError("SpectatorManager not found!");
        }
    }

    void HandleClickPlayer4()
    {
        if (!IsOwner) return;

        spectatorManager.SwitchCamera(3);
    }
}
