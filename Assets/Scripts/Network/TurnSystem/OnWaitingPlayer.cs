using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class OnWaitingPlayer : NetworkBehaviour
{
    public int amountOfPlayer = 0;
    [SerializeField] private Canvas OnWaitingPlayerCanvas;

    // Update is called once per frame
    void Update()
    {
        if (!IsServer) return;

        HandleControlLookAroundPlayerOnWaitingServerRpc();
    }

    [ServerRpc(RequireOwnership = false)]
    void HandleControlLookAroundPlayerOnWaitingServerRpc()
    {
        bool shouldEnable = amountOfPlayer >= 4;

        for (int i = 0; i < NetworkManager.Singleton.ConnectedClientsList.Count; i++)
        {
            var playerObject = NetworkManager.Singleton.ConnectedClientsList[i].PlayerObject;
            if (playerObject == null)
            {
                Debug.LogError("PlayerObject is null");
                continue;
            }

            foreach (Transform child in playerObject.transform)
            {
                if (child.name == "PlayerCamera")
                {
                    LookAroundNetwork lookAroundNetworkPlayer = child.GetComponent<LookAroundNetwork>();
                    if (lookAroundNetworkPlayer != null)
                    {
                        lookAroundNetworkPlayer.SetActive(shouldEnable);
                        OnWaitingPlayerCanvas.gameObject.SetActive(!shouldEnable);
                        // Call the ClientRpc for this client
                        HandleControlLookAroundPlayerOnWaitingClientRpc(NetworkManager.Singleton.ConnectedClientsList[i].ClientId, shouldEnable);
                    }
                }
            }
        }
    }

    [ClientRpc]
    void HandleControlLookAroundPlayerOnWaitingClientRpc(ulong clientId, bool controlLook)
    {
        // Only execute if this is the client the Rpc is intended for
        if (NetworkManager.Singleton.LocalClientId != clientId)
        {
            return;
        }

        var playerObject = NetworkManager.Singleton.LocalClient.PlayerObject;
        if (playerObject == null)
        {
            Debug.LogError("PlayerObject is null (HandleControlLookAroundPlayerOnWaitingClientRpc)");
            return;
        }
        foreach (Transform child in playerObject.transform)
        {
            if (child.name == "PlayerCamera")
            {
                LookAroundNetwork lookAroundNetworkPlayer = child.GetComponent<LookAroundNetwork>();
                if (lookAroundNetworkPlayer != null)
                {
                    lookAroundNetworkPlayer.SetActive(controlLook);
                    OnWaitingPlayerCanvas.gameObject.SetActive(!controlLook);
                    Debug.Log("SetPlayer Onwaiting Successful!");
                    break;
                }
            }
        }

        
    }
}
