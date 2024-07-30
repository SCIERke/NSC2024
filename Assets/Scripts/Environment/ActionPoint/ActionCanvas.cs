using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionCanvas : MonoBehaviour
{
    public Image AP_1;
    public Image AP_2;
    public Image AP_3;

    private StatPlayer statPlayer;

    void Start()
    {
        // Find and cache the StatPlayer instance
        statPlayer = FindObjectOfType<StatPlayer>();
        UpdateActionPoints();
    }

    void Update()
    {
        // Update action points display every frame
        UpdateActionPoints();
    }

    private void UpdateActionPoints()
    {
        if (statPlayer == null) return;

        int actionPoints = statPlayer.actionPoints;

        // Update the visibility of the images based on action points
        AP_1.enabled = actionPoints > 0;
        AP_2.enabled = actionPoints > 1;
        AP_3.enabled = actionPoints > 2;
    }
}
