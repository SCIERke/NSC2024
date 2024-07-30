/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DisplayCard : MonoBehaviour
{
    public int id;
    public string cardName;
    public int workingpoint;
    public int actionpoint;
    public string type;
    public string description;
    public Sprite thisImage;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI workingpointText;
    public Image frameImage;
    public Image characterImage;

    public GameObject CardDescription;

    private Transform currentPos;

    void Start()
    {
        currentPos = transform;
        CardDescription.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit) && hit.transform == currentPos)
            {
                bool isActive = CardDescription.activeSelf;
                /*
                if (transform.parent.ToString() == "holdPostion")
                {

                } else
                {

                }
                CardDescription.SetActive(!isActive);
            }
        }
    }

    public void SetCardData(Card card)
    {
        id = card.id;
        cardName = card.cardName;
        workingpoint = card.workingpoint;
        actionpoint = card.actionpoint;
        type = card.type;
        description = card.description;
        thisImage = card.thisImage;

        UpdateCardInfo();
    }
    
    public void SetCardData(CardScriptable card)
    {
        id = card.id;
        cardName = card.cardName;
        workingpoint = card.workingPoints;
        actionpoint = card.actionPoints;
        type = card.cardType.ToString();
        description = card.description;
        thisImage = card.imageCard;

        UpdateCardInfo();
    }

    void UpdateCardInfo()
    {
        Debug.Log("Updated");
        nameText.text = cardName;
        descriptionText.text = description;
        workingpointText.text = workingpoint.ToString();
        characterImage.sprite = thisImage;

        Sprite frameSprite = Resources.Load<Sprite>("Picture/Frame/" + type + "_Frame");
        if (frameSprite != null)
        {
            frameImage.sprite = frameSprite;
        }
        else
        {
            Debug.LogError("Frame sprite not found for type: " + type);
        }
    }
}
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.Netcode;

public class DisplayCard : NetworkBehaviour
{
    public int id;
    public int ownerId;
    public string cardName;
    public int workingpoint;
    public int actionpoint;
    public string type;
    public string description;
    public Sprite thisImage;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI workingpointText;
    public Image frameImage;
    public Image characterImage;

    public GameObject CardDescription;

    private Transform currentPos;

    void Start()
    {
        currentPos = transform;
        CardDescription.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit) && hit.transform == currentPos)
            {
                bool isActive = CardDescription.activeSelf;
                CardDescription.SetActive(!isActive);
            }
        }
    }

    public void SetCardData(CardScriptable card, ulong clientId)
    {
        id = card.id;
        cardName = card.cardName;
        workingpoint = card.workingPoints;
        actionpoint = card.actionPoints;
        type = card.cardType.ToString();
        description = card.description;
        thisImage = card.imageCard;
        ownerId = (int)clientId;

        UpdateCardInfo();
    }

    void UpdateCardInfo()
    {
        Debug.Log("Updated");
        nameText.text = cardName;
        descriptionText.text = description;
        workingpointText.text = workingpoint.ToString();
        characterImage.sprite = thisImage;

        Sprite frameSprite = Resources.Load<Sprite>("Picture/Frame/" + type + "_Frame");
        if (frameSprite != null)
        {
            frameImage.sprite = frameSprite;
        }
        else
        {
            Debug.LogError("Frame sprite not found for type: " + type);
        }
    }

    // Method to update card data using a ClientRpc
    [ClientRpc]
    public void UpdateCardClientRpc(int id, string cardName, int workingpoint, int actionpoint, string type, string description, ulong clientId)
    {
        this.id = id;
        this.cardName = cardName;
        this.workingpoint = workingpoint;
        this.actionpoint = actionpoint;
        this.type = type;
        this.description = description;
        this.ownerId = (int)clientId;

        if (cardName == "Employee" || cardName == "Hacker" || cardName == "Human_resources_executive" || cardName == "Marketing_consultant" || cardName == "Publicist" || cardName == "Software_Engineer" || cardName == "Workaholic")
        {
            this.thisImage = Resources.Load<Sprite>("Picture/Character/" + cardName);
        } else
        {
            thisImage = Resources.Load<Sprite>("Picture/Character/" + "Employee");
        }
        

        UpdateCardInfo();
    }
}
