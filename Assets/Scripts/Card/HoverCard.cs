using System.Collections;
using UnityEngine;

public class HoverCard : MonoBehaviour
{
    /*
    private StatPlayer statPlayer; // Reference to the StatPlayer script
    public GameObject cardPrefab;

    private Vector3 initialPosition; // Store the initial position as a Vector3
    private bool isUsed = false;
    private bool isAnimating = false;
    public bool isClick = false;
    public float hoverDuration = 0.1f; // Duration for smooth transition
    public float hoverOffset = 0.1f; // Offset for hover effect

    private GameObject pickedCard;
    private Transform pickedArea;
    void Start()
    {
        // Initialize the initial position
        initialPosition = transform.localPosition;

        // Find the StatPlayer script in the scene (assuming there's only one)
        statPlayer = FindObjectOfType<StatPlayer>();
        // Alternatively, assign statPlayer in the Unity Editor
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && statPlayer.GetPickedCard() != null && statPlayer.GetTransform() != null)
        {
            pickedCard = statPlayer.GetPickedCard();
            pickedArea = statPlayer.GetTransform();

            statPlayer.MinusActionPoint();

            GameObject newCard = Instantiate(cardPrefab, pickedArea.position + new Vector3(0,0.3f,0), Quaternion.Euler(0, 0, -90));
            newCard.transform.parent = pickedArea.transform;
            newCard.name =pickedCard.name;

            // Optionally, set the properties of the new card
            DisplayCard displayCard = newCard.GetComponent<DisplayCard>();
            DisplayCard oldDisplayCard = pickedCard.GetComponent<DisplayCard>();
            if (displayCard != null && oldDisplayCard != null)
            {
                displayCard.SetCardData(new Card
                {
                    id = oldDisplayCard.id,
                    cardName = oldDisplayCard.cardName,
                    workingpoint = oldDisplayCard.workingpoint,
                    actionpoint = oldDisplayCard.actionpoint,
                    type = oldDisplayCard.type,
                    description = oldDisplayCard.description,
                    thisImage = oldDisplayCard.thisImage
                });
            }
            if (oldDisplayCard.type.ToString() == "IT")
            {
                statPlayer.itDepartmentCount += 1;
            } else if (oldDisplayCard.type.ToString() == "Marketing")
            {
                statPlayer.marketingDepartmentCount += 1;
            }
            else if (oldDisplayCard.type.ToString() == "Human_Resource")
            {
                statPlayer.hrDepartmentCount += 1;
            }
            else if (oldDisplayCard.type.ToString() == "Accountant")
            {
                statPlayer.accountingDepartmentCount += 1;
            }

            statPlayer.DeletePickedCard();
            pickedArea = null;
            pickedCard = null;
            
        }

        // Check if the card is a child of holdPosition
        if (transform.parent != null && transform.parent.name == "holdPosition" && !isUsed)
        {
            // Store the initial position of the card
            initialPosition = transform.localPosition;
            isUsed = true;
        }

        // Check for mouse click and raycast hit
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform == transform && hit.transform.gameObject.CompareTag("Card"))
                {
                    // Update the PickedCard parameter in the StatPlayer script
                    if (statPlayer != null)
                    {
                        statPlayer.SetPickedCard(hit.transform.gameObject);
                    }
                    else
                    {
                        Debug.LogWarning("StatPlayer not found!");
                    }
                }
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.CompareTag("Area"))
                {
                    if (statPlayer != null)
                    {
                        statPlayer.SetPickedArea(hit.transform);
                    }
                    else
                    {
                        Debug.LogWarning("Area not found!");
                    }
                }
            }
        }
    }

    void OnMouseDown()
    {
        if (isUsed && !isAnimating)
        {
            StartCoroutine(SmoothHover(initialPosition + new Vector3(0, 0.3f, 0)));
            isClick = !isClick;
        }
    }

    void OnMouseEnter()
    {
        if (isUsed && !isAnimating && !isClick)
        {
            StartCoroutine(SmoothHover(initialPosition + new Vector3(0, hoverOffset, 0)));
        }
    }

    void OnMouseExit()
    {
        if (isUsed && !isAnimating && !isClick)
        {
            StartCoroutine(SmoothHover(initialPosition));
        }
    }

    IEnumerator SmoothHover(Vector3 targetPosition)
    {
        isAnimating = true;
        Vector3 startPosition = transform.localPosition;
        float elapsedTime = 0;

        while (elapsedTime < hoverDuration)
        {
            transform.localPosition = Vector3.Lerp(startPosition, targetPosition, elapsedTime / hoverDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = targetPosition;
        isAnimating = false;
    }
    */
}
