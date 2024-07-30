using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System.Globalization;
using UnityEngine.UI;

public class ActionPointNetwork : NetworkBehaviour
{
    [SerializeField] private Image AP_1;
    [SerializeField] private Image AP_2;
    [SerializeField] private Image AP_3;

    private StatPlayerNetwork statPlayerNetwork;

    override public void OnNetworkSpawn()
    {
        if (!IsOwner) return;
        base.OnNetworkSpawn();


        statPlayerNetwork = transform.GetComponent<StatPlayerNetwork>();
        UpdateActionPoints();
    }

    void Update()
    {
        if (!IsOwner) return;
        UpdateActionPoints();
    }

    private void UpdateActionPoints()
    {
        if (statPlayerNetwork == null) return;

        int actionPoints = statPlayerNetwork.actionPoints;

        // Update the visibility of the images based on action points
        AP_1.enabled = actionPoints > 0;
        AP_2.enabled = actionPoints > 1;
        AP_3.enabled = actionPoints > 2;
    }
}
