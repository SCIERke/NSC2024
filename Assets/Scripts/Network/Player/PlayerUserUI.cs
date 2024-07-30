using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerUserUI : NetworkBehaviour
{
    public override void OnNetworkSpawn()
    {
        if (!IsOwner)
        {
            gameObject.SetActive(false);
        }
        base.OnNetworkSpawn();
    }

    void Update()
    {
        // Add any per-frame logic here
    }
}
