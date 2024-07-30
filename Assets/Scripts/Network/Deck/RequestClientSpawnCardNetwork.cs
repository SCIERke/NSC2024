using Unity.Netcode;
using UnityEngine;

public class RequestClientSpawnCardNetwork : NetworkBehaviour
{
    private DeckManager deckManager;

    void Start()
    {
        if (!IsOwner) return;

        deckManager = FindObjectOfType<DeckManager>();
        if (deckManager == null)
        {
            Debug.LogError("Can't find DeckManager");
        }
    }

    private void Update()
    {
        if (!IsOwner) return;

        if (Camera.main == null) return;

        if (deckManager != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Input.GetKeyDown(KeyCode.F) && Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform == transform)
                {
                    Debug.Log("Click!");
                    HandleCreateDeckNetworkServerRpc(NetworkManager.Singleton.LocalClientId);
                }
            }
        }
        else
        {
            deckManager = FindObjectOfType<DeckManager>();
            if (deckManager == null)
            {
                Debug.LogWarning("DeckManager not found, will retry.");
            }
        }
    }

    

    [ServerRpc(RequireOwnership = false)]
    private void HandleCreateDeckNetworkServerRpc(ulong clientId)
    {
        Debug.Log("Received request to handle deck network.");

        if (deckManager == null)
        {
            Debug.LogError("DeckManager is null, retrying.");
            deckManager = FindObjectOfType<DeckManager>();

            if (deckManager == null)
            {
                Debug.LogError("Failed to find DeckManager.");
                return;
            }
        }

        Debug.Log("Processing deck network request.");
        deckManager.HandleCreateDeckNetwork(clientId);
    }
}
