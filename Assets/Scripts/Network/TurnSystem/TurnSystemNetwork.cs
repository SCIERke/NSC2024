
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
            HandleGameStartCanvasSetServerRpc(true);
            started = true;
            turnOfPlayer = 0;
            StartCoroutine(StartGameSequence());
        }
    }

    private IEnumerator StartGameSequence()
    {
        yield return new WaitForSeconds(3f);
        HandleOnChangeTurnPlayer(turnOfPlayer);
    }

    public void HandleOnChangeTurnPlayer(int turnOfPlayer)
    {
        for (int i = 0; i < 4; i++)
        {
            OnChangeTurnPlayerClientRpc(turnOfPlayer,i);
        }
        
    }

    [ClientRpc(RequireOwnership = false)]
    void OnChangeTurnPlayerClientRpc(int turnOfPlayer ,int whocall)
    {
        if (NetworkManager.Singleton.LocalClientId == (ulong)whocall && NetworkManager.Singleton.LocalClientId != (ulong)turnOfPlayer)
        {
            spectatorManager.SwitchCamera(turnOfPlayer);
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
