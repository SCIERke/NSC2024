using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Rendering;

public class PopUpProjectConditionInteractNetwork : NetworkBehaviour
{
    private StatPlayerNetwork statPlayerNetwork;
    private NetworkObject networkObject;
    private ProjectManager projectManager;

    [ServerRpc(RequireOwnership = false)]
    public void ClickYesPopUpProjectCondtionServerRpc()
    {
        networkObject = GetComponent<NetworkObject>();
        if (networkObject == null)
        {
            Debug.LogError("Can't find networkObject (ProjectConditionServerRpc)");
            return;
        }
        projectManager = FindObjectOfType<ProjectManager>();
        if (projectManager == null) {
            Debug.LogError("Can't find ProjectManager (ProjectConditionServerRpc)");
            return;
        }
        var playerObject = NetworkManager.Singleton.ConnectedClients[networkObject.OwnerClientId].PlayerObject;
        if (playerObject == null)
        {
            Debug.LogError("Can't find PlayerObject (ProjectConditionServerRpc)!");
            return;
        }

        statPlayerNetwork = playerObject.GetComponent<StatPlayerNetwork>();
        if (statPlayerNetwork == null)
        {
            Debug.LogError("Can't find statPlayerNetwork (ProjectConditionServerRpc)!");
            return;
        }
        int IDProjectSelectedInidProjectList = projectManager.idProjectDeckList[statPlayerNetwork.selectedProject];

        ProjectScriptable projectScriptable = projectManager.GetProjectById(IDProjectSelectedInidProjectList);
        if (statPlayerNetwork.itDepartmentCount >= projectScriptable.reqIT
            && statPlayerNetwork.hrDepartmentCount >= projectScriptable.reqHumanResource
            && statPlayerNetwork.marketingDepartmentCount >= projectScriptable.reqMarketing
            && statPlayerNetwork.accountingDepartmentCount >= projectScriptable.reqAccountant
            && statPlayerNetwork.workingPoints >= projectScriptable.reqWorkingPoint)
        {
            statPlayerNetwork.projectPlayerCount += 1;
            
            projectManager.idProjectDeckList.RemoveAt(statPlayerNetwork.selectedProject);
            DespawnPopupProjectConditionServerRpc(networkObject.NetworkObjectId);
            SuccessClickYesPopUpProjectConditionClientRpc(networkObject.OwnerClientId);
        } else
        {
            DespawnPopupProjectConditionServerRpc(networkObject.NetworkObjectId);
            FailClickYesPopUpProjectConditionClientRpc(networkObject.OwnerClientId);
        }
    }

    public void ClickNoPopUpProjectCondtion()
    {
        networkObject = GetComponent<NetworkObject>();
        if (networkObject == null)
        {
            Debug.LogError("Can't find networkObject (ProjectConditionServerRpc)");
            return;
        }
        DespawnPopupProjectConditionServerRpc(networkObject.NetworkObjectId);
    }

    [ClientRpc]
    void SuccessClickYesPopUpProjectConditionClientRpc(ulong clientId)
    {
        Debug.Log($"Player ({clientId}) success DoProject!");
    }

    [ClientRpc]
    void FailClickYesPopUpProjectConditionClientRpc(ulong clientId)
    {
        Debug.Log($"Player ({clientId}) failed to DoProject!");
    }

    [ServerRpc(RequireOwnership = false)]
    private void DespawnPopupProjectConditionServerRpc(ulong networkObjectId)
    {
        if (NetworkManager.Singleton.SpawnManager.SpawnedObjects.TryGetValue(networkObjectId, out var networkObject))
        {
            networkObject.Despawn();
        }
        else
        {
            Debug.LogError($"NetworkObject not found for ID: {networkObjectId}");
        }
    }

}
