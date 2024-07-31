
/*
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Netcode;
using UnityEngine;

public class TurnSystemNetwork : NetworkBehaviour
{
    private OnWaitingPlayer onWaitingPlayer;
    [SerializeField] private Canvas gameStartCanvas;
    [SerializeField] private int turnOfPlayer;
    [SerializeField] private int day;

    private SpectatorManager spectatorManager;
    
    private bool started;

    void Start()
    {
        onWaitingPlayer = FindObjectOfType<OnWaitingPlayer>();
        if (onWaitingPlayer == null)
        {
            Debug.LogError("Can't find OnWaitingPlayer Component!");
            return;
        }
        spectatorManager = FindObjectOfType<SpectatorManager>();
        if (spectatorManager == null)
        {
            Debug.LogError("Can't find SpectatorManager Component!");
            return;
        }
        started = false;
    }

    void Update()
    {
        if (!IsServer) return;

        //StartGame Function
        if (NetworkManager.Singleton.ConnectedClients.Count >= 4 && !started)
        {
            HandleGameStartCanvasSetServerRpc(true);
            started = true ;
            turnOfPlayer = 0;

            //yeild to 3 second!;

            HandleOnChangeTurnPlayer(turnOfPlayer);
        }


    }

    void HandleOnChangeTurnPlayer(int turnofplayer)
    {
        for (int i = 0; i < NetworkManager.Singleton.ConnectedClients.Count; i++)
        {
            OnChangeTurnPlayerClientRpc(i);
        }
    }

    [ClientRpc]
    void OnChangeTurnPlayerClientRpc(int turnofplayer)
    {
        if (NetworkManager.Singleton.LocalClientId != (ulong)turnofplayer)
        {
            return;
        }
        spectatorManager.SwitchCamera(turnofplayer);
    }

    [ServerRpc(RequireOwnership = false)]
    void HandleGameStartCanvasSetServerRpc(bool setEnable)
    {
        // Set canvas on the server
        gameStartCanvas.gameObject.SetActive(setEnable);

        // Call the ClientRpc to set the canvas on all clients
        HandleGameStartCanvasSetClientRpc(setEnable);

        if (setEnable)
        {
            StartCoroutine(DisableCanvasAfterDelay());
        }
    }

    private IEnumerator DisableCanvasAfterDelay()
    {
        yield return new WaitForSeconds(2f);

        gameStartCanvas.gameObject.SetActive(false);
        HandleGameStartCanvasSetClientRpc(false);
    }

    [ClientRpc]
    void HandleGameStartCanvasSetClientRpc(bool setEnable)
    {
        gameStartCanvas.gameObject.SetActive(setEnable);
    }
}
*/

/*
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class TurnSystemNetwork : NetworkBehaviour
{
    private OnWaitingPlayer onWaitingPlayer;
    [SerializeField] private Canvas gameStartCanvas;
    public int turnOfPlayer;
    [SerializeField] private int day;

    private SpectatorManager spectatorManager;

    private bool started;

    void Start()
    {
        onWaitingPlayer = FindObjectOfType<OnWaitingPlayer>();
        if (onWaitingPlayer == null)
        {
            Debug.LogError("Can't find OnWaitingPlayer Component!");
            return;
        }
        spectatorManager = FindObjectOfType<SpectatorManager>();
        if (spectatorManager == null)
        {
            Debug.LogError("Can't find SpectatorManager Component!");
            return;
        }
        started = false;
    }

    void Update()
    {
        if (!IsServer) return;

        //StartGame Function
        if (NetworkManager.Singleton.ConnectedClients.Count >= 4 && !started)
        {
            Debug.Log("Game Start!");
            HandleGameStartCanvasSetServerRpc(true);
            started = true;
            turnOfPlayer = 0;
            StartCoroutine(StartGameSequence());
        }
    }

    private IEnumerator StartGameSequence()
    {
        yield return new WaitForSeconds(3f);
        setTurnPlayerOnStart(turnOfPlayer);
    }
    
    [ServerRpc(RequireOwnership = false)]
    public void HandleOnChangeTurnPlayerServerRpc(int turnOfPlayer)
    {
        HandleChangeTurnAndDayServerRpc();
        for (int i = 0; i < 4; i++)
        {
            OnChangeTurnPlayerClientRpc(turnOfPlayer,i);
        }
        
    }


    
    [ServerRpc(RequireOwnership = false)]
    void HandleChangeTurnAndDayServerRpc()
    {
        turnOfPlayer += 1;
        turnOfPlayer %= 4;
        if (turnOfPlayer == 0 && day != 0)
        {
            day += 1;
        }
        HandleChangeTurnAndDayClientRpc();
    }

    [ClientRpc]
    void HandleChangeTurnAndDayClientRpc()
    {
        turnOfPlayer += 1;
        turnOfPlayer %= 4;
        if (turnOfPlayer == 0 && day != 0)
        {
            day += 1;
        }
    }

    public void setTurnPlayerOnStart(int turnOfPlayer)
    {
        for (int i = 0; i < 4; i++)
        {
            OnChangeTurnPlayerClientRpc(turnOfPlayer, i);
        }
    }


    [ClientRpc]
    void OnChangeTurnPlayerClientRpc(int turnOfPlayer, int clientIndex)
    {
        if (NetworkManager.Singleton.LocalClientId != (ulong)clientIndex) return;

        if (NetworkManager.Singleton.LocalClientId != (ulong)turnOfPlayer)
        {
            Debug.LogError($"{clientIndex} - Focusing on turn player {turnOfPlayer}");
            spectatorManager.SwitchCamera(turnOfPlayer);
        }
        else
        {
            Debug.LogError($"{clientIndex} - Closing all cameras");
            spectatorManager.CloseAllCameras();
        }
    }




    [ServerRpc(RequireOwnership = false)]
    void HandleGameStartCanvasSetServerRpc(bool setEnable)
    {
        // Set canvas on the server
        gameStartCanvas.gameObject.SetActive(setEnable);

        // Call the ClientRpc to set the canvas on all clients
        HandleGameStartCanvasSetClientRpc(setEnable);

        if (setEnable)
        {
            StartCoroutine(DisableCanvasAfterDelay());
        }
    }

    private IEnumerator DisableCanvasAfterDelay()
    {
        yield return new WaitForSeconds(2f);

        gameStartCanvas.gameObject.SetActive(false);
        HandleGameStartCanvasSetClientRpc(false);
    }

    [ClientRpc]
    void HandleGameStartCanvasSetClientRpc(bool setEnable)
    {
        gameStartCanvas.gameObject.SetActive(setEnable);
    }
}
*/

