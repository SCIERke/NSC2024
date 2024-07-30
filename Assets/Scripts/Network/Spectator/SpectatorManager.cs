using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using UnityEngine;

public class SpectatorManager : NetworkBehaviour
{
    [SerializeField] private Transform[] SeeOtherPlayerCanvasPoints;
    [SerializeField] private Camera[] cameraLookPlayer;
    [SerializeField] private Canvas[] canvasLookPlayer;

    [SerializeField] private GameObject SpectatorOtherPlayerPrefab;

    public void CloseAllCameras()
    {
        foreach (var camera in cameraLookPlayer)
        {
            if (camera != null)
            {
                camera.gameObject.SetActive(false);
                
            }
        }
        foreach (var canvas in canvasLookPlayer)
        {
            if (canvas != null)
            {
                canvas.gameObject.SetActive(false);

            }
        }
    }

    public void SwitchCamera(int playerIndex)
    {
        for (int i = 0; i < cameraLookPlayer.Length; i++)
        {
            cameraLookPlayer[i].gameObject.SetActive(i == playerIndex);
            canvasLookPlayer[i].gameObject.SetActive(i == playerIndex);
        }
    }

    public void HandlePressFSpectator(ulong clientId)
    {
        SpawnSpectatorOtherPlayerServerRpc(clientId);
    }


    [ServerRpc(RequireOwnership = false)]
    void SpawnSpectatorOtherPlayerServerRpc(ulong clientId)
    {
        var playerObject = NetworkManager.Singleton.ConnectedClients[clientId].PlayerObject;
        if (playerObject == null)
        {
            Debug.LogError("Can't find PlayerObject! (SpawnSpectator)");
            return;
        }
        Transform SeeOtherPlayerCanvasPoint = SeeOtherPlayerCanvasPoints[clientId];
        if (SeeOtherPlayerCanvasPoint == null)
        {
            Debug.LogError("Can't find SeeOtherPlayerCanvasPoint!");
            return;
        }
        Quaternion rot = Quaternion.Euler(0, 45, 0);

        GameObject SeeOtherPlayerCanvasObjectNetwork = Instantiate(SpectatorOtherPlayerPrefab, SeeOtherPlayerCanvasPoint.position, rot);
        SeeOtherPlayerCanvasObjectNetwork.name = "SeeOtherPlayerCanvas";
        NetworkObject networkObject = SeeOtherPlayerCanvasObjectNetwork.GetComponent<NetworkObject>();
        if (networkObject != null)
        {
            networkObject.SpawnWithOwnership(clientId);

            
            if (playerObject != null)
            {
                SeeOtherPlayerCanvasObjectNetwork.transform.SetParent(playerObject.transform, true);
            }
            else
            {
                Debug.LogError($"Player object not found for ClientId: {clientId}");
            }

            SpawnSpectatorOtherPlayerClientRpc(networkObject.NetworkObjectId, clientId);
        }
        else
        {
            Debug.LogError("Can't get NetworkObject! (SpawnSpectatorToolNetwork)");
        }
    }

    [ClientRpc]
    void SpawnSpectatorOtherPlayerClientRpc(ulong networkObjectId, ulong clientId)
    {
        if (NetworkManager.Singleton.SpawnManager.SpawnedObjects.TryGetValue(networkObjectId, out var networkObject))
        {
            GameObject SpawnSpectatorOtherObject = networkObject.gameObject;
            Debug.Log($"SpawnSpectatorOther object spawned for ClientId: {clientId} (SpawnSpectatorOther)");            
        }
        else
        {
            Debug.LogError($"NetworkObject not found for ID: {networkObjectId}");
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
