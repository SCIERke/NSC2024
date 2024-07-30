using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Place_CarScrpits : MonoBehaviour
{
    StatPlayer statPlayer;
    HandController handController;

    public Transform positionPlace;
    private GameObject pickedCard;

    private Vector3 originalScale;

    void Start()
    {
        // Find the StatPlayer in the scene
        statPlayer = FindObjectOfType<StatPlayer>();
        if (statPlayer == null)
        {
            Debug.LogError("StatPlayer not found in the scene!");
        }

        handController = FindObjectOfType<HandController>();

        if (handController == null)
        {
            Debug.LogError("HandController not found in the scene!");
        }
    }

    void Update()
    {

    }
}
