using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using TMPro;
using Newtonsoft.Json.Bson;


public class WorkingPointsNetwork : NetworkBehaviour
{
    [SerializeField] private TextMeshProUGUI workingPointsText;

    private StatPlayerNetwork statPlayerNetwork;

    public override void OnNetworkSpawn()
    {
        if (!IsOwner) return;
        base.OnNetworkSpawn();

        statPlayerNetwork = transform.GetComponent<StatPlayerNetwork>();
    }

    void Update()
    {
        if (!IsOwner) return;
        UpdateWorkingPoints();
    }

    private void UpdateWorkingPoints()
    {
        statPlayerNetwork = transform.GetComponent<StatPlayerNetwork>();
        if (statPlayerNetwork == null) return;
        
        int workingPoints = statPlayerNetwork.workingPoints;

        workingPointsText.text = workingPoints.ToString();
    }
}
