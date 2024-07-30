using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class OnPickCardNetwork : NetworkBehaviour
{
    private DeckManager deckManager;
    private DisplayCard displayCard;

    void Start()
    {
        deckManager = FindObjectOfType<DeckManager>();
        displayCard = GetComponent<DisplayCard>();

        if (deckManager == null)
        {
            Debug.LogError("Can't find DeckManager");
        }
        if (displayCard == null)
        {
            Debug.LogError("Can't find DisplayCard");
        }
    }

    void Update()
    {
        if (displayCard == null)
        {
            displayCard = GetComponent<DisplayCard>();
            if (displayCard == null)
            {
                Debug.LogError("DisplayCard component not found.");
                return;
            }
        }

        if ((int)NetworkManager.Singleton.LocalClientId != displayCard.ownerId)
        {
            return;
        }

        if (deckManager == null)
        {
            deckManager = FindObjectOfType<DeckManager>();
            if (deckManager == null)
            {
                Debug.LogWarning("DeckManager not found, will retry.");
                return;
            }
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform == transform)
                {
                    Debug.Log("Pick Card!");
                    deckManager.HandlePickCardNetwork(NetworkManager.Singleton.LocalClientId, displayCard.id ,transform);
                    //DeleteCard(transform);
                    //DeleteCardClientRpc(NetworkManager.Singleton.LocalClientId, displayCard.id);
                    Destroy(transform.gameObject);
                }
            }
        }
    }
    /*
    [ClientRpc]
    public void DeleteCardClientRpc(ulong clientId, int cardId)
    {
        // Ensure only the owner of the card deletes it
        if (NetworkManager.Singleton.LocalClientId == clientId)
        {
            Destroy(gameObject);
        }
    }
    */
}