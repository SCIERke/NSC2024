using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class OnClickPlateCardNetwork : NetworkBehaviour
{
    private FieldClientManager fieldClientManager;
    private NetworkObject networkObject;
    [SerializeField] private GameObject fieldClientManagerTransform;

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        networkObject = fieldClientManagerTransform.GetComponent<NetworkObject>();
        if (networkObject == null)
        {
            Debug.LogError("NetworkObject not found!");
            return;
        }

        if (NetworkManager.Singleton.LocalClientId != networkObject.OwnerClientId)
        {
            return;
        }

        fieldClientManager = fieldClientManagerTransform.GetComponent<FieldClientManager>();
        if (fieldClientManager == null)
        {
            Debug.LogError("FieldClientManager not found!");
            return;
        }

        
    }

    void Update()
    {
        if (NetworkManager.Singleton.LocalClientId != networkObject.OwnerClientId)
        {
            return;
        }


        if (Camera.main == null) return;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform == transform)
                {
                    fieldClientManager.HandleClickPlateNetwork(NetworkManager.Singleton.LocalClientId, transform);
                }
            }
        }
    }
}
