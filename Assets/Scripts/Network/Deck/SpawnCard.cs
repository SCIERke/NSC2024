/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class SpawnCard : NetworkBehaviour
{
    private DeckNetwork deckNetwork;
    private StatPlayerNetwork statPlayerNetwork;
    [SerializeField] private GameObject cardPrefab;

    void Start()
    {
        if (!IsOwner) return;

        deckNetwork = FindObjectOfType<DeckNetwork>();
        statPlayerNetwork = FindObjectOfType<StatPlayerNetwork>();
    }

    void Update()
    {
        if (!IsOwner) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit) && Input.GetKeyDown(KeyCode.F))
        {
            if (hit.transform == transform)
            {
                // Request the server to draw a card for this client
                deckNetwork.DrawCardServerRpc(NetworkManager.Singleton.LocalClientId);
            }
        }
    }

    // This method should be called on the client after the server response
    public void OnCardDrawn(int selectedCardId)
    {
        // Retrieve card data based on the ID
        CardScriptable cardData = deckNetwork.GetCardById(selectedCardId);
        if (cardData != null)
        {
            Debug.Log($"CardSpawn: {cardData.cardName}");

            // Set spawn position
            Vector3 spawnPosition = transform.position + new Vector3(0, 3, 0);

            // Instantiate the card prefab
            GameObject newCard = Instantiate(cardPrefab, spawnPosition, Quaternion.Euler(0, -90, 0));

            // Set card data to the prefab (if applicable)
            DisplayCard displayCard = newCard.GetComponent<DisplayCard>();
            if (displayCard != null)
            {
                displayCard.SetCardData(cardData);
            }

            newCard.name = cardData.cardName;
        }
        else
        {
            Debug.LogError("Card data not found for ID: " + selectedCardId);
        }
    }
}
*/

using UnityEngine;
using Unity.Netcode;
/*
public class SpawnCard : NetworkBehaviour
{
    private DeckNetwork deckNetwork;
    private StatPlayerNetwork statPlayerNetwork;
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private Transform spawnCard1;
    [SerializeField] private Transform spawnCard2;
    [SerializeField] private Transform spawnCard3;
    [SerializeField] private Transform spawnCard4;

    private bool usedDeckNetwork;

    void Start()
    {

    }

    void Update()
    {
        
        if (!AreAllClientsConnected() && NetworkManager.Singleton.ConnectedClientsList.Count == 4)
        {
            return;
        }

        deckNetwork = FindObjectOfType<DeckNetwork>();

        if (deckNetwork != null )
        {
            handleCreateDecknetwork();
        }
    }*/

    /*
    void handleCreateDecknetwork()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit) && Input.GetKeyDown(KeyCode.F))
        {
            if (hit.transform == transform)
            {
                // Debug.Log("Card has already spawned");
                if (deckNetwork != null)
                {
                    deckNetwork.DrawCardServerRpc(NetworkManager.Singleton.LocalClientId);
                }
                else
                {
                    Debug.LogError("DeckNetwork reference is null in Update.");
                }
            }
        }
    }

    public void OnCardDrawn(int selectedCardId)
    {
        if (deckNetwork == null)
        {
            Debug.LogError("DeckNetwork reference is null.");
            return;
        }

        CardScriptable cardData = deckNetwork.GetCardById(selectedCardId);
        if (cardData != null)
        {
            Debug.Log($"CardSpawn: {cardData.cardName}");

            Vector3 spawnPosition = transform.position + new Vector3(0, 3, 0);
            GameObject newCard = Instantiate(cardPrefab, spawnPosition, Quaternion.Euler(0, -90, 0));

            DisplayCard displayCard = newCard.GetComponent<DisplayCard>();
            if (displayCard != null)
            {
                displayCard.SetCardData(cardData);
            }

            newCard.name = cardData.cardName;
        }
        else
        {
            Debug.LogError("Card data not found for ID: " + selectedCardId);
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
    */
