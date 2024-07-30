using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectDatabaseNetwork : MonoBehaviour
{
    public static List<ProjectScriptable> Projects = new List<ProjectScriptable>();
    public static Dictionary<int, ProjectScriptable> ProjectDictionary = new Dictionary<int, ProjectScriptable>();

    [SerializeField] private ProjectScriptable[] projectAssets;

    void Awake()
    {
        Projects = new List<ProjectScriptable>(projectAssets);
        foreach (var project in projectAssets)
        {
            if (!ProjectDictionary.ContainsKey(project.id))
            {
                ProjectDictionary.Add(project.id, project);
            }
        }
    }

    public static ProjectScriptable GetProjectById(int id)
    {
        ProjectDictionary.TryGetValue(id, out ProjectScriptable project);
        return project;
    }
}
