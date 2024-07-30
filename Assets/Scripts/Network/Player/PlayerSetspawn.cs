using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.VisualScripting;
using JetBrains.Annotations;
using System.Runtime.CompilerServices;

public class PlayerSetSpawn : NetworkBehaviour
{
    //Hello world

    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private Transform[] spawnDeckObjectsPoint;
    [SerializeField] private Transform[] spawnFieldPoints;
    [SerializeField] private Transform[] spawnCanvasPoints;
    [SerializeField] private Transform[] spawnSpectatorPoints;
    [SerializeField] private Transform[] spawnTonextPlayerPoints;

    [SerializeField] private GameObject deckObjectPrefeb;
    [SerializeField] private GameObject feildObjectPrefeb;
    [SerializeField] private GameObject MoniterCanvasPrefeb;
    [SerializeField] private GameObject SpectatorToolPrefeb;
    [SerializeField] private GameObject TonextPlayerObject;

    private OnWaitingPlayer onWaitingPlayer;

    void Start()
    {
        // Register the OnServerStarted event
        NetworkManager.Singleton.OnServerStarted += OnServerStarted;
    }

    void OnServerStarted()
    {
        if (IsServer)
        {
            Debug.Log("Server started (PlayerSetSpawn): Registering OnClientConnectedCallback");
            NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
        }
    }

    public override void OnDestroy() 
    {
        base.OnDestroy();

        if (NetworkManager.Singleton != null)
        {
            Debug.Log("Server (PlayerSetSpawn): Unregistering OnClientConnectedCallback");
            NetworkManager.Singleton.OnClientConnectedCallback -= OnClientConnected;
        }
    }

    private void OnClientConnected(ulong clientId)
    {
        Debug.Log($"Client Joined! ClientId: {clientId} (PlayerSetSpawn)");

        SetPositionPlayerServerRpc(clientId);
        SpawnDeckCardNetworkServerRpc(clientId);
        SpawnFieldNetworkServerRpc(clientId);
        SpawnCanvasNetworkServerRpc(clientId);
        SpawnSpectatorToolNetworkServerRpc(clientId);
        SpawnTonextPlayerNetworkServerRpc(clientId);
        IncreaseNumberofPlayerServerRpc();
    }

    [ServerRpc(RequireOwnership = false)]
    void SpawnTonextPlayerNetworkServerRpc(ulong clientId) {
        if (clientId >= (ulong)spawnTonextPlayerPoints.Length)
        {
            Debug.LogError($"No spawn points for TonextPlayer assigned for ClientId: {clientId} (SpawnTonextPlayerNetworkServerRpc)");
            return;
        }
        Transform spawnTonextPlayerPoint = spawnTonextPlayerPoints[clientId];
        if (spawnTonextPlayerPoint == null)
        {
            Debug.LogError($"No spawn point assigned for ClientId: {clientId} (SpawnTonextPlayerNetworkServerRpc)");
            return;
        }
        Quaternion rot = Quaternion.Euler(-90, 0, -90);
        GameObject TonextPlayerObjectNetwork = Instantiate(TonextPlayerObject, spawnTonextPlayerPoint.position, rot);
        TonextPlayerObjectNetwork.name = "TonextPlayer";
        NetworkObject networkObject = TonextPlayerObjectNetwork.GetComponent<NetworkObject>();
        if (networkObject != null)
        {
            networkObject.SpawnWithOwnership(clientId);

            // Set the parent on the server side
            var playerObject = NetworkManager.Singleton.ConnectedClients[clientId].PlayerObject;
            if (playerObject != null)
            {
                TonextPlayerObjectNetwork.transform.SetParent(playerObject.transform, true);
            }
            else
            {
                Debug.LogError($"Player object not found for ClientId: {clientId}");
            }

            SpawnSpectatorToolNetworkClientRpc(networkObject.NetworkObjectId, clientId);
        }
        else
        {
            Debug.LogError("Can't get NetworkObject! (SpawnTonextPlayerNetworkServerRpc)");
        }
    }

    [ServerRpc(RequireOwnership = false)]
    void IncreaseNumberofPlayerServerRpc()
    {
        onWaitingPlayer = FindObjectOfType<OnWaitingPlayer>();
        if (onWaitingPlayer != null )
        {
            onWaitingPlayer.amountOfPlayer = NetworkManager.Singleton.ConnectedClientsList.Count;
        } else
        {
            Debug.LogError("Can't Increase amount of player!");
            return;
        }
    }

