using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class HandleClickTonextPlayerNetwork : NetworkBehaviour
{
    private TurnSystemNetwork turnSystemNetwork;

    void Start()
    {
        turnSystemNetwork = FindObjectOfType<TurnSystemNetwork>();
        if (turnSystemNetwork == null )
        {
            Debug.LogError("Can't find TurnSystemNetwork Component!");
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner) return;

        if (Camera.main == null) return;
        turnSystemNetwork = FindObjectOfType<TurnSystemNetwork>();
        if (turnSystemNetwork == null)
        {
            Debug.LogError("Can't find TurnSystemNetwork Component!");
            return;
        }

        if (turnSystemNetwork.turnOfPlayer != (int)NetworkManager.Singleton.LocalClientId) return;

        if (turnSystemNetwork != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Input.GetKeyDown(KeyCode.F) && Physics.Raycast(ray, out RaycastHit hit))
            {   
                if (hit.transform == transform)
                {
                    Debug.Log("Click!");
                    
                    turnSystemNetwork.HandleOnChangeTurnPlayerServerRpc(turnSystemNetwork.turnOfPlayer);
                }
            }
        }
    }
}
