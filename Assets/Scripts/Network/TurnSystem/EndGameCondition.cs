using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.EventSystems;

public class EndGameCondition : NetworkBehaviour
{
    private EndGameVoteCondition endGameVoteCondition;
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        endGameVoteCondition = FindObjectOfType<EndGameVoteCondition>();
        if (endGameVoteCondition == null )
        {
            Debug.LogError("Something went wrong ,Can't find EndGameVoteCondition!");
        }
    }

    public void OnclickYesEndGameCondition()
    {
        if (endGameVoteCondition != null)
        {
            endGameVoteCondition.OnVoteBegan();
        }
    }

    
    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
