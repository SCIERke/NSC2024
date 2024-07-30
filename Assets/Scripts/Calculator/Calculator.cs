using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calculator : MonoBehaviour
{
    // Start is called before the first frame update
    public Canvas HologramCalculator;


    private Transform currentPos;
    private bool toggle;
    void Start()
    {
        currentPos = transform;
        toggle = false;
        HologramCalculator.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit) && hit.transform == currentPos)
            {
                toggle = !toggle;
                HologramCalculator.gameObject.SetActive(toggle);
            }
        }
    }
}
