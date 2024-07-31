using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class VoteEndGameNetwork : NetworkBehaviour
{
    private EndGameVoteCondition endGameVoteCondition;
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        endGameVoteCondition = FindObjectOfType<EndGameVoteCondition>();
        if (endGameVoteCondition == null)
        {
            Debug.LogError("Can't find EndGameVoteCondition");
            return;
        }
    }
    [ServerRpc(RequireOwnership = false)]
    public void ClickYesEndGameVoteConditionServerRpc()
    {
        endGameVoteCondition.voteAccept += 1;
        endGameVoteCondition.voteTotal += 1;

        DespawnVoteEndGameObject();
        //Despawnit self
    }

    [ServerRpc(RequireOwnership = false)]
    public void ClickNoEndGameVoteConditionServerRpc()
    {
        endGameVoteCondition.voteCancel += 1;
        endGameVoteCondition.voteTotal += 1;

        DespawnVoteEndGameObject();
        //Despawnit self
    }

    private void DespawnVoteEndGameObject()
    {
        // Ensure the object is only despawned if it has a NetworkObject component
        var networkObject = GetComponent<NetworkObject>();
        if (networkObject != null)
        {
            networkObject.Despawn();
        }
        else
        {
            Debug.LogError("No NetworkObject found to despawn!");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
