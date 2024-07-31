using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UIElements;

public class StatPlayerNetwork : NetworkBehaviour
{
    public int actionPoints; // Points available for actions
    public int workingPoints; // Points available for work
    //[SerializeField] private int projectCount; // Number of projects
    //[SerializeField] private int reportCount; // Number of reports

    public int selectedCardId;
    public int selectedPlateId;
    public int selectedProject;
    // Picked Card
    /*
    private HandController handController;
    public GameObject PickedCard;
    public Transform PickedArea;
    public int IndexPickedCard;
    */

    public string sideofPlayer;

    // Department Counts
    public int itDepartmentCount; // Number of IT department members
    public int hrDepartmentCount; // Number of HR department members
    public int marketingDepartmentCount; // Number of Marketing department members
    public int accountingDepartmentCount; // Number of Accounting department members

    // Project Stat
    public int projectPlayerCount;

    /*
    public List<Project> projectListHave;
    public GameObject selectedProject;
    */

    // WorkingPoints Deal
    /*
    [SerializeField] private Transform Pos1;
    [SerializeField] private Transform Pos2;
    [SerializeField] private Transform Pos3;
    [SerializeField] private Transform Pos4;
    [SerializeField] private Transform Pos5;
    [SerializeField] private Transform Pos6;
    [SerializeField] private Transform Pos7;
    [SerializeField] private Transform Pos8;
    */

    /*
    private CountAll countAll1;
    private CountAll countAll2;
    private CountAll countAll3;
    private CountAll countAll4;
    private CountAll countAll5;
    private CountAll countAll6;
    private CountAll countAll7;
    private CountAll countAll8;
    */
    override public void OnNetworkSpawn()
    {
        if (!IsOwner) return;

        OnClientConnected(NetworkManager.Singleton.LocalClientId);


        // WorkingPoints Deal
        //handController = FindObjectOfType<HandController>();


        // this code for Count action points each position
        /* 
        if (Pos1 != null) countAll1 = Pos1.GetComponent<CountAll>();
        if (Pos2 != null) countAll2 = Pos2.GetComponent<CountAll>();
        if (Pos3 != null) countAll3 = Pos3.GetComponent<CountAll>();
        if (Pos4 != null) countAll4 = Pos4.GetComponent<CountAll>();
        if (Pos5 != null) countAll5 = Pos5.GetComponent<CountAll>();
        if (Pos6 != null) countAll6 = Pos6.GetComponent<CountAll>();
        if (Pos7 != null) countAll7 = Pos7.GetComponent<CountAll>();
        if (Pos8 != null) countAll8 = Pos8.GetComponent<CountAll>();
        */
    }

    void OnClientConnected(ulong clientId)
    {
        SetActionPointsOnstartRoundServerRpc(clientId);
        SetWorkingPointsOnstartServerRpc(clientId);
    }

    [ServerRpc]
    public void SetWorkingPointsOnstartServerRpc(ulong clientId)
    {
        var playerObject = NetworkManager.Singleton.ConnectedClients[clientId].PlayerObject;
        StatPlayerNetwork statPlayerNetwork = playerObject.GetComponent<StatPlayerNetwork>();
        if (statPlayerNetwork != null)
        {
            statPlayerNetwork.workingPoints = 0;
            SetWorkingPointsOnstartClientRpc(clientId);
            Debug.Log($"WorkingPoints have been set {clientId}");
        }
        else
        {
            Debug.LogError($"Cant find clientId:{clientId}");
        }
    }

    [ClientRpc]
    public void SetWorkingPointsOnstartClientRpc(ulong clientId)
    {
        var playerObject = NetworkManager.Singleton.SpawnManager.GetLocalPlayerObject();
        StatPlayerNetwork statPlayerNetwork = playerObject.GetComponent<StatPlayerNetwork>();
        if (statPlayerNetwork != null)
        {
            statPlayerNetwork.workingPoints = 0;
        }
        else
        {
            Debug.LogError($"Cant find clientId:{clientId}");
        }
    }


    // This function sets ActionPoint at on start round (ServerRpc)
    [ServerRpc]
    public void SetActionPointsOnstartRoundServerRpc(ulong clientId)
    {
        var playerObject = NetworkManager.Singleton.ConnectedClients[clientId].PlayerObject;
        StatPlayerNetwork statPlayerNetwork = playerObject.GetComponent<StatPlayerNetwork>();
        if (statPlayerNetwork != null)
        {
            statPlayerNetwork.actionPoints = 3;
            SetActionPointsClientRpcOnstartRoundClientRpc(clientId);
            Debug.Log($"ActionPoint have been set {clientId}");
        } else
        {
            Debug.LogError($"Cant find clientId:{clientId}");
        }
    }
    // This function sets ActionPoint at on start round (ClientRpc)
    [ClientRpc]
    void SetActionPointsClientRpcOnstartRoundClientRpc(ulong clientId)
    {
        if (NetworkManager.Singleton.LocalClientId == clientId)
        {
            var playerObject = NetworkManager.Singleton.SpawnManager.GetLocalPlayerObject();
            StatPlayerNetwork statPlayerNetwork = playerObject.GetComponent<StatPlayerNetwork>();
            if (statPlayerNetwork != null)
            {
                statPlayerNetwork.actionPoints = 3;
            } else
            {
                Debug.LogError($"Cant find clientId:{clientId}");
            }
        }
    }

    void Update()
    {
        // WorkingPoints Deal

        
        //code below will sum all count position actionpoints 
        /*workingPoints = 0;
        if (countAll1 != null) workingPoints += countAll1.OwnWorkingPoint;
        if (countAll2 != null) workingPoints += countAll2.OwnWorkingPoint;
        if (countAll3 != null) workingPoints += countAll3.OwnWorkingPoint;
        if (countAll4 != null) workingPoints += countAll4.OwnWorkingPoint;
        if (countAll5 != null) workingPoints += countAll5.OwnWorkingPoint;
        if (countAll6 != null) workingPoints += countAll6.OwnWorkingPoint;
        if (countAll7 != null) workingPoints += countAll7.OwnWorkingPoint;
        if (countAll8 != null) workingPoints += countAll8.OwnWorkingPoint;

        //Debug.Log("Overall Workpoint: " + workingPoints);
        */
    }


    //This code will be setted Picked Card
    /*public void SetPickedCard(GameObject card)
    {
        PickedCard = card;
        Debug.Log("Picked Card set to: " + card.name);
    }*/

    //This code will be setted Picked Area
    /*public void SetPickedArea(Transform area)
    {
        PickedArea = area;
        Debug.Log("Picked Area set to: " + area.name);
    }*/


    //Get Picked Transform
    /*public Transform GetTransform()
    {
        return PickedArea;
    }*/

    //Get Picked Card
    /*public GameObject GetPickedCard()
    {
        return PickedCard;
    }*/

    /*public void DeletePickedCard()
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
    }*/

    /*
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
    */
}
