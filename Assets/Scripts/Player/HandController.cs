/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    public GameObject cardPrefab;
    public int numberOfCards = 0;
    public float cardSpacing = 1f; // Spacing between cards
    public float cardCurve = 10f; // Curve angle for hand arrangement

    public Transform holdPos;

    private List<GameObject> cardsInHand = new List<GameObject>();

    

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit))
            {
                if (hit.transform.gameObject.tag == "Card" && hit.transform.parent != holdPos.transform)
                {
                    numberOfCards++;
                    GameObject pickedCard = hit.transform.gameObject;
                    Rigidbody pickedCardRb = pickedCard.GetComponent<Rigidbody>();
                    DisplayCard displayCard = pickedCard.GetComponent<DisplayCard>();
                    if (pickedCardRb)
                    {
                        cardsInHand.Add(pickedCard);
                        pickedCardRb.isKinematic = true;
                        pickedCardRb.transform.parent = holdPos.transform;
                        ArrangeCards();
                    }
                }
            }
        }
    }

    public void ArrangeCards()
    {
        float angleStep = cardCurve / (numberOfCards - 1);
        float startAngle = -cardCurve / 2;

        for (int i = 0; i < cardsInHand.Count; i++)
        {
            float angle = startAngle + i * angleStep;
            Vector3 cardPosition = new Vector3(i * 0.5f,0,0);

            cardsInHand[i].transform.localPosition = cardPosition;
            //cardsInHand[i].transform.localRotation = cardRotation;
        }
    }

    public void NumberofCardsMinus()
    {
        numberOfCards -= 1;
    }
}
*/

using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    public GameObject cardPrefab;
    public int numberOfCards = 0;
    public float cardSpacing = 1f; // Spacing between cards
    public float cardCurve = 10f; // Curve angle for hand arrangement

    public Transform holdPos;

    private List<GameObject> cardsInHand = new List<GameObject>();

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit))
            {
                if (hit.transform.gameObject.tag == "Card" && hit.transform.parent != holdPos.transform)
                {
                    if (hit.transform.parent != null)
                    {
                        if (hit.transform.parent.name != null && hit.transform.parent.name.ToString().Contains("Plate"))
                        {
                            return;
                        }
                    }

                    numberOfCards++;
                    GameObject pickedCard = hit.transform.gameObject;
                    Rigidbody pickedCardRb = pickedCard.GetComponent<Rigidbody>();
                    DisplayCard displayCard = pickedCard.GetComponent<DisplayCard>();
                    if (pickedCardRb)
                    {
                        cardsInHand.Add(pickedCard);
                        pickedCardRb.isKinematic = true;
                        pickedCardRb.transform.parent = holdPos.transform;
                        ArrangeCards();
                    }
                }
            }
        }
    }

    public void DeleteofPickedCard(string name)
    {
        for (int i = 0; i < cardsInHand.Count; i++)
        {
            if (cardsInHand[i].name == name)
            {
                cardsInHand.RemoveAt(i);
                break;
            }
        }
    }

    public void ArrangeCards()
    {
        float angleStep = cardCurve / (numberOfCards - 1);
        float startAngle = -cardCurve / 2;

        for (int i = 0; i < cardsInHand.Count; i++)
        {
            float angle = startAngle + i * angleStep;
            Vector3 cardPosition = new Vector3(i * 0.5f, 0, 0);

            cardsInHand[i].transform.localPosition = cardPosition;
        }
    }

    public void NumberofCardsMinus()
    {
        numberOfCards -= 1;
    }
}
