using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDatabaseNetwork : MonoBehaviour
{
    public static List<CardScriptable> Cards = new List<CardScriptable>();
    public static Dictionary<int, CardScriptable> CardDictionary = new Dictionary<int, CardScriptable>();

    [SerializeField] private CardScriptable[] cardAssets;
    void Awake()
    {
        Cards = new List<CardScriptable>(cardAssets);
        foreach (var card in cardAssets)
        {
            if (!CardDictionary.ContainsKey(card.id))
            {
                CardDictionary.Add(card.id, card);
            }
        }
    }

    public static CardScriptable GetCardById(int id)
    {
        CardDictionary.TryGetValue(id, out var card);
        return card;
    }
}
