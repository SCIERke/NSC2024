using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class OnPlayerPressODoProject : NetworkBehaviour
{
    [SerializeField] private GameObject popUpProjectConditionPrefab;
    private NetworkObject networkObject;
    private StatPlayerNetwork statPlayerNetwork;
    private ProjectManager projectManager;
    private TurnSystemNetwork turnSystemNetwork;

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        networkObject = GetComponent<NetworkObject>();
        if (networkObject == null)
        {
            Debug.LogError("NetworkObject not found!");
            return;
        }

        if (NetworkManager.Singleton.LocalClientId != networkObject.OwnerClientId)
        {
            return;
        }

        turnSystemNetwork = FindObjectOfType<TurnSystemNetwork>();
        if (turnSystemNetwork == null)
        {
            Debug.LogError("TurnSystemNetwork not found!");
            return;
        }

        statPlayerNetwork = GetComponent<StatPlayerNetwork>();
        if (statPlayerNetwork == null)
        {
            Debug.LogError("StatPlayerNetwork not found!");
            return;
        }

        projectManager = FindObjectOfType<ProjectManager>();
        if (projectManager == null)
        {
            Debug.LogError("ProjectManager not found!");
            return;
        }
    }

    void Update()
    {
        if (networkObject == null || statPlayerNetwork == null || projectManager == null)
        {
            return;
        }

        if (NetworkManager.Singleton.LocalClientId != networkObject.OwnerClientId)
        {
            return;
        }

        turnSystemNetwork = FindObjectOfType<TurnSystemNetwork>();
        if (turnSystemNetwork == null)
        {
            Debug.LogError("TurnSystemNetwork not found!");
            return;
        }

        if (turnSystemNetwork.turnOfPlayer != (int)NetworkManager.Singleton.LocalClientId) return;

        if (Input.GetKeyDown(KeyCode.O))
        {
            HandleSpawnPopUpProjectConditionServerRpc(NetworkManager.Singleton.LocalClientId);
        }
    }

    [ServerRpc(RequireOwnership = false)]
    void HandleSpawnPopUpProjectConditionServerRpc(ulong clientId)
    {
        projectManager = FindObjectOfType<ProjectManager>();
        if (projectManager == null)
        {
            Debug.LogError("ProjectManager not found!");
            return;
        }

        if (clientId >= (ulong)projectManager.spawnProjectPopUpPoints.Length)
        {
            Debug.LogError($"No spawn points for PopUpProjectCondition assigned for ClientId: {clientId}");
            return;
        }

        Transform spawnPopUpProjectConditionPoint = projectManager.spawnProjectPopUpPoints[clientId];
        if (spawnPopUpProjectConditionPoint == null)
        {
            Debug.LogError($"No spawn point assigned for ClientId: {clientId} (SpawnProjectCondition)");
            return;
        }
        Quaternion rot = Quaternion.Euler(0, 90, 0);
        GameObject popUpProjectConditionObjectNetwork = Instantiate(popUpProjectConditionPrefab, spawnPopUpProjectConditionPoint.position, rot);
        popUpProjectConditionObjectNetwork.name = "PopUpProjectCondition";
        NetworkObject networkObject = popUpProjectConditionObjectNetwork.GetComponent<NetworkObject>();
        if (networkObject != null)
        {
            networkObject.SpawnWithOwnership(clientId);

            var playerObject = NetworkManager.Singleton.ConnectedClients[clientId].PlayerObject;
            if (playerObject != null)
            {
                popUpProjectConditionObjectNetwork.transform.SetParent(playerObject.transform, true);
            }
            else
            {
                Debug.LogError($"Player Object not found for ClientId: {clientId}");
            }
            HandleSpawnPopUpProjectConditionClientRpc(networkObject.NetworkObjectId, clientId);
        }
        else
        {
            Debug.LogError("Can't get NetworkObject! (SpawnPopUpNetwork)");
        }
    }

    [ClientRpc]
    void HandleSpawnPopUpProjectConditionClientRpc(ulong networkObjectId, ulong clientId)
    {
        if (NetworkManager.Singleton.SpawnManager.SpawnedObjects.TryGetValue(networkObjectId, out var networkObject))
        {
            GameObject popUpProjectConditionObject = networkObject.gameObject;
            Debug.Log($"PopUpProjectConditionObject spawned for ClientId: {clientId} (PopUpProjectCondition)");
            // Perform any client-side updates if necessary
        }
        else
        {
            Debug.LogError($"NetworkObject not found for ID: {networkObjectId}");
        }
    }
}
