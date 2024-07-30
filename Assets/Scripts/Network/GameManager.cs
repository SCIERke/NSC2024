using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : NetworkBehaviour
{/*/
    [SerializeField] private GameObject requestClientSpawnCardNetworkPrefab;
    private int amountPlayer = 0;
    
    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            SpawnRequestClientSpawnCardNetwork();
        }
    }

    private void SpawnRequestClientSpawnCardNetwork()
    {
        GameObject requestClientSpawnCardNetworkInstance = Instantiate(requestClientSpawnCardNetworkPrefab);
        NetworkObject networkObject = requestClientSpawnCardNetworkInstance.GetComponent<NetworkObject>();
        networkObject.Spawn();
    }*/
}