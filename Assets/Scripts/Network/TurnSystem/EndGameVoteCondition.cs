using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class EndGameVoteCondition : NetworkBehaviour
{
    public int voteAccept;
    public int voteCancel;
    public int voteTotal;

    [SerializeField] private GameObject VoteEndgamePopupPrefeb;
    [SerializeField] private Canvas goodPeopleWin;
    [SerializeField] private Canvas badPeopleWin;

    private ProjectManager projectManager;

    // Start is called before the first frame update
    void Start()
    {
        voteAccept = 0;
        voteCancel = 0;
        voteTotal = 0;
        projectManager = FindObjectOfType<ProjectManager>();
        if (projectManager == null)
        {
            Debug.LogError("Can't find projectManager");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsServer) { return; }

        if (voteTotal >= 4)
        {
            int goodprojectCount = 0;
            int badprojectCount = 0;
            for (int i = 0;i < NetworkManager.Singleton.ConnectedClients.Count; i++)
            {
                var playerObject = NetworkManager.Singleton.ConnectedClients[(ulong)i].PlayerObject;
                if (playerObject != null)
                {
                    StatPlayerNetwork statPlayerNetwork = playerObject.GetComponent<StatPlayerNetwork>();
                    if (statPlayerNetwork != null)
                    {
                        if (statPlayerNetwork.sideofPlayer == "Good")
                        {
                            goodprojectCount += statPlayerNetwork.projectPlayerCount;
                        } else
                        {
                            badprojectCount += statPlayerNetwork.projectPlayerCount;
                        }
                    } else
                    {
                        Debug.LogError("Can't find StatPlayerNetwork");
                    }
                    
                } else
                {
                    Debug.LogError("Can't find PlayerObject");
                }
            }
            if (goodprojectCount >= 2* NetworkManager.Singleton.ConnectedClients.Count)
            {
                SetCanvasGoodPeopleWinServerRpc();
                Debug.Log("Good Player Win!!");
            } else
            {
                SetCanvasBadPeopleWinServerRpc();
                Debug.Log("Bad Player Win!!");
            }
        }
        // Handle the voting state and other game logic here
    }

    [ServerRpc(RequireOwnership = false)]
    void SetCanvasGoodPeopleWinServerRpc()
    {
        goodPeopleWin.gameObject.SetActive(true);
    }

    [ServerRpc(RequireOwnership = false)]
    void SetCanvasBadPeopleWinServerRpc()
    {
        badPeopleWin.gameObject.SetActive(true);
    }

    public void OnVoteBegan()
    {
        DeleteAllEndGameConditionPopUpServerRpc();
        SpawnVotePopUpServerRpc();
    }

    [ServerRpc(RequireOwnership = false)]
    void SpawnVotePopUpServerRpc()
    {
        for (int clientId = 0; clientId < NetworkManager.Singleton.ConnectedClientsList.Count;clientId++)
        {
            if (clientId >= projectManager.spawnProjectPopUpPoints.Length)
            {
                Debug.LogError($"No spawn points for VotePopUp assigned for ClientId: {clientId} (SpawnVotePopUpSeverRpc)");
                return;
            }
            Transform voteEndGameSpawnPoint = projectManager.spawnProjectPopUpPoints[clientId];
            if (voteEndGameSpawnPoint == null)
            {
                Debug.LogError($"No spawn point assigned for ClientId: {clientId} (SpawnVotePopUpSeverRpc)");
                return;
            }
            Quaternion rot = Quaternion.Euler(0, 90, 0);
            GameObject voteEndGameObjectNetwork = Instantiate(VoteEndgamePopupPrefeb, voteEndGameSpawnPoint.position, rot);
            voteEndGameObjectNetwork.name = "VoteEndGamePopUp";
            NetworkObject networkObject = voteEndGameObjectNetwork.GetComponent<NetworkObject>();
            if (networkObject != null)
            {
                networkObject.SpawnWithOwnership((ulong)clientId);

                // Set the parent on the server side
                var playerObject = NetworkManager.Singleton.ConnectedClients[(ulong)clientId].PlayerObject;
                if (playerObject != null)
                {
                    voteEndGameObjectNetwork.transform.SetParent(playerObject.transform, true);
                }
                else
                {
                    Debug.LogError($"Player object not found for ClientId: {clientId}");
                }

                SpawnVotePopUpClientRpc(networkObject.NetworkObjectId, (ulong)clientId);
            }
            else
            {
                Debug.LogError("Can't get NetworkObject! (SpawnTonextPlayerNetworkServerRpc)");
            }
        }
    }

    [ClientRpc]
    void SpawnVotePopUpClientRpc(ulong networkObjectId, ulong clientId)
    {
        if (NetworkManager.Singleton.SpawnManager.SpawnedObjects.TryGetValue(networkObjectId, out var networkObject))
        {
            GameObject voteEndgameObject = networkObject.gameObject;
            Debug.Log($"VoteEndgameObject spawned for ClientId: {clientId} (SpawnVotePopUpClientRpc)");
            // Perform any client-side updates if necessary
        }
        else
        {
            Debug.LogError($"NetworkObject not found for ID: {networkObjectId}");
        }
    }
    
    [ServerRpc(RequireOwnership =false)]
    void DeleteAllEndGameConditionPopUpServerRpc()
    {
        for (int i = 0; i < NetworkManager.Singleton.ConnectedClients.Count; i++)
        {
            var playerObject = NetworkManager.Singleton.ConnectedClients[(ulong)i].PlayerObject;
            if (playerObject != null)
            {
                foreach (Transform child in playerObject.transform)
                {
                    if (child.name == "EndGameCondition")
                    {
                        var networkObject = child.GetComponent<NetworkObject>();
                        if (networkObject != null)
                        {
                            networkObject.Despawn();
                        }
                    }
                }
            }
        }
    }

    

    
}