using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class TurnSystemNetwork : NetworkBehaviour
{
    private OnWaitingPlayer onWaitingPlayer;
    [SerializeField] private Canvas gameStartCanvas;
    public int turnOfPlayer;
    [SerializeField] public int day;
    [SerializeField] private GameObject endGamePlayerPrefeb;
    [SerializeField] private Transform[] endGameSpawnPoints;

    private SpectatorManager spectatorManager;
    private bool spawnedEndGameCondition;

    private bool started;

    void Start()
    {
        onWaitingPlayer = FindObjectOfType<OnWaitingPlayer>();
        if (onWaitingPlayer == null)
        {
            Debug.LogError("Can't find OnWaitingPlayer Component!");
            return;
        }
        spectatorManager = FindObjectOfType<SpectatorManager>();
        if (spectatorManager == null)
        {
            Debug.LogError("Can't find SpectatorManager Component!");
            return;
        }
        spawnedEndGameCondition = true;
        started = false;
    }

    void Update()
    {
        if (!IsServer) return;
        /*
        if (NetworkManager.Singleton.ConnectedClients.Count >= 4 && day >= 10 && spawnedEndGameCondition)
        {
            yield 2 second

            for (int i = 0; i < NetworkManager.Singleton.ConnectedClientsList.Count; i++)
            {
                SpawnendGameConditionServerRpc((ulong)i);
            }
            spawnedEndGameCondition = false;
        }*/

        if (NetworkManager.Singleton.ConnectedClients.Count >= 4 && day >= 10 && spawnedEndGameCondition)
        {
            StartCoroutine(HandleEndGameCondition());
            spawnedEndGameCondition = false;
        }

        // StartGame Function
        if (NetworkManager.Singleton.ConnectedClients.Count >= 4 && !started)
        {
            Debug.Log("Game Start!");
            HandleGameStartCanvasSetServerRpc(true);
            started = true;
            turnOfPlayer = 0;
            StartCoroutine(StartGameSequence());
        }
    }

    private IEnumerator HandleEndGameCondition()
    {
        yield return new WaitForSeconds(2f);

        for (int i = 0; i < NetworkManager.Singleton.ConnectedClientsList.Count; i++)
        {
            SpawnendGameConditionServerRpc((ulong)i);
        }
    }

    [ServerRpc(RequireOwnership = false)]
    void SpawnendGameConditionServerRpc(ulong clientId)
    {
        if (clientId >= (ulong)endGameSpawnPoints.Length)
        {
            Debug.LogError($"No spawn points for EndGameCondition assigned for ClientId: {clientId} (SpawnendGameConditionServerRpc)");
            return;
        }
        Transform endGameSpawnPoint = endGameSpawnPoints[clientId];
        if (endGameSpawnPoint == null)
        {
            Debug.LogError($"No spawn point assigned for ClientId: {clientId} (SpawnendGameConditionServerRpc)");
            return;
        }
        Quaternion rot = Quaternion.Euler(0, 180, 0);
        GameObject EndGameObjectNetwork = Instantiate(endGamePlayerPrefeb, endGameSpawnPoint.position, rot);
        EndGameObjectNetwork.name = "EndGameCondition";
        NetworkObject networkObject = EndGameObjectNetwork.GetComponent<NetworkObject>();
        if (networkObject != null)
        {
            networkObject.SpawnWithOwnership(clientId);

            // Set the parent on the server side
            var playerObject = NetworkManager.Singleton.ConnectedClients[clientId].PlayerObject;
            if (playerObject != null)
            {
                EndGameObjectNetwork.transform.SetParent(playerObject.transform, true);
            }
            else
            {
                Debug.LogError($"Player object not found for ClientId: {clientId}");
            }

            SpawnendGameConditionClientRpc(networkObject.NetworkObjectId, clientId);
        }
        else
        {
            Debug.LogError("Can't get NetworkObject! (SpawnTonextPlayerNetworkServerRpc)");
        }
    }

    [ClientRpc]
    void SpawnendGameConditionClientRpc(ulong networkObjectId, ulong clientId)
    {
        if (NetworkManager.Singleton.SpawnManager.SpawnedObjects.TryGetValue(networkObjectId, out var networkObject))
        {
            GameObject EndgameObject = networkObject.gameObject;
            Debug.Log($"EndgameObject spawned for ClientId: {clientId} (SpawnendGameConditionClientRpc)");
            // Perform any client-side updates if necessary
        }
        else
        {
            Debug.LogError($"NetworkObject not found for ID: {networkObjectId}");
        }
    }

    /*
    [ServerRpc(RequireOwnership = false)]
    void SpawnTonextPlayerNetworkServerRpc(ulong clientId)
    {
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

            SpawnTonextPlayerNetworkClientRpc(networkObject.NetworkObjectId, clientId);
        }
        else
        {
            Debug.LogError("Can't get NetworkObject! (SpawnTonextPlayerNetworkServerRpc)");
        }
    }

    [ClientRpc]
    void SpawnTonextPlayerNetworkClientRpc(ulong networkObjectId, ulong clientId)
    {
        if (NetworkManager.Singleton.SpawnManager.SpawnedObjects.TryGetValue(networkObjectId, out var networkObject))
        {
            GameObject TonextToolObject = networkObject.gameObject;
            Debug.Log($"CanvasMoniter object spawned for ClientId: {clientId} (SpawnTonextPlayerNetworkClientRpc)");
            // Perform any client-side updates if necessary
        }
        else
        {
            Debug.LogError($"NetworkObject not found for ID: {networkObjectId}");
        }
    }
    */

    private IEnumerator StartGameSequence()
    {
        yield return new WaitForSeconds(3f);
        SetTurnPlayerOnStart(turnOfPlayer);
    }

    [ServerRpc(RequireOwnership = false)]
    public void HandleOnChangeTurnPlayerServerRpc(int currentTurnOfPlayer)
    {
        HandleChangeTurnAndDayServerRpc();
        for (int i = 0; i < 4; i++)
        {
            OnChangeTurnPlayerClientRpc(turnOfPlayer, i);
        }
    }

    [ServerRpc(RequireOwnership = false)]
    void HandleChangeTurnAndDayServerRpc()
    {
        turnOfPlayer = (turnOfPlayer + 1) % 4;
        if (turnOfPlayer == 0)
        {
            day++;
        }
        HandleChangeTurnAndDayClientRpc(turnOfPlayer, day);
    }

    [ClientRpc]
    void HandleChangeTurnAndDayClientRpc(int newTurnOfPlayer, int newDay)
    {
        turnOfPlayer = newTurnOfPlayer;
        day = newDay;
    }

    public void SetTurnPlayerOnStart(int initialTurnOfPlayer)
    {
        for (int i = 0; i < 4; i++)
        {
            OnChangeTurnPlayerClientRpc(initialTurnOfPlayer, i);
        }
    }

    [ClientRpc]
    void OnChangeTurnPlayerClientRpc(int currentTurnOfPlayer, int clientIndex)
    {
        if (NetworkManager.Singleton.LocalClientId != (ulong)clientIndex) return;

        if (NetworkManager.Singleton.LocalClientId != (ulong)currentTurnOfPlayer)
        {
            Debug.Log($"{clientIndex} - Focusing on turn player {currentTurnOfPlayer}");
            spectatorManager.SwitchCamera(currentTurnOfPlayer);
        }
        else
        {
            Debug.Log($"{clientIndex} - Closing all cameras");
            spectatorManager.CloseAllCameras();
        }
    }

    [ServerRpc(RequireOwnership = false)]
    void HandleGameStartCanvasSetServerRpc(bool setEnable)
    {
        // Set canvas on the server
        gameStartCanvas.gameObject.SetActive(setEnable);

        // Call the ClientRpc to set the canvas on all clients
        HandleGameStartCanvasSetClientRpc(setEnable);

        if (setEnable)
        {
            StartCoroutine(DisableCanvasAfterDelay());
        }
    }

    private IEnumerator DisableCanvasAfterDelay()
    {
        yield return new WaitForSeconds(2f);

        gameStartCanvas.gameObject.SetActive(false);
        HandleGameStartCanvasSetClientRpc(false);
    }

    [ClientRpc]
    void HandleGameStartCanvasSetClientRpc(bool setEnable)
    {
        gameStartCanvas.gameObject.SetActive(setEnable);
    }
}
