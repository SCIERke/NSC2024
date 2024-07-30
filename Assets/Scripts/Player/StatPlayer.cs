/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatPlayer : MonoBehaviour
{
    // General Stats
    public int actionPoints; // Points available for actions
    public int workingPoints; // Points available for work
    public int projectCount; // Number of projects
    public int reportCount; // Number of reports

    // Picked Card
    public GameObject PickedCard;
    public Transform PickedArea;

    // Department Counts
    public int itDepartmentCount; // Number of IT department members
    public int hrDepartmentCount; // Number of HR department members
    public int marketingDepartmentCount; // Number of Marketing department members
    public int accountingDepartmentCount; // Number of Accounting department members

    void Update()
    {
        
    }

    public void SetPickedCard(GameObject card)
    {
        PickedCard = card;
        Debug.Log("Picked Card set to: " + card.name);
    }

    public void SetPickedArea(Transform Area)
    {
        PickedArea = Area;
        Debug.Log("Picked Area set to :" + Area.name);
    }

    public Transform GetTransform()
    {
        return PickedArea;
    }

    public GameObject GetPickedCard()
    {
        return PickedCard;
    }

    public void DeletePickedCard()
    {
        if (PickedCard != null)
        {
            //PickedCard = null;
            Destroy(PickedCard);
            PickedArea = null;
        }
    }

    public DisplayCard GetPickedDisplaycard()
    {
        return PickedCard.GetComponent<DisplayCard>();
    }
}
*/

using System.Collections.Generic;
using UnityEngine;

public class StatPlayer : MonoBehaviour
{
    // General Stats
    public int actionPoints; // Points available for actions
    public int workingPoints; // Points available for work
    public int projectCount; // Number of projects
    public int reportCount; // Number of reports

    // Picked Card
    private HandController handController;
    public GameObject PickedCard;
    public Transform PickedArea;
    public int IndexPickedCard;

    // Department Counts
    public int itDepartmentCount; // Number of IT department members
    public int hrDepartmentCount; // Number of HR department members
    public int marketingDepartmentCount; // Number of Marketing department members
    public int accountingDepartmentCount; // Number of Accounting department members

    // Project Stat
    public List<Project> projectListHave;
    public GameObject selectedProject;

    // WorkingPoints Deal
    public Transform Pos1;
    public Transform Pos2;
    public Transform Pos3;
    public Transform Pos4;
    public Transform Pos5;
    public Transform Pos6;
    public Transform Pos7;
    public Transform Pos8;

    private CountAll countAll1;
    private CountAll countAll2;
    private CountAll countAll3;
    private CountAll countAll4;
    private CountAll countAll5;
    private CountAll countAll6;
    private CountAll countAll7;
    private CountAll countAll8;

    void Start()
    {
        // WorkingPoints Deal
        handController = FindObjectOfType<HandController>();
        
        if (Pos1 != null) countAll1 = Pos1.GetComponent<CountAll>();
        if (Pos2 != null) countAll2 = Pos2.GetComponent<CountAll>();
        if (Pos3 != null) countAll3 = Pos3.GetComponent<CountAll>();
        if (Pos4 != null) countAll4 = Pos4.GetComponent<CountAll>();
        if (Pos5 != null) countAll5 = Pos5.GetComponent<CountAll>();
        if (Pos6 != null) countAll6 = Pos6.GetComponent<CountAll>();
        if (Pos7 != null) countAll7 = Pos7.GetComponent<CountAll>();
        if (Pos8 != null) countAll8 = Pos8.GetComponent<CountAll>();
    }

    void Update()
    {
        // WorkingPoints Deal

        workingPoints = 0;
        if (countAll1 != null) workingPoints += countAll1.OwnWorkingPoint;
        if (countAll2 != null) workingPoints += countAll2.OwnWorkingPoint;
        if (countAll3 != null) workingPoints += countAll3.OwnWorkingPoint;
        if (countAll4 != null) workingPoints += countAll4.OwnWorkingPoint;
        if (countAll5 != null) workingPoints += countAll5.OwnWorkingPoint;
        if (countAll6 != null) workingPoints += countAll6.OwnWorkingPoint;
        if (countAll7 != null) workingPoints += countAll7.OwnWorkingPoint;
        if (countAll8 != null) workingPoints += countAll8.OwnWorkingPoint;

        //Debug.Log("Overall Workpoint: " + workingPoints);
    }

    public void SetPickedCard(GameObject card)
    {
        PickedCard = card;
        Debug.Log("Picked Card set to: " + card.name);
    }

    public void SetPickedArea(Transform area)
    {
        PickedArea = area;
        Debug.Log("Picked Area set to: " + area.name);
    }

    public Transform GetTransform()
    {
        return PickedArea;
    }

    public GameObject GetPickedCard()
    {
        return PickedCard;
    }

    public void DeletePickedCard()
    {
        if (PickedCard != null)
        {
            if (handController != null)
            {
                handController.DeleteofPickedCard(PickedCard.name.ToString());
            }
            Destroy(PickedCard);
            PickedCard = null;
            PickedArea = null;
        }
    }
    public DisplayCard GetPickedDisplaycard()
    {
        return PickedCard.GetComponent<DisplayCard>();
    }

    public int GetActionPoint()
    {
        return actionPoints;
    }
    
    public void MinusActionPoint()
    {
        actionPoints -= 1;
    }
}
