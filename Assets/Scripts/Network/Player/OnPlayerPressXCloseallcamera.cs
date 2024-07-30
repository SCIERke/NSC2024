using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class OnPlayerPressXCloseallcamera : NetworkBehaviour
{
    public override void OnNetworkSpawn()
    {
        if (!IsOwner) return;
        spectatorManager = FindObjectOfType<SpectatorManager>();
    }

    private SpectatorManager spectatorManager;
    void Update()
    {
        if (!IsOwner) return;
        if (Input.GetKeyUp(KeyCode.X))
        {
            spectatorManager.CloseAllCameras();
        }
    }
}
