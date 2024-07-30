/*
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class HandlePressFSpectatorToolNetwork : NetworkBehaviour
{
    private SpectatorManager spectatorManager;
    private NetworkObject networkObject;
    public override void OnNetworkSpawn()
    {
        networkObject = GetComponent<NetworkObject>();
        if (networkObject == null)
        {
            Debug.LogError("Can't find NetworkObject (HandlePressF)");
            return;
        }

        if (NetworkManager.Singleton.LocalClientId != networkObject.OwnerClientId)
        {
            return;
        }


        spectatorManager = FindObjectOfType<SpectatorManager>();
        if (spectatorManager == null)
        {
            Debug.LogError("Can't find SpectatorManager (HandlePressF)");
            return;
        }
    }

    // Update is called once per frame
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
                    spectatorManager.HandlePressFSpectator(NetworkManager.Singleton.LocalClientId);
                }
            }

        }
    }
}
*/

using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class HandlePressFSpectatorToolNetwork : NetworkBehaviour
{
    private SpectatorManager spectatorManager;
    private NetworkObject networkObject;

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        networkObject = GetComponent<NetworkObject>();
        if (networkObject == null)
        {
            Debug.LogError("Can't find NetworkObject (HandlePressF)");
            return;
        }

        if (NetworkManager.Singleton.LocalClientId != networkObject.OwnerClientId)
        {
            return;
        }

        spectatorManager = FindObjectOfType<SpectatorManager>();
        if (spectatorManager == null)
        {
            Debug.LogError("Can't find SpectatorManager (HandlePressF)");
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
                    HandlePressFSpectatorServerRpc(NetworkManager.Singleton.LocalClientId);
                }
            }
        }
    }

    [ServerRpc(RequireOwnership = false)]
    void HandlePressFSpectatorServerRpc(ulong clientId)
    {
        if (spectatorManager == null)
        {
            spectatorManager = FindObjectOfType<SpectatorManager>();
            if (spectatorManager == null)
            {
                Debug.LogError("Can't find SpectatorManager (ServerRpc)");
                return;
            }
        }

        spectatorManager.HandlePressFSpectator(clientId);
    }
}
