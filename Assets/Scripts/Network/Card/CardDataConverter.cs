using UnityEngine;

public static class CardDataConverter
{
    public static CardData ToCardData(CardScriptable cardScriptable)
    {
        return new CardData
        {
            id = cardScriptable.id,
            cardName = cardScriptable.cardName,
            description = cardScriptable.description,
            workingPoints = cardScriptable.workingPoints,
            actionPoints = cardScriptable.actionPoints,
            cardType = cardScriptable.cardType,
            imagePath = cardScriptable.imageCard ? cardScriptable.imageCard.name : string.Empty
        };
    }

    public static CardScriptable ToCardScriptable(CardData cardData)
    {
        CardScriptable cardScriptable = ScriptableObject.CreateInstance<CardScriptable>();
        cardScriptable.id = cardData.id;
        cardScriptable.cardName = cardData.cardName.ToString();
        cardScriptable.description = cardData.description.ToString();
        cardScriptable.workingPoints = cardData.workingPoints;
        cardScriptable.actionPoints = cardData.actionPoints;
        cardScriptable.cardType = cardData.cardType;
        cardScriptable.imageCard = Resources.Load<Sprite>("Picture/Character/" + cardData.imagePath.ToString());
        return cardScriptable;
    }
}