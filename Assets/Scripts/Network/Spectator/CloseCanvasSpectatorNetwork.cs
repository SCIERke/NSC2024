using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class CloseCanvasSpectatorNetwork : NetworkBehaviour
{
    private SpectatorManager spectatorManager;
    private NetworkObject networkObject;

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        networkObject = GetComponent<NetworkObject>();
        if (networkObject == null)
        {
            Debug.LogError("Can't find NetworkObject (HandlePressFClose)");
            return;
        }

        if (NetworkManager.Singleton.LocalClientId != networkObject.OwnerClientId)
        {
            return;
        }

        spectatorManager = FindObjectOfType<SpectatorManager>();
        if (spectatorManager == null)
        {
            Debug.LogError("Can't find SpectatorManager (HandlePressFClose)");
        }
    }

    void Update()
    {
        if (NetworkManager.Singleton.LocalClientId != networkObject.OwnerClientId)
        {
            return;
        }


        if (Input.GetKeyDown(KeyCode.F))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform == transform)
                {
                    Debug.Log("Interact!");
                    DespawnSpectatorCanvasServerRpc(networkObject.NetworkObjectId);
                }
            }
        }
    }

    [ServerRpc(RequireOwnership = false)]
    private void DespawnSpectatorCanvasServerRpc(ulong networkObjectId)
    {
        if (NetworkManager.Singleton.SpawnManager.SpawnedObjects.TryGetValue(networkObjectId, out var networkObject))
        {
            networkObject.Despawn();
        }
        else
        {
            Debug.LogError($"NetworkObject not found for ID: {networkObjectId}");
        }
    }
}
