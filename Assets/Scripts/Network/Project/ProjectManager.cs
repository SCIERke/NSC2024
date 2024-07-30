using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectManager : NetworkBehaviour
{
    private ProjectDatabaseNetwork projectDatabaseNetwork;

    [SerializeField] public List<int> idProjectDeckList = new List<int>();
    [SerializeField] private int sizeProjectDeck;

    public Transform[] spawnProjectPopUpPoints;

    private bool spawned;
    void Start()
    {
        spawned = false;
        projectDatabaseNetwork = FindObjectOfType<ProjectDatabaseNetwork>();
        if (projectDatabaseNetwork == null )
        {
            Debug.LogError("ProjectDatabaseNetwork not found!");
        }

        NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
    }

    void OnClientConnected(ulong clientId)
    {
        UpdateDeckProjectNetwork();
    }
    
    void UpdateDeckProjectNetwork()
    {
        if (!IsServer) return;

        if (NetworkManager.Singleton.ConnectedClientsList.Count == 4 && !spawned)
        {
            if (AreAllClientsConnected())
            {
                spawned = true;
                SpawnProjectDeck();
            }
        }
    }

    void SpawnProjectDeck()
    {
        if (ProjectDatabaseNetwork.Projects == null || ProjectDatabaseNetwork.Projects.Count == 0)
        {
            Debug.LogError("ProjectDatabaseNetwork is not initialized or empty!");
            return;
        }
        
        idProjectDeckList.Clear();
        for (int i = 0; i < sizeProjectDeck; i++)
        {
            int randomIndex = Random.Range(0, ProjectDatabaseNetwork.Projects.Count);
            idProjectDeckList.Add(randomIndex);

            Debug.Log($"Added Project {i + 1}: " + ProjectDatabaseNetwork.Projects[randomIndex].projectName);
        }
    }


    bool AreAllClientsConnected()
    {
        foreach (var clientId in NetworkManager.Singleton.ConnectedClients.Keys)
        {
            var client = NetworkManager.Singleton.ConnectedClients[clientId];
            if (client == null || client.PlayerObject == null)
            {
                return false;
            }
        }
        return true;
    }

    public ProjectScriptable GetProjectById(int id)
    {
        return ProjectDatabaseNetwork.GetProjectById(id);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
