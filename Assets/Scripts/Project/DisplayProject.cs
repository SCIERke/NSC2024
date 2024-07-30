
/*
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DisplayProject : MonoBehaviour
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

    public TextMeshProUGUI projectNameText;
    public TextMeshProUGUI projectWorkingPointText;
    public TextMeshProUGUI upperIntText;
    public TextMeshProUGUI lowerIntText;
    public TextMeshProUGUI upperConditionText;
    public TextMeshProUGUI lowerConditionText;

    public Image LogoField1;
    public Image LogoField2;
    public Image LogoField3;
    public Image LogoField4;

    private Sprite ITLogoImage;
    private Sprite MKlogoImage;
    private Sprite HRlogoImage;
    private Sprite AClogoImage;

    private Image[] logoFields;

    void Awake()
    {
        ITLogoImage = Resources.Load<Sprite>("Picture/Logo/IT_Logo");
        MKlogoImage = Resources.Load<Sprite>("Picture/Logo/Marketing_Logo");
        HRlogoImage = Resources.Load<Sprite>("Picture/Logo/Human_Resource_Logo");
        AClogoImage = Resources.Load<Sprite>("Picture/Logo/Account_Logo");

        if (ITLogoImage == null || MKLogoImage == null || HRLogoImage == null || ACLogoImage == null)
        {
            Debug.LogError("One or more logo images failed to load.");
        }

        logoFields = new Image[] { LogoField1, LogoField2, LogoField3, LogoField4 };

        UpdateProjectInfo();
    }

    void Update()
    {
    }

    public void SetProjectData(ProjectScriptable project)
    {
        id = project.id;
        projectName = project.projectName;
        reqIT = project.reqIT;
        reqMarketing = project.reqMarketing;
        reqHumanResource = project.reqHumanResource;
        reqAccountant = project.reqAccountant;
        reqWorkingPoint = project.reqWorkingPoint;
        upperInt = project.upperInt;
        lowerInt = project.lowerInt;
        upperCondition = project.upperCondition;
        lowerCondition = project.lowerCondition;

        UpdateProjectInfo();
    }

    void UpdateProjectInfo()
    {
        projectNameText.text = projectName;
        projectWorkingPointText.text = reqWorkingPoint.ToString() + " WP";
        upperIntText.text = "+" + upperInt.ToString();
        lowerIntText.text = "-" + lowerInt.ToString();
        upperConditionText.text = upperCondition;
        lowerConditionText.text = lowerCondition;

        SetLogos(reqIT, ITLogoImage);
        SetLogos(reqMarketing, MKlogoImage);
        SetLogos(reqHumanResource, HRlogoImage);
        SetLogos(reqAccountant, AClogoImage);
    }

    [ClientRpc]
    public void UpdateProjectClientRpc(int Id, string ProjectName, int ReqIT, int ReqMarketing ,int ReqHumanResource ,int ReqAccountant , int ReqWorkingPoint ,int UpperInt ,int LowerInt ,string UpperCondition , string LowerCondition)
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

        UpdateProjectInfo();
    }

    void SetLogos(int reqCount, Sprite logoImage)
    {
        for (int i = 0; i < logoFields.Length && reqCount > 0; i++)
        {
            if (logoFields[i].sprite == null)
            {
                logoFields[i].sprite = logoImage;
                reqCount--;
            }
        }
    }
}
*/

using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class DisplayProject : MonoBehaviour
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

    public TextMeshProUGUI projectNameText;
    public TextMeshProUGUI projectWorkingPointText;
    public TextMeshProUGUI upperIntText;
    public TextMeshProUGUI lowerIntText;
    public TextMeshProUGUI upperConditionText;
    public TextMeshProUGUI lowerConditionText;

    public Image LogoField1;
    public Image LogoField2;
    public Image LogoField3;
    public Image LogoField4;

    private Sprite ITLogoImage;
    private Sprite MKLogoImage;
    private Sprite HRLogoImage;
    private Sprite ACLogoImage;

    private Image[] logoFields;

    void Awake()
    {
        ITLogoImage = Resources.Load<Sprite>("Picture/Logo/IT_Logo");
        MKLogoImage = Resources.Load<Sprite>("Picture/Logo/Marketing_Logo");
        HRLogoImage = Resources.Load<Sprite>("Picture/Logo/Human_Resource_Logo");
        ACLogoImage = Resources.Load<Sprite>("Picture/Logo/Account_Logo");

        if (ITLogoImage == null || MKLogoImage == null || HRLogoImage == null || ACLogoImage == null)
        {
            Debug.LogError("One or more logo images failed to load.");
        }

        logoFields = new Image[] { LogoField1, LogoField2, LogoField3, LogoField4 };

        UpdateProjectInfo();
    }

    void Update()
    {
    }

    public void SetProjectData(ProjectScriptable project)
    {
        id = project.id;
        projectName = project.projectName;
        reqIT = project.reqIT;
        reqMarketing = project.reqMarketing;
        reqHumanResource = project.reqHumanResource;
        reqAccountant = project.reqAccountant;
        reqWorkingPoint = project.reqWorkingPoint;
        upperInt = project.upperInt;
        lowerInt = project.lowerInt;
        upperCondition = project.upperCondition;
        lowerCondition = project.lowerCondition;

        UpdateProjectInfo();
    }

    void UpdateProjectInfo()
    {
        
        projectNameText.text = projectName;

        if (projectWorkingPointText != null)
            projectWorkingPointText.text = reqWorkingPoint.ToString() + " WP";

        if (upperIntText != null)
            upperIntText.text = "+" + upperInt.ToString();

        if (lowerIntText != null)
            lowerIntText.text = "-" + lowerInt.ToString();

        if (upperConditionText != null)
            upperConditionText.text = upperCondition;

        if (lowerConditionText != null)
            lowerConditionText.text = lowerCondition;

        ClearLogos();
        SetLogos(reqIT, ITLogoImage);
        SetLogos(reqMarketing, MKLogoImage);
        SetLogos(reqHumanResource, HRLogoImage);
        SetLogos(reqAccountant, ACLogoImage);
    }

    [ClientRpc]
    public void UpdateProjectClientRpc(int Id, string ProjectName, int ReqIT, int ReqMarketing, int ReqHumanResource, int ReqAccountant, int ReqWorkingPoint, int UpperInt, int LowerInt, string UpperCondition, string LowerCondition)
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

        UpdateProjectInfo();
    }

    void SetLogos(int reqCount, Sprite logoImage)
    {
        for (int i = 0; i < logoFields.Length && reqCount > 0; i++)
        {
            if (logoFields[i].sprite == null)
            {
                logoFields[i].sprite = logoImage;
                reqCount--;
            }
        }
    }

    void ClearLogos()
    {
        foreach (var logoField in logoFields)
        {
            logoField.sprite = null;
        }
    }
}
