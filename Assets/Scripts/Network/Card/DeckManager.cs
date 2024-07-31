using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Newtonsoft.Json.Bson;
using Unity.VisualScripting;
using System.Runtime.CompilerServices;


public class DeckManager : NetworkBehaviour
{
    [SerializeField] private GameObject cardPrefab;
    
    [SerializeField] private Transform[] spawnDeckCardPoints;

    [SerializeField] private Transform[] spawnCardPoints;

    [SerializeField] private List<int> idCardDeckList = new List<int>();
    [SerializeField] private int sizeDeck;

    //HandPlayer
    [SerializeField] public List<HandPlayerList> cardHandPlayerId = new List<HandPlayerList>();

    //Plate Card
    [SerializeField] public List<int> ListfieldofPlayerNetworkObjectId = new List<int>(); //New

    //[SerializeField] private List<FieldPlateListNetwork> fieldPlateListNetworks = new List<FieldPlateListNetwork>();
    

    private StatPlayerNetwork statPlayerNetwork;
    private bool spawned;
    private CardDatabaseNetwork cardDatabaseNetwork;

    void Start()
    {

        //inialize spawn value -> make it spawn once
        spawned = false;
        //Link Card Database
        cardDatabaseNetwork = FindObjectOfType<CardDatabaseNetwork>();
        if (cardDatabaseNetwork == null) // check card database
        {
            Debug.LogError("CardDatabaseNetwork not found!");
        }

        NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
    }

    void OnClientConnected(ulong userId)
    {
        UpdateDeckNetwork();
    }

    void Update()
    {
        if (!IsOwner) return;

        if (!AreAllClientsConnected() && NetworkManager.Singleton.ConnectedClientsList.Count == 4)
        {
            return;
        }
        //HandleCreateDeckNetwork();
    }
        
    public void HandleSelectedCardNetwork(ulong clientId ,int selectedCard)
    {
        OnSelectedCardServerRpc(clientId, selectedCard);
    }

    [ServerRpc(RequireOwnership = false)]
    void OnSelectedCardServerRpc(ulong clientId, int selectedCard)
    {
        var playerObject = NetworkManager.Singleton.ConnectedClients[clientId].PlayerObject;
        if (playerObject == null)
        {
            Debug.LogError($"Can't find PlayerObject ID: {clientId} (OnSelectedCardServerRpc)");
            return;
        }

        StatPlayerNetwork statPlayerNetwork = playerObject.GetComponent<StatPlayerNetwork>();
        statPlayerNetwork.selectedCardId = selectedCard;
        OnSelectedCardClientRpc(clientId , selectedCard);
    }

    [ClientRpc]
    void OnSelectedCardClientRpc(ulong clientId , int selectedCard)
    {
        var playerObject = NetworkManager.Singleton.LocalClient.PlayerObject;
        if (playerObject == null)
        {
            Debug.LogError($"Can't find PlayerObject ID: {clientId} (OnSelectedCardServerRpc)");
            return;
        }

        StatPlayerNetwork statPlayerNetwork = playerObject.GetComponent<StatPlayerNetwork>();
        if (statPlayerNetwork == null)
        {
            Debug.LogError("Can't find StatPlayerNetwork component on PlayerObject (OnSelectedCardClientRpc)");
            return;
        }

        statPlayerNetwork.selectedCardId = selectedCard;
        Debug.Log($"ClietID {clientId} Selected Card ID : {selectedCard}!");
    }

    public void HandleCreateDeckNetwork(ulong userId)
    {
        DrawCardServerRpc(userId);
    }

