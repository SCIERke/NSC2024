using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class OnClickCardNetwork : NetworkBehaviour
{
    // Start is called before the first frame update
    private DeckManager deckManager;
    private StatPlayerNetwork statPlayerNetwork;
    private DisplayCard displayCard;

    void Start()
    {
        deckManager = FindObjectOfType<DeckManager>();
        displayCard = transform.GetComponent<DisplayCard>();
        if (deckManager == null )
        {
            Debug.LogError("Can't find DeckManager , Retrying...");
            deckManager = FindObjectOfType<DeckManager>();
            if ( deckManager == null )
            {
                Debug.LogError("Can't find DeckManager!!");
                return;
            }
        }

        if (displayCard == null)
        {
            Debug.LogError("Can't find Displaycard , Retrying...");
            deckManager = FindObjectOfType<DeckManager>();
            if (deckManager == null)
            {
                Debug.LogError("Can't find DisplayCard!!");
                return;
            }
        }
    }

    void Update()
    {
        if (displayCard == null)
        {
            Debug.LogError("Can't find Displaycard , Retrying...");
            deckManager = FindObjectOfType<DeckManager>();
            if (deckManager == null)
            {
                Debug.LogError("Can't find DisplayCard!!");
                return;
            }
        }

        if (NetworkManager.Singleton.LocalClientId != (ulong)displayCard.ownerId)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform == transform)
                {
                    deckManager.HandleSelectedCardNetwork(NetworkManager.Singleton.LocalClientId, displayCard.id);
                }
            }
        }
    }
}
