using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WorkingPoint : MonoBehaviour
{
    public TextMeshProUGUI Score;
    private StatPlayer statPlayer;

    private int WP;

    void Start()
    {
        statPlayer = FindObjectOfType<StatPlayer>();
        WP = statPlayer.workingPoints;
        Score.text = WP.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        WP = statPlayer.workingPoints;
        Score.text = WP.ToString();
    }
}