    public void OnCardDrawn(int selectedCardId , ulong clientId)
    {
        if (clientId >= (ulong)spawnDeckCardPoints.Length)
        {
            Debug.LogError($"No spawn point assigned for ClientId: {clientId} (CardDrawn)");
            return;
        }
        Transform spawnCardPoint = spawnDeckCardPoints[clientId];
        if (spawnCardPoint == null)
        {
            Debug.LogError($"No spawn point assigned for ClientId: {clientId} (CardDrawn)");
            return;
        }
        CardScriptable cardData = GetCardById(selectedCardId);
        if (cardData != null)
        {
            Debug.Log($"CardSpawn: {cardData.cardName}");

            Vector3 spawnPosition = spawnCardPoint.position + new Vector3(0, 3, 0);
            GameObject newCard = Instantiate(cardPrefab, spawnPosition, Quaternion.Euler(0, -90, 0));
            newCard.name = cardData.cardName;


            DisplayCard displayCard = newCard.GetComponent<DisplayCard>();
            if (displayCard != null)
            {
                displayCard.SetCardData(cardData,clientId);
                displayCard.UpdateCardClientRpc(cardData.id, cardData.cardName, cardData.workingPoints, cardData.actionPoints, cardData.cardType.ToString(), cardData.description, clientId);
            }
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

    // DeckNetwork portion
    void UpdateDeckNetwork()
    {
        if (!IsServer) return;

        if (NetworkManager.Singleton.ConnectedClientsList.Count == 4 && !spawned)
        {
            if (AreAllClientsConnected())
            {
                spawned = true;
                SpawnCardDeck();
            }
        }
    }

    void SpawnCardDeck()
    {
        if (CardDatabaseNetwork.Cards == null || CardDatabaseNetwork.Cards.Count == 0)
        {
            Debug.LogError("CardDatabaseNetwork is not initialized or empty!");
            return;
        }

        idCardDeckList.Clear();
        for (int i = 0; i < sizeDeck; i++)
        {
            int randomIndex = Random.Range(0, CardDatabaseNetwork.Cards.Count);
            idCardDeckList.Add(randomIndex);

            Debug.Log($"Added card {i + 1}: " + CardDatabaseNetwork.Cards[randomIndex].cardName);
        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void DrawCardServerRpc(ulong clientId)
    {
        if (idCardDeckList == null)
        {
            Debug.LogError("Cant Load idCardDeckList");
            return;
        }
        if (idCardDeckList.Count == 0)
        {
            Debug.LogError("No cards left to draw!");
            return;
        }

        int selectedCardId = idCardDeckList[idCardDeckList.Count - 1];
        Debug.Log("selectedCardId :" + selectedCardId.ToString());
        idCardDeckList.RemoveAt(idCardDeckList.Count - 1);

        var playerObject = NetworkManager.Singleton.ConnectedClients[clientId].PlayerObject;

        if (playerObject != null)
        {
            StatPlayerNetwork statPlayerNetwork = playerObject.GetComponent<StatPlayerNetwork>();
            if (statPlayerNetwork != null)
            {
                statPlayerNetwork.selectedCardId = selectedCardId;
                DrawCardClientRpc(selectedCardId, clientId);
            }
            else
            {
                Debug.LogError("StatPlayerNetwork component not found on player object.");
            }
        }
        else
        {
            Debug.LogError("No player object found for clientId: " + clientId);
        }
    }

    [ClientRpc]
    private void DrawCardClientRpc(int selectedCardId, ulong clientId)
    {
        OnCardDrawn(selectedCardId , clientId);
        Debug.Log($"Client {clientId} drew card with ID: {selectedCardId}");
    }

    
    public void HandlePickCardNetwork(ulong clientId ,int idPickCard , Transform deleteCard)
    {
        PickCardNetworkServerRpc(clientId ,idPickCard);
        //Delay(2second);
        OnHandPlayerChangeServerRpc(clientId);
        //DeleteCard(deleteCard);
    }

    [ServerRpc(RequireOwnership =false)]
    void PickCardNetworkServerRpc(ulong clientId ,int idPickCard)
    {
        if (cardHandPlayerId[(int)clientId] == null)
        {
            Debug.LogError($"Cant load Hand of Player clietId : {clientId}");
            return;
        }
        cardHandPlayerId[(int)clientId].handPlayerList.Add(idPickCard);
        //OnHandPlayerChange
        
    }

    [ClientRpc]
    void PickCardNetworkClientRpc(ulong clientId)
    {
        Debug.Log($"Has already added Card ({clientId})");
    }

    private void DeleteCard(Transform cardTransform)
    {
        if (IsServer)
        {
            NetworkObject networkObject = cardTransform.GetComponent<NetworkObject>();
            if (networkObject != null)
            {
                networkObject.Despawn();
            }
        }
        else
        {
            DeleteCardServerRpc(cardTransform.GetComponent<NetworkObject>().NetworkObjectId);
        }
    }

    [ServerRpc(RequireOwnership = false)]
    private void DeleteCardServerRpc(ulong networkObjectId)
    {
        NetworkObject networkObject = NetworkManager.SpawnManager.SpawnedObjects[networkObjectId];
        if (networkObject != null)
        {
            networkObject.Despawn();
        }
    }

    [ClientRpc]
    public void DeleteCardClientRpc(ulong clientId, int cardId)
    {
        // Ensure only the owner of the card deletes it
        if (NetworkManager.Singleton.LocalClientId == clientId)
        {
            Destroy(gameObject);
        }
    }

    [ServerRpc(RequireOwnership = false)]
    void OnHandPlayerChangeServerRpc(ulong clientId)
    {
        if (clientId >= (ulong)spawnCardPoints.Length)
        {
            Debug.LogError($"No spawn point assigned for ClientId: {clientId} (DeckManager-OnHandPlayerChange)");
            return;
        }

        if (cardHandPlayerId[(int)clientId] == null)
        {
            Debug.LogError($"Can't find PlayerHand for ClientId: {clientId}");
            return;
        }

        Transform spawnCardPoint = spawnCardPoints[(int)clientId];
        if (spawnCardPoint == null)
        {
            Debug.LogError($"No spawnPoints for HandPlayer assigned for ClientId: {clientId} (DeckManager-OnHandPlayerChange)");
            return;
        }

        var playerObjectToDelete = NetworkManager.Singleton.ConnectedClients[clientId].PlayerObject;
        if (playerObjectToDelete == null)
        {
            Debug.LogError("Can't find PlayerObjectToDelete (HandControllerPlayerRpc)");
            return;
        }

        List<ulong> objectIdsToDelete = new List<ulong>();
        foreach (Transform child in playerObjectToDelete.transform)
        {
            if (child.name != "UserMoniter" && child.name != "Capsule" && child.name != "StatPlayer" 
                && child.name != "PlayerCamera" && child.name != "Field" && child.name != "MoniterCanvas"
                && child.name != "SpectatorTool" && child.name != "SeeOtherPlayerCanvas" && child.name != "TonextPlayer" 
                && child.name != "EndGameCondition" && child.name != "VoteEndGamePopUp")
            {
                var networkObject = child.GetComponent<NetworkObject>();
                if (networkObject != null)
                {
                    objectIdsToDelete.Add(networkObject.NetworkObjectId);
                }
            }
        }

        // Server-side despawning
        foreach (ulong objectId in objectIdsToDelete)
        {
            if (NetworkManager.Singleton.SpawnManager.SpawnedObjects.TryGetValue(objectId, out var networkObject))
            {
                networkObject.Despawn();
            }
            else
            {
                Debug.LogError($"NetworkObject not found for ID: {objectId}");
            }
        }

        //DeleteObjectsClientRpc(objectIdsToDelete.ToArray()); // Notify clients about deletion

        // Spawn new cards
        for (int i = 0; i < cardHandPlayerId[(int)clientId].handPlayerList.Count; i++)
        {
            CardScriptable cardData = GetCardById(cardHandPlayerId[(int)clientId].handPlayerList[i]);
            if (cardData == null)
            {
                Debug.LogError($"Card data not found for ID: {cardHandPlayerId[(int)clientId].handPlayerList[i]}");
                continue;
            }

            Debug.Log($"CardSpawn: {cardData.cardName}");

            Vector3 spawnPosition = spawnCardPoint.position + new Vector3(0, 0, -i * 0.4f); // Adjust spacing if needed
            GameObject newCard = Instantiate(cardPrefab, spawnPosition, Quaternion.Euler(0, 0, 0));
            newCard.name = cardData.cardName;

            NetworkObject networkObject = newCard.GetComponent<NetworkObject>();
            if (networkObject != null)
            {
                networkObject.SpawnWithOwnership(clientId);
            }
            else
            {
                Debug.LogError("NetworkObject component is missing on the card prefab.");
            }

            DisplayCard displayCard = newCard.GetComponent<DisplayCard>();
            if (displayCard != null)
            {
                displayCard.SetCardData(cardData, clientId);
                displayCard.UpdateCardClientRpc(cardData.id, cardData.cardName, cardData.workingPoints, cardData.actionPoints, cardData.cardType.ToString(), cardData.description, clientId);
            }
            else
            {
                Debug.LogError("DisplayCard component is missing on the card prefab.");
            }

            // Server handles reparenting
            ReparentCardServerRpc(newCard.GetComponent<NetworkObject>().NetworkObjectId, clientId);
        }
    }
    [ServerRpc]
    void ReparentCardServerRpc(ulong cardNetworkObjectId, ulong clientId)
    {
        if (NetworkManager.Singleton.SpawnManager.SpawnedObjects.TryGetValue(cardNetworkObjectId, out var networkObject))
        {
            GameObject cardObject = networkObject.gameObject;
            var playerObject = NetworkManager.Singleton.ConnectedClients[clientId].PlayerObject;
            if (playerObject != null)
            {
                cardObject.transform.SetParent(playerObject.transform, true);
            }
            else
            {
                Debug.LogError($"Player object not found for ClientId: {clientId}");
            }
        }
        else
        {
            Debug.LogError($"NetworkObject not found for ID: {cardNetworkObjectId}");
        }
    }

    [ClientRpc]
    void DeleteObjectsClientRpc(ulong[] objectIds)
    {
        foreach (ulong objectId in objectIds)
        {
            if (NetworkManager.Singleton.SpawnManager.SpawnedObjects.TryGetValue(objectId, out var networkObject))
            {
                networkObject.Despawn();
            }
            else
            {
                Debug.LogError($"NetworkObject not found for ID: {objectId}");
            }
        }
    }

    public void HandleRemoveCardOfPlayer(ulong clientId , int seletedCard)
    {
        HandleRemoveCardOfPlayerServerRpc(clientId, seletedCard);
    }

    [ServerRpc(RequireOwnership = false)]
    void HandleRemoveCardOfPlayerServerRpc(ulong clientId, int seletedCard)
    {
        Debug.Log($"selected :{seletedCard}");
        var playerObject = NetworkManager.Singleton.ConnectedClients[clientId].PlayerObject;

        if (playerObject == null)
        {
            Debug.LogError("Can't find PlayerObject!");
            return;
        }

        CardScriptable cardData = GetCardById(seletedCard);
        if (cardData == null)
        {
            Debug.LogError("Can't find cardData! ,Retrying...");
            cardData = GetCardById(seletedCard);
            if (cardData == null)
            {
                Debug.LogError("Fail to find cardData!");
                return;
            }
            
        }

        List<ulong> objectIdsToDelete = new List<ulong>();

        foreach (Transform child in playerObject.transform)
        {
            if (child.name != "UserMoniter" && child.name != "Capsule" && child.name != "StatPlayer" && child.name != "PlayerCamera" && child.name != "Field")
            {
                var networkObject = child.GetComponent<NetworkObject>();
                DisplayCard displayCard = child.GetComponent<DisplayCard>();

                if (networkObject != null && displayCard != null && displayCard.id == cardData.id)
                {
                    objectIdsToDelete.Add(networkObject.NetworkObjectId);
                    break;
                }
            }
        }

        // Handle deletion of network objects here
        foreach (var objectId in objectIdsToDelete)
        {
            if (NetworkManager.Singleton.SpawnManager.SpawnedObjects.TryGetValue(objectId, out var obj))
            {
                obj.Despawn();
            }
        }
        OnHandPlayerChangeServerRpc(clientId);
        HandleRemoveCardOfPlayerClientRpc(clientId , cardData.name);
    }
    


    [ClientRpc]
    void HandleRemoveCardOfPlayerClientRpc(ulong clientId, string CardName)
    {
        Debug.Log($"Delete Card in PlayerPrefeb({clientId}) -> CardName: {CardName}!");
    }

    public CardScriptable GetCardById(int id)
    {
        return CardDatabaseNetwork.GetCardById(id);
    }
}
