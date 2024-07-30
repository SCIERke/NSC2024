using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class FieldClientManager : NetworkBehaviour
{
    [SerializeField] public GameObject[] plateCards;
    private StatPlayerNetwork statPlayerNetwork;

    public void HandleClickPlateNetwork(ulong clientId, Transform transformPlate)
    {
        int plateIndex = -1;

        for (int i = 0; i < plateCards.Length; i++)
        {
            if (plateCards[i].transform == transformPlate)
            {
                plateIndex = i;
                break;
            }
        }

        if (plateIndex != -1)
        {
            OnClickPlateNetworkServerRpc(clientId, plateIndex);
        }
        else
        {
            Debug.LogError("Plate not found in plateCards array.");
        }
    }

    [ServerRpc(RequireOwnership = false)]
    void OnClickPlateNetworkServerRpc(ulong clientId, int plateIndex)
    {
        var playerObject = NetworkManager.Singleton.ConnectedClients[clientId].PlayerObject;
        statPlayerNetwork = playerObject.GetComponent<StatPlayerNetwork>();

        if (plateIndex >= 0 && plateIndex < plateCards.Length)
        {
            statPlayerNetwork.selectedPlateId = plateIndex;
            OnClickPlateNetworkClientRpc(clientId, plateIndex);
        }
        else
        {
            Debug.LogError("Invalid plate index");
        }
    }

    [ClientRpc]
    void OnClickPlateNetworkClientRpc(ulong clientId, int selectedPlateId)
    {
        Debug.Log($"ClientID {clientId} Selected Plate ID: {selectedPlateId}!");
    }
}