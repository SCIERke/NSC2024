using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickToseePlayer2 : NetworkBehaviour, IPointerClickHandler
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
            HandleClickPlayer2();
        }
        else
        {
            Debug.LogError("SpectatorManager not found!");
        }
    }

    void HandleClickPlayer2()
    {
        if (!IsOwner) return;

        spectatorManager.SwitchCamera(1);
    }
}
