using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatCard : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject cardPrefab; // Corrected the spelling of 'cardPrefab'
    private Transform currentPos;
    public Transform parentObject;

    void Start()
    {
        currentPos = transform; // Initialize currentPos with the current object's transform
    }

    void Update()
    {
        // Check for user input to create a card (e.g., left mouse click)
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                // Check if the GameObject hit by the raycast is this GameObject
                if (hit.transform == currentPos)
                {
                    Vector3 spawnPosition = currentPos.position + new Vector3(0, 2, 0);
                    CreateNewCard(spawnPosition);
                }
            }
        }
    }

    void CreateNewCard(Vector3 spawnPosition)
    {
        // Instantiate the card at the hit position
        GameObject newCard = Instantiate(cardPrefab, spawnPosition, Quaternion.identity);
        newCard.transform.SetParent(parentObject, true); // true keeps the world position
        //newCard.name = cardBaseName + "_" + cardCounter;
    }
}
