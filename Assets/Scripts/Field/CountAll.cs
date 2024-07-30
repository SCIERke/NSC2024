using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountAll : MonoBehaviour
{
    public int OwnWorkingPoint = 0;

    void Update()
    {
        if (transform.childCount != 0)
        {
            Transform childTransform = transform.GetChild(0);

            DisplayCard displaycardChild = childTransform.GetComponent<DisplayCard>();
            if (displaycardChild != null)
            {
                OwnWorkingPoint = displaycardChild.workingpoint;
            }
        }
        else
        {
            OwnWorkingPoint = 0; // Reset to 0 if no children
        }
    }
}
