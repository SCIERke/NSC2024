using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Project" ,menuName ="Project")]
public class ProjectScriptable : ScriptableObject
{
    public int id;
    public string projectName;
    public int reqIT;
    public int reqMarketing;
    public int reqHumanResource;
    public int reqAccountant;
    public int reqWorkingPoint;
    public int upperInt;
    public int lowerInt;
    public string upperCondition;
    public string lowerCondition;


    public void Print()
    {
        Debug.Log(projectName);
    }
}