    [ServerRpc(RequireOwnership = false)]
    void SpawnSpectatorToolNetworkServerRpc(ulong clientId)
    {
        if (clientId >= (ulong)spawnSpectatorPoints.Length)
        {
            Debug.LogError($"No spawn points for SpectatorTool assigned for ClientId: {clientId} (SpawnSpectator)");
            return;
        }
        Transform spawnSpectatorPoint = spawnSpectatorPoints[clientId];
        if (spawnSpectatorPoint == null)
        {
            Debug.LogError($"No spawn point assigned for ClientId: {clientId} (SpawnSpectator)");
            return;
        }
        Quaternion rot = spawnSpectatorPoint.rotation;
        GameObject SpectatorToolObjectNetwork = Instantiate(SpectatorToolPrefeb, spawnSpectatorPoint.position, rot);
        SpectatorToolObjectNetwork.name = "SpectatorTool";
        NetworkObject networkObject = SpectatorToolObjectNetwork.GetComponent<NetworkObject>();
        if (networkObject != null)
        {
            networkObject.SpawnWithOwnership(clientId);

            // Set the parent on the server side
            var playerObject = NetworkManager.Singleton.ConnectedClients[clientId].PlayerObject;
            if (playerObject != null)
            {
                SpectatorToolObjectNetwork.transform.SetParent(playerObject.transform, true);
            }
            else
            {
                Debug.LogError($"Player object not found for ClientId: {clientId}");
            }

            SpawnSpectatorToolNetworkClientRpc(networkObject.NetworkObjectId, clientId);
        }
        else
        {
            Debug.LogError("Can't get NetworkObject! (SpawnSpectatorToolNetwork)");
        }
    }

    [ClientRpc]
    void SpawnSpectatorToolNetworkClientRpc(ulong networkObjectId, ulong clientId)
    {
        if (NetworkManager.Singleton.SpawnManager.SpawnedObjects.TryGetValue(networkObjectId, out var networkObject))
        {
            GameObject SpectatorToolObject = networkObject.gameObject;
            Debug.Log($"CanvasMoniter object spawned for ClientId: {clientId} (SpawnSpectatorToolNetwork)");
            // Perform any client-side updates if necessary
        }
        else
        {
            Debug.LogError($"NetworkObject not found for ID: {networkObjectId}");
        }
    }

    [ServerRpc(RequireOwnership = false)]
    void SpawnCanvasNetworkServerRpc(ulong clientId)
    {
        if (clientId >= (ulong)spawnCanvasPoints.Length)
        {
            Debug.LogError($"No spawn points for MoniterCanvas assigned for ClientId: {clientId} (SpawnCanvasCard)");
            return;
        }

        Transform spawnCanvasPoint = spawnCanvasPoints[clientId];
        if (spawnCanvasPoint == null)
        {
            Debug.LogError($"No spawn point assigned for ClientId: {clientId} (SpawnCanvasCard)");
            return;
        }
        Quaternion rot = Quaternion.Euler(0, 90, 0);
        GameObject CanvasMoniterObjectNetwork = Instantiate(MoniterCanvasPrefeb, spawnCanvasPoint.position, rot);
        CanvasMoniterObjectNetwork.name = "MoniterCanvas";
        NetworkObject networkObject = CanvasMoniterObjectNetwork.GetComponent<NetworkObject>();
        if (networkObject != null)
        {
            networkObject.SpawnWithOwnership(clientId);

            // Set the parent on the server side
            var playerObject = NetworkManager.Singleton.ConnectedClients[clientId].PlayerObject;
            if (playerObject != null)
            {
                CanvasMoniterObjectNetwork.transform.SetParent(playerObject.transform, true);
            }
            else
            {
                Debug.LogError($"Player object not found for ClientId: {clientId}");
            }

            DeckManager deckManager = FindObjectOfType<DeckManager>();
            // Notify all clients that the field object has been spawned
            SpawnCanvasNetworkClientRpc(networkObject.NetworkObjectId, clientId);
        }
        else
        {
            Debug.LogError("Can't get NetworkObject! (SpawnFieldNetwork)");
        }
    }

    [ClientRpc]
    void SpawnCanvasNetworkClientRpc(ulong networkObjectId, ulong clientId)
    {
        if (NetworkManager.Singleton.SpawnManager.SpawnedObjects.TryGetValue(networkObjectId, out var networkObject))
        {
            GameObject CanvasObject = networkObject.gameObject;
            Debug.Log($"CanvasMoniter object spawned for ClientId: {clientId} (SpawnCanvasNetwork)");
            // Perform any client-side updates if necessary
        }
        else
        {
            Debug.LogError($"NetworkObject not found for ID: {networkObjectId}");
        }
    }

