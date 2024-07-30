using Unity.Netcode;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickToSeePlayer1 : NetworkBehaviour, IPointerClickHandler
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
            HandleClickPlayer1();
        }
        else
        {
            Debug.LogError("SpectatorManager not found!");
        }
    }

    void HandleClickPlayer1()
    {
        if (!IsOwner) return;

        spectatorManager.SwitchCamera(0);
    }
}