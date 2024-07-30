using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDeck : MonoBehaviour
{
    /*
    public List<Card> deck = new List<Card>();
    public int deckSize;
    public int x;

    public Transform parentObject; // The parent object to which the new card will be assigned

    public GameObject cardPrefab; // Reference to the card prefab

    private StatPlayer statPlayer;

    void Start()
    {
        InitializeDeck();
        statPlayer = FindObjectOfType<StatPlayer>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && statPlayer.GetActionPoint() > 0 )
        {
            DrawCard();
        }
    }

    void InitializeDeck()
    {
        for (int i = 0; i < deckSize; i++)
        {
            x = Random.Range(0, CardDatabase.Cards.Count);
            deck.Add(CardDatabase.Cards[x]);
        }
    }

    void DrawCard()
    {
        if (deck.Count > 0)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform == transform)
                {
                    statPlayer.MinusActionPoint();
                    Vector3 spawnPosition = transform.position + new Vector3(0, 3, 0);
                    Card pickedCard = deck[0];
                    GameObject newCard = Instantiate(cardPrefab, spawnPosition, Quaternion.Euler(0, -90, 0));

                    newCard.name = pickedCard.cardName;
                    newCard.transform.SetParent(parentObject, true);

                    DisplayCard displayCard = newCard.GetComponent<DisplayCard>();
                    if (displayCard != null)
                    {
                        displayCard.SetCardData(pickedCard);
                    }
                    deck.RemoveAt(0);
                }
            }
        }
        else
        {
            Debug.LogWarning("Deck is empty!");
        }
    }
    */
}
