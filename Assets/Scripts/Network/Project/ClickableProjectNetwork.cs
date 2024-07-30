using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickableProjectNetwork : NetworkBehaviour, IPointerClickHandler
{
    private TurnSystemNetwork turnSystemNetwork;

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        turnSystemNetwork = FindObjectOfType<TurnSystemNetwork>();
        if (turnSystemNetwork == null)
        {
            Debug.LogError("TurnSystemNetwork not found!");
            return;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        turnSystemNetwork = FindObjectOfType<TurnSystemNetwork>();
        if (turnSystemNetwork == null)
        {
            Debug.LogError("TurnSystemNetwork not found!");
            return;
        }

        if (turnSystemNetwork.turnOfPlayer != (int)NetworkManager.Singleton.LocalClientId) return;

        Debug.Log("Clicked on: " + gameObject.name + " (Local)");

        // Get the project number from the game object name
        int projectNumber = (int)gameObject.name[gameObject.name.Length - 1] - 49;

        // Request the server to handle the click
        HandleClickServerRpc(projectNumber);
    }

    [ServerRpc(RequireOwnership = false)]
    private void HandleClickServerRpc(int projectNumber, ServerRpcParams serverRpcParams = default)
    {
        ulong clientId = serverRpcParams.Receive.SenderClientId;
        Debug.Log($"Server received click from client {clientId} on project {projectNumber}");

        // Find the player object for the client who clicked
        NetworkObject playerObject = NetworkManager.Singleton.ConnectedClients[clientId].PlayerObject;
        if (playerObject != null)
        {
            StatPlayerNetwork statPlayerNetwork = playerObject.GetComponent<StatPlayerNetwork>();
            if (statPlayerNetwork != null)
            {
                // Update the selected project for this player
                statPlayerNetwork.selectedProject = projectNumber;

                // Optionally, you can call a ClientRpc to update all clients
                UpdateSelectedProjectClientRpc(clientId, projectNumber);
            }
        }
    }

    [ClientRpc]
    private void UpdateSelectedProjectClientRpc(ulong clientId, int projectNumber)
    {
        Debug.Log($"Client updated: Player {clientId} selected project {projectNumber}");

        // If you need to update something on all clients, do it here
        // For example, you might want to update UI elements to reflect the selection
    }
}