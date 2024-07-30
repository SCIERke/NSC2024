using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class DoProject : MonoBehaviour
{
    /*
    private ProjectDeck projectDeck;
    private StatPlayer statPlayer;

    [SerializeField] private Canvas popUpTemplate; 
    [SerializeField] private Canvas projectFailPopup;
    [SerializeField] private Canvas projectFailActionPointPopup;
    [SerializeField] private Canvas HologramCalculator;

    private int ITPlayerhave;
    private int HRPlayerhave;
    private int MKPlayerhave;
    private int ACPlayerhave;

    void Start()
    {
    }

    public void ClickedYes()
    {
        statPlayer = FindObjectOfType<StatPlayer>();
        projectDeck = FindObjectOfType<ProjectDeck>();

        string selectedProjectName = statPlayer.selectedProject.GetComponent<DisplayProject>().projectName;

        if (statPlayer.actionPoints <= 0)
        {
            popUpTemplate.gameObject.SetActive(false);
            projectFailActionPointPopup.gameObject.SetActive(true);
            return;
        }

        if (statPlayer != null )
        {
            ITPlayerhave = statPlayer.itDepartmentCount;
            HRPlayerhave = statPlayer.hrDepartmentCount;
            MKPlayerhave = statPlayer.marketingDepartmentCount;
            ACPlayerhave = statPlayer.accountingDepartmentCount;
        }
        if (selectedProjectName == "Project A")
        {
            Project projectDo = ProjectDatabase.Projects[1]; // Project A
            if (ITPlayerhave >= projectDo.reqIT && HRPlayerhave >= projectDo.reqHumanResource && MKPlayerhave >= projectDo.reqMarketing && ACPlayerhave >= projectDo.reqAccountant)
            {
                HologramDice hologramDice = HologramCalculator.gameObject.GetComponent<HologramDice>();
                HologramCalculator.gameObject.SetActive(true);
                hologramDice.RunAnimation();
                //if ()
                //{
                    Debug.Log("Project A Picked");
                    statPlayer.projectCount += 1;
                    statPlayer.projectListHave.Add(projectDo);
                    projectDeck.DeleteProject(projectDo.projectName);
                    popUpTemplate.gameObject.SetActive(false);
                //}
            }
            else
            {
                projectFailPopup.gameObject.SetActive(true);
            }
        } else if (selectedProjectName == "Project C")
        {
            Project projectDo = ProjectDatabase.Projects[3]; // Project C
            if (ITPlayerhave >= projectDo.reqIT && HRPlayerhave >= projectDo.reqHumanResource && MKPlayerhave >= projectDo.reqMarketing && ACPlayerhave >= projectDo.reqAccountant)
            {
                Debug.Log("Project C Picked");
                statPlayer.projectCount += 1;
                statPlayer.projectListHave.Add(projectDo);
                projectDeck.DeleteProject(projectDo.projectName);
                popUpTemplate.gameObject.SetActive(false);
            }
            else {
                projectFailPopup.gameObject.SetActive(true);
            }
        } else if (selectedProjectName == "Project K")
        {
            Project projectDo = ProjectDatabase.Projects[10]; // Project K
            if (ITPlayerhave >= projectDo.reqIT && HRPlayerhave >= projectDo.reqHumanResource && MKPlayerhave >= projectDo.reqMarketing && ACPlayerhave >= projectDo.reqAccountant)
            {
                Debug.Log("Project K Picked");
                statPlayer.projectCount += 1;
                statPlayer.projectListHave.Add(projectDo);
                projectDeck.DeleteProject(projectDo.projectName);
                popUpTemplate.gameObject.SetActive(false);
            }
            else
            {
                projectFailPopup.gameObject.SetActive(true);
            }
        } else if (selectedProjectName == "Project L")
        {
            Project projectDo = ProjectDatabase.Projects[11]; // Project L
            if (ITPlayerhave >= projectDo.reqIT && HRPlayerhave >= projectDo.reqHumanResource && MKPlayerhave >= projectDo.reqMarketing && ACPlayerhave >= projectDo.reqAccountant)
            {
                HologramDice hologramDice = HologramCalculator.gameObject.GetComponent<HologramDice>();
                HologramCalculator.gameObject.SetActive(true);
                hologramDice.RunAnimation();

                Debug.Log("Project L Picked");
                statPlayer.projectCount += 1;
                statPlayer.projectListHave.Add(projectDo);
                projectDeck.DeleteProject(projectDo.projectName);
                popUpTemplate.gameObject.SetActive(false);
            }
            else
            {
                projectFailPopup.gameObject.SetActive(true);
            }
        }

    }

    public void ClickedNo() {
        popUpTemplate.gameObject.SetActive(false);
    }
    */
}
