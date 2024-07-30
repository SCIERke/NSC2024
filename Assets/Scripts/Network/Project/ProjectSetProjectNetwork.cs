using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Networking.Transport;
using UnityEngine;

/*
public class ProjectSetProjectNetwork : NetworkBehaviour
{
    [SerializeField] private Canvas Project1Canvas;
    [SerializeField] private Canvas Project2Canvas;
    [SerializeField] private Canvas Project3Canvas;

    private ProjectManager projectManager;

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        projectManager = FindObjectOfType<ProjectManager>();
        if (projectManager == null)
        {
            Debug.LogError("Can't find ProjectManager! ,Retrying...");
            projectManager = FindObjectOfType<ProjectManager>();
            if (projectManager == null)
            {
                Debug.LogError("Fail to find ProjectManager!");
            }
        }
    }

    void Update()
    {
        if (!IsOwner) return;
        SetProjectNetworkServerRpc(NetworkManager.Singleton.LocalClientId);    
    }

    [ServerRpc(RequireOwnership = false)]
    void SetProjectNetworkServerRpc(ulong clientId)
    {
        projectManager = FindObjectOfType<ProjectManager>();
        if (projectManager == null)
        {
            Debug.LogError("Can't find ProjectManager! ,Retrying...");
            projectManager = FindObjectOfType<ProjectManager>();
            if (projectManager == null)
            {
                Debug.LogError("Fail to find ProjectManager!");
            }
        }

        if (projectManager.idProjectDeckList.Count == 0)
        {
            Debug.LogError("ProjectManager Empty!");
            return;
        }

        UpdateProjectCanvas(Project1Canvas, projectManager.idProjectDeckList, 0);
        UpdateProjectCanvas(Project2Canvas, projectManager.idProjectDeckList, 1);
        UpdateProjectCanvas(Project3Canvas, projectManager.idProjectDeckList, 2);
    }

    void UpdateProjectCanvas(Canvas canvas, List<int> projectDeckList, int index)
    {
        if (projectDeckList.Count > index)
        {
            ProjectScriptable project = projectManager.GetProjectById(projectDeckList[index]);
            DisplayProject displayProject = canvas.GetComponent<DisplayProject>();
            displayProject.SetProjectData(project);
            displayProject.UpdateProjectClientRpc(project.id, project.projectName, project.reqIT, project.reqMarketing, project.reqHumanResource, project.reqAccountant, project.reqWorkingPoint, project.upperInt, project.lowerInt, project.upperCondition, project.lowerCondition);

        }
    }

}
*/

public class ProjectSetProjectNetwork : NetworkBehaviour
{
    [SerializeField] private Canvas Project1Canvas;
    [SerializeField] private Canvas Project2Canvas;
    [SerializeField] private Canvas Project3Canvas;
    private ProjectManager projectManager;

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        FindProjectManager();
        if (IsServer)
        {
            StartCoroutine(UpdateProjectsCoroutine());
        }
    }

    private void FindProjectManager()
    {
        projectManager = FindObjectOfType<ProjectManager>();
        if (projectManager == null)
        {
            Debug.LogError("Can't find ProjectManager!");
        }
    }

    private IEnumerator UpdateProjectsCoroutine()
    {
        while (true)
        {
            UpdateProjects();
            yield return new WaitForSeconds(1f); // Update every second, adjust as needed
        }
    }

    private void UpdateProjects()
    {
        if (projectManager == null || projectManager.idProjectDeckList.Count == 0)
        {
            Debug.LogError("ProjectManager is null or empty!");
            return;
        }

        for (int i = 0; i < 3; i++)
        {
            if (projectManager.idProjectDeckList.Count > i)
            {
                ProjectScriptable project = projectManager.GetProjectById(projectManager.idProjectDeckList[i]);
                UpdateProjectClientRpc(i, project.id, project.projectName, project.reqIT, project.reqMarketing,
                    project.reqHumanResource, project.reqAccountant, project.reqWorkingPoint,
                    project.upperInt, project.lowerInt, project.upperCondition, project.lowerCondition);
            }
        }
    }

    [ClientRpc]
    private void UpdateProjectClientRpc(int canvasIndex, int id, string projectName, int reqIT, int reqMarketing,
        int reqHumanResource, int reqAccountant, int reqWorkingPoint,
        int upperInt, int lowerInt, string upperCondition, string lowerCondition)
    {
        Canvas canvas = null;
        switch (canvasIndex)
        {
            case 0: canvas = Project1Canvas; break;
            case 1: canvas = Project2Canvas; break;
            case 2: canvas = Project3Canvas; break;
        }

        if (canvas != null)
        {
            DisplayProject displayProject = canvas.GetComponent<DisplayProject>();
            if (displayProject != null)
            {
                displayProject.UpdateProjectClientRpc(id, projectName, reqIT, reqMarketing, reqHumanResource,
                    reqAccountant, reqWorkingPoint, upperInt, lowerInt, upperCondition, lowerCondition);
            }
        }
    }
}