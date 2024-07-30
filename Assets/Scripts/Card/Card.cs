using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class Card
{
    public int id;
    public string cardName;
    public int workingpoint;
    public int actionpoint;
    public string type;
    public string description;
    public Sprite thisImage;

    public Card()
    {

    }

    public Card(int Id,string CardName ,int WorkingPoint, int ActionPoint , string Type ,string Description ,Sprite ThisImage)
    {
        id = Id;
        cardName = CardName;
        workingpoint = WorkingPoint;
        actionpoint = ActionPoint;
        type = Type;
        description = Description;
        thisImage = ThisImage;
    }



}
