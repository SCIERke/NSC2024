using Unity.Collections;
using UnityEngine;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Netcode;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class CardScriptable : ScriptableObject
{
    public int id;

    public string cardName;
    public string description;
    
    public int workingPoints;
    public int actionPoints;

    public CardType cardType;
    
    public Sprite imageCard;

    public void Print()
    {
        Debug.Log(cardName);
    }
}

public enum CardType
{
    IT,
    Marketing,
    HumanResource,
    Accountant,
    None
}