    [ServerRpc(RequireOwnership = false)]
    void SpawnFieldNetworkServerRpc(ulong clientId)
    {
        if (clientId >= (ulong)spawnFieldPoints.Length)
        {
            Debug.LogError($"No spawn points for Field assigned for ClientId: {clientId} (SpawnFieldCard)");
            return;
        }

        Transform spawnFieldPoint = spawnFieldPoints[clientId];
        if (spawnFieldPoint == null)
        {
            Debug.LogError($"No spawn point assigned for ClientId: {clientId} (SpawnDeckCard)");
            return;
        }

        GameObject fieldObjectNetwork = Instantiate(feildObjectPrefeb, spawnFieldPoint.position, Quaternion.identity);
        fieldObjectNetwork.name = "Field";
        NetworkObject networkObject = fieldObjectNetwork.GetComponent<NetworkObject>();
        if (networkObject != null)
        {
            networkObject.SpawnWithOwnership(clientId);

            // Set the parent on the server side
            var playerObject = NetworkManager.Singleton.ConnectedClients[clientId].PlayerObject;
            if (playerObject != null)
            {
                fieldObjectNetwork.transform.SetParent(playerObject.transform, true);
            }
            else
            {
                Debug.LogError($"Player object not found for ClientId: {clientId}");
            }
            DeckManager deckManager = FindObjectOfType<DeckManager>();
            deckManager.ListfieldofPlayerNetworkObjectId.Add((int)networkObject.NetworkObjectId); //NEW
            // Notify all clients that the field object has been spawned
            NotifyClientsFieldObjectSpawnedClientRpc(networkObject.NetworkObjectId, clientId);
        }
        else
        {
            Debug.LogError("Can't get NetworkObject! (SpawnFieldNetwork)");
        }
    }

    [ClientRpc]
    void NotifyClientsFieldObjectSpawnedClientRpc(ulong networkObjectId, ulong clientId)
    {
        if (NetworkManager.Singleton.SpawnManager.SpawnedObjects.TryGetValue(networkObjectId, out var networkObject))
        {
            GameObject fieldObject = networkObject.gameObject;
            Debug.Log($"Field object spawned for ClientId: {clientId} (SpawnFieldNetwork)");
            // Perform any client-side updates if necessary
        }
        else
        {
            Debug.LogError($"NetworkObject not found for ID: {networkObjectId}");
        }
    }

    [ServerRpc]
    void SpawnDeckCardNetworkServerRpc(ulong clientId)
    {
        if (clientId >= (ulong)spawnDeckObjectsPoint.Length)
        {
            Debug.LogError($"No spawnPointsDeckObject assigned for ClientId: {clientId} (SpawnDeckCard)");
            return;
        }

        Transform spawnDeckPoint = spawnDeckObjectsPoint[clientId];
        if (spawnDeckPoint == null)
        {
            Debug.LogError($"No spawnPointsDeckObject assigned for ClientId: {clientId} (SpawnDeckCard)");
            return;
        }

        GameObject deckObjectNetwork = Instantiate(deckObjectPrefeb, spawnDeckPoint.position, Quaternion.identity);
        NetworkObject networkObject = deckObjectNetwork.GetComponent<NetworkObject>();
        networkObject.SpawnWithOwnership(clientId);

        // Call Client RPC to notify all clients
        NotifyClientsDeckObjectSpawnedClientRpc(deckObjectNetwork.GetComponent<NetworkObject>().NetworkObjectId, clientId);
    }

    [ClientRpc]
    void NotifyClientsDeckObjectSpawnedClientRpc(ulong networkObjectId, ulong clientId)
    {
        if (NetworkManager.Singleton.SpawnManager.SpawnedObjects.TryGetValue(networkObjectId, out var networkObject))
        {
            var deckObjectNetwork = networkObject.GetComponent<GameObject>();
            if (deckObjectNetwork != null)
            {
                Debug.Log($"Deck object spawned for ClientId: {clientId} (PlayerSetSpawn)");
            }
        }
    }

    [ServerRpc(RequireOwnership = false)]
    void SetPositionPlayerServerRpc(ulong clientId)
    {
        if (clientId >= (ulong)spawnPoints.Length)
        {
            Debug.LogError($"No spawn point assigned for ClientId: {clientId} (PlayerSetSpawn)");
            return;
        }

        Transform spawnPoint = spawnPoints[clientId];
        if (spawnPoint == null)
        {
            Debug.LogError($"spawnPoint is not assigned for ClientId: {clientId} (PlayerSetSpawn)");
            return;
        }

        var playerObject = NetworkManager.Singleton.ConnectedClients[clientId].PlayerObject;
        if (playerObject != null)
        {
            playerObject.name = "Player" + clientId.ToString();

            // Set the position on the server
            playerObject.transform.position = spawnPoint.position;

            Debug.Log($"Setting position for ClientId: {clientId} to {spawnPoint.position} (PlayerSetSpawn)");

            // Call client RPC to set position on the client
            SetPositionClientRpc(clientId, spawnPoint.position);
        }
        else
        {
            Debug.LogError($"PlayerObject for ClientId: {clientId} is null (PlayerSetSpawn)");
        }
    }

    [ClientRpc]
    void SetPositionClientRpc(ulong clientId, Vector3 position)
    {
        if (NetworkManager.Singleton.LocalClientId == clientId)
        {
            var playerObject = NetworkManager.Singleton.SpawnManager.GetLocalPlayerObject();
            if (playerObject != null)
            {
                // Set the position on the client
                playerObject.transform.position = position;
            }
            else
            {
                Debug.LogError($"PlayerObject for ClientId: {clientId} is null in ClientRpc (PlayerSetSpawn)");
            }
        }
    }
}
