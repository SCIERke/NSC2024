using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class OnPlayerPressPPlaceCard : NetworkBehaviour
{
    private StatPlayerNetwork statPlayerNetwork;
    private NetworkObject networkObject;
    private DeckManager deckManager;

    [SerializeField] private GameObject CardTemplatePrefeb;

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        networkObject = GetComponent<NetworkObject>();
        if (networkObject == null)
        {
            Debug.LogError("NetworkObject not found!");
            return;
        }

        if (NetworkManager.Singleton.LocalClientId != networkObject.OwnerClientId)
        {
            return;
        }

        statPlayerNetwork = GetComponent<StatPlayerNetwork>();
        if (statPlayerNetwork == null)
        {
            Debug.LogError("StatPlayerNetwork not found!");
            return;
        }

        deckManager = FindObjectOfType<DeckManager>();
        if (deckManager == null)
        {
            Debug.LogError("DeckManager not found!");
            return;
        }
    }

    void Update()
    {
        if (NetworkManager.Singleton.LocalClientId != networkObject.OwnerClientId)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            ulong clientId = NetworkManager.Singleton.LocalClientId;
            if (deckManager == null || statPlayerNetwork == null)
            {
                Debug.LogError("Dependencies not found (OnPressP)!");
                return;
            }


            DeleteCardInPlayerHandServerRpc((int)clientId);
            deckManager.HandleRemoveCardOfPlayer(clientId, statPlayerNetwork.selectedCardId);
            SpawnCardatAtPlateServerRpc(clientId, statPlayerNetwork.selectedCardId, statPlayerNetwork.selectedPlateId);
            ChangeWorkingPointsPlayerUIServerRpc(clientId);
        }
    }

    [ServerRpc(RequireOwnership = false)]
    void ChangeWorkingPointsPlayerUIServerRpc(ulong clientId)
    {
        var playerObject = NetworkManager.Singleton.ConnectedClients[clientId].PlayerObject;
        if (playerObject == null)
        {
            Debug.LogError("Can't find PlayerObject!");
            return;
        }
        GameObject childTransform = playerObject.transform.GetChild(0).gameObject;
        MoniterPlayerControllerNetwork moniterPlayerControllerNetwork = childTransform.GetComponent<MoniterPlayerControllerNetwork>();
        statPlayerNetwork = playerObject.GetComponent<StatPlayerNetwork>();

        if (statPlayerNetwork == null)
        {
            Debug.LogError("Can't find statPlayerNetwork ,Retrying...");
            statPlayerNetwork = playerObject.GetComponent<StatPlayerNetwork>();
            if (statPlayerNetwork == null)
            {
                Debug.LogError("Fail to find StatPlayer!");
                return;
            }
        }

        moniterPlayerControllerNetwork.workingPointText.text = statPlayerNetwork.workingPoints.ToString();  
        ChangeWorkingPointsPlayerUIClientRpc(clientId , statPlayerNetwork.workingPoints);
    }

    [ClientRpc]
    void ChangeWorkingPointsPlayerUIClientRpc(ulong clientId ,int workingPoint)
    {
        if (NetworkManager.Singleton.LocalClientId != clientId)
        {
            return;
        }
        var playerObject = NetworkManager.Singleton.LocalClient.PlayerObject;
        if (playerObject == null)
        {
            Debug.LogError("Can't find PlayerObject!");
            return;
        }
        GameObject childTransform = playerObject.transform.GetChild(0).gameObject;
        MoniterPlayerControllerNetwork moniterPlayerControllerNetwork = childTransform.GetComponent<MoniterPlayerControllerNetwork>();
        statPlayerNetwork = playerObject.GetComponent<StatPlayerNetwork>();

        if (statPlayerNetwork == null)
        {
            Debug.LogError("Can't find statPlayerNetwork ,Retrying...");
            statPlayerNetwork = playerObject.GetComponent<StatPlayerNetwork>();
            if (statPlayerNetwork == null)
            {
                Debug.LogError("Fail to find StatPlayer!");
                return;
            }
        }
        moniterPlayerControllerNetwork.workingPointText.text = workingPoint.ToString();


        Debug.Log($"PlayerObject({clientId}) set workingpoint to : {workingPoint}");
    }

    [ServerRpc(RequireOwnership = false)]
    void DeleteCardInPlayerHandServerRpc(int clientId)
    {
        DeckManager deckManager = FindObjectOfType<DeckManager>();
        statPlayerNetwork = transform.GetComponent<StatPlayerNetwork>();

        while (deckManager == null)
        {
            deckManager = FindObjectOfType<DeckManager>();
            Debug.LogError("DeckManager not found!");
        }

        while (statPlayerNetwork== null)
        {
            statPlayerNetwork = transform.GetComponent<StatPlayerNetwork>();
            Debug.LogError("StatPlayerNetwork not found!");
        }

        for (int i = 0; i < deckManager.cardHandPlayerId[clientId].handPlayerList.Count; i++)
        {
            if (statPlayerNetwork.selectedCardId == deckManager.cardHandPlayerId[clientId].handPlayerList[i])
            {
                deckManager.cardHandPlayerId[clientId].handPlayerList.RemoveAt(i);
                DeleteCardInPlayerHandClientRpc(clientId, i);
                break;
            }
        }
    }

    [ClientRpc]
    void DeleteCardInPlayerHandClientRpc(int clientId, int indexRemoveCard)
    {
        Debug.Log($"Removed Card in DeckManager PlayerHand ID({clientId}) IndexDelete: {indexRemoveCard}");
    }

    [ServerRpc(RequireOwnership = false)]
    void SpawnCardatAtPlateServerRpc(ulong clientId, int selectedCard, int selectedPlate)
    {
        DeckManager deckManager = FindObjectOfType<DeckManager>();
        statPlayerNetwork = transform.GetComponent<StatPlayerNetwork>();

        while (deckManager == null)
        {
            deckManager = FindObjectOfType<DeckManager>();
            Debug.LogError("DeckManager not found!");
        }

        while (statPlayerNetwork == null)
        {
            statPlayerNetwork = transform.GetComponent<StatPlayerNetwork>();
            Debug.LogError("StatPlayerNetwork not found!");
        }
        statPlayerNetwork.workingPoints += deckManager.GetCardById(statPlayerNetwork.selectedCardId).workingPoints;

        if (deckManager.GetCardById(statPlayerNetwork.selectedCardId).cardType.ToString() == "IT")
        {
            statPlayerNetwork.itDepartmentCount += 1;
        } else if (deckManager.GetCardById(statPlayerNetwork.selectedCardId).cardType.ToString() == "Marketing")
        {
            statPlayerNetwork.marketingDepartmentCount += 1;
        } else if (deckManager.GetCardById(statPlayerNetwork.selectedCardId).cardType.ToString() == "HumanResource")
        {
            statPlayerNetwork.hrDepartmentCount += 1;
        } else if (deckManager.GetCardById(statPlayerNetwork.selectedCardId).cardType.ToString() == "Accountant")
        {
            statPlayerNetwork.accountingDepartmentCount += 1;
        }
        
        CardScriptable cardData = deckManager.GetCardById(selectedCard);
        if (cardData == null)
        {
            Debug.LogError("Can't find cardData!");
            return;
        }

        if (!NetworkManager.Singleton.ConnectedClients.TryGetValue(clientId, out var client) || client.PlayerObject == null)
        {
            Debug.LogError("Can't find PlayerObject!");
            return;
        }

        int networkObjectFieldID = deckManager.ListfieldofPlayerNetworkObjectId[(int)clientId];
        if (!NetworkManager.Singleton.SpawnManager.SpawnedObjects.TryGetValue((ulong)networkObjectFieldID, out var retrievedNetworkObject))
        {
            Debug.LogError("Failed to retrieve NetworkObject by ID");
            return;
        }

        FieldClientManager fieldClientManager = retrievedNetworkObject.GetComponent<FieldClientManager>();
        if (fieldClientManager == null)
        {
            Debug.LogError("Failed to retrieve FieldClientManager component from NetworkObject");
            return;
        }
        //Debug.LogError("selectedCard:" + selectedCard.ToString());
        // Debug.LogError("selectedPlate:" + statPlayerNetwork.selectedPlateId.ToString());
        Transform spawnCardPoint = fieldClientManager.plateCards[statPlayerNetwork.selectedPlateId].transform;
        if (spawnCardPoint == null)
        {
            Debug.LogError("Can't find SpawnPoint or Carddata (SpawnCardatAtPlateServerRpc)");
            return;
        }
        Quaternion rot = Quaternion.Euler(0, 0, -90);
        GameObject CardObjectNetwork = Instantiate(CardTemplatePrefeb, spawnCardPoint.position + new Vector3(0, 0.4f, 0), rot);
       
        CardObjectNetwork.name = cardData.name;
        NetworkObject networkObject = CardObjectNetwork.GetComponent<NetworkObject>();
        if (networkObject != null)
        {
            networkObject.SpawnWithOwnership(clientId);

            DisplayCard displayCard = CardObjectNetwork.GetComponent<DisplayCard>();
            if (displayCard != null)
            {
                displayCard.SetCardData(cardData, clientId);
                displayCard.UpdateCardClientRpc(cardData.id, cardData.cardName, cardData.workingPoints, cardData.actionPoints, cardData.cardType.ToString(), cardData.description, clientId);
            }
        }
    }
}
