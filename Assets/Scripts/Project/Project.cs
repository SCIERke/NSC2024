using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class Project
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

    public Project()
    {
        
    }
    public Project(int Id,string ProjectName ,int ReqIT ,int ReqMarketing ,int ReqHumanResource ,int ReqAccountant ,int ReqWorkingPoint ,int UpperInt ,int LowerInt ,string UpperCondition ,string LowerCondition)
    {
        id = Id;
        projectName = ProjectName;
        reqIT = ReqIT;
        reqMarketing = ReqMarketing;
        reqHumanResource = ReqHumanResource;
        reqAccountant = ReqAccountant;
        reqWorkingPoint = ReqWorkingPoint;
        upperInt = UpperInt;
        lowerInt = LowerInt;
        upperCondition = UpperCondition;
        lowerCondition = LowerCondition;
    }
}
