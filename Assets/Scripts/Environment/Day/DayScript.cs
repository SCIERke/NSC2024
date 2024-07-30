using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class DayScript : MonoBehaviour
{
    private StatEnvironment statEnvironment;

    public TextMeshProUGUI DayText;
    private int day;

    void Start()
    {
        statEnvironment = FindObjectOfType<StatEnvironment>();
        day = statEnvironment.day;
        DayText.text = day.ToString();
    }

    void Update()
    {
        day = statEnvironment.day;
        DayText.text = day.ToString();
    }
}
