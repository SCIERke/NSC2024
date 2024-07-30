
using Unity.Netcode;
using UnityEngine;

public class NetworkUI : MonoBehaviour
{
    [SerializeField] private Canvas userMonitor; // UI element to toggle visibility

    // Called when the button is pressed by the client or host
    public void OnButtonPress()
    {
        // Toggle the userMonitor state locally without synchronization
        if (userMonitor != null)
        {
            userMonitor.gameObject.SetActive(!userMonitor.gameObject.activeSelf);
        }
        else
        {
            Debug.LogError("UserMonitor canvas is not assigned.");
        }
    }

    // Server setup
    public void OnServer()
    {
        if (NetworkManager.Singleton != null)
        {
            NetworkManager.Singleton.StartServer();
        }
        else
        {
            Debug.LogError("NetworkManager is not initialized.");
        }
    }

    // Host setup
    public void OnHostServer()
    {
        if (NetworkManager.Singleton != null)
        {
            NetworkManager.Singleton.StartHost();
            userMonitor.gameObject.SetActive(false); // Hide user monitor for host
        }
        else
        {
            Debug.LogError("NetworkManager is not initialized.");
        }
    }

    // Client setup
    public void OnClientServer()
    {
        if (NetworkManager.Singleton != null)
        {
            NetworkManager.Singleton.StartClient();
            userMonitor.gameObject.SetActive(false); // Hide user monitor for client
        }
        else
        {
            Debug.LogError("NetworkManager is not initialized.");
        }
    }

    // Stop server
    public void StopServer()
    {
        if (NetworkManager.Singleton != null)
        {
            NetworkManager.Singleton.Shutdown();
        }
        else
        {
            Debug.LogError("NetworkManager is not initialized.");
        }
    }
}


/*
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;




public class NetworkUI : NetworkBehaviour
{
    //[SerializeField] private TextMeshProUGUI countPlayerText;
    [SerializeField] private Canvas userMonitor; // Corrected typo in variable name

    private int countPlayer = 0;

    // Called when the button is pressed by the client or host
    public void OnButtonPress()
    {
        if (IsServer)
        {
            // If it's the server, directly update all clients
            UpdateUserMonitorStateClientRpc(false);
        }
        else
        {
            // If it's a client, send a request to the server
            RequestUpdateUserMonitorStateServerRpc(false);
        }
    }

    public void OnServer()
    {
        if (NetworkManager.Singleton != null)
        {
            NetworkManager.Singleton.StartServer();
            // No need to call SetUserMonitorActiveServerRpc here; it will be handled by OnButtonPress
        }
        else
        {
            Debug.LogError("NetworkManager is not initialized.");
        }
    }

    public void OnHostServer()
    {
        if (NetworkManager.Singleton != null)
        {
            NetworkManager.Singleton.StartHost();
            //d
            //userMonitor.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogError("NetworkManager is not initialized.");
        }
    }

    public void OnClientServer()
    {
        if (NetworkManager.Singleton != null)
        {
            NetworkManager.Singleton.StartClient();
            // No need to call SetUserMonitorActiveServerRpc here; it will be handled by OnButtonPress
            //userMonitor.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogError("NetworkManager is not initialized.");
        }
    }

    public void StopServer()
    {
        if (NetworkManager.Singleton != null)
        {
            NetworkManager.Singleton.Shutdown();
            // No need to call SetUserMonitorActiveServerRpc here; it will be handled by OnButtonPress
        }
        else
        {
            Debug.LogError("NetworkManager is not initialized.");
        }
    }

    private void Update()
    {
        if (!IsServer) return;

        // Uncomment the following lines if you want to display the player count
        // countPlayer = NetworkManager.Singleton.ConnectedClientsList.Count;
        // countPlayerText.text = "PlayerCount :" + countPlayer.ToString();
    }

    // ServerRpc called by clients to request the server to update the user monitor's active state
    [ServerRpc(RequireOwnership = false)]
    private void RequestUpdateUserMonitorStateServerRpc(bool isActive)
    {
        // The server then updates all clients
        UpdateUserMonitorStateClientRpc(isActive);
    }

    // ClientRpc called by the server to update the user monitor's active state on all clients
    [ClientRpc]
    private void UpdateUserMonitorStateClientRpc(bool isActive)
    {
        if (userMonitor != null)
        {
            userMonitor.gameObject.SetActive(isActive);
        }
        else
        {
            Debug.LogError("UserMonitor canvas is not assigned.");
        }
    }


}*/
