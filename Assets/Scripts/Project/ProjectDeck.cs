using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectDeck : MonoBehaviour
{
    
    // Start is called before the first frame update
    public int deckProjectSize;
    public List<Project> deck_Project = new List<Project>();
    private int x;
    public Canvas Project1;
    public Canvas Project2;
    public Canvas Project3;

    void Start()
    {
        //RealGameCode
        /*for (int i = 0; i < deckProjectSize; i++)
        {
            x = Random.Range(0, ProjectDatabase.Projects.Count);
            deck_Project.Add(ProjectDatabase.Projects[x]);
        }*/

        //FakeCode
        /*
        deck_Project.Add(ProjectDatabase.Projects[1]); // Project A
        deck_Project.Add (ProjectDatabase.Projects[3]); // Project C
        deck_Project.Add(ProjectDatabase.Projects[10]); // Project K
        deck_Project.Add(ProjectDatabase.Projects[11]); // Project L
        */
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (deck_Project.Count == 0) {
            Debug.Log("Deck Project Empty");
        }
        if (deck_Project.Count >= 1)
        {
            Project project1 = deck_Project[0];
            DisplayProject displayProject1 = Project1.GetComponent<DisplayProject>();
            displayProject1.SetProjectData(project1);
        }
        if (deck_Project.Count >= 2)
        {
            Project project2 = deck_Project[1];
            DisplayProject displayProject2 = Project2.GetComponent<DisplayProject>();
            displayProject2.SetProjectData(project2);

        }
        if (deck_Project.Count >= 3)
        {
            Project project3 = deck_Project[2];
            DisplayProject displayProject3 = Project3.GetComponent<DisplayProject>();
            displayProject3.SetProjectData(project3);
        }
        */
    }
    
    /*
    public void DeleteProject(string projectname)
    {
        for (int i = 0; i < deck_Project.Count; i++)
        {
            if (deck_Project[i].projectName == projectname)
            {
                deck_Project.RemoveAt(i);
            }
        }
    }
    */
    
}
