
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
using UnityEngine;

public class TurnSystemNetwork : NetworkBehaviour
{
    private OnWaitingPlayer onWaitingPlayer;
    [SerializeField] private Canvas gameStartCanvas;
    public int turnOfPlayer;
    [SerializeField] public int day;

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
