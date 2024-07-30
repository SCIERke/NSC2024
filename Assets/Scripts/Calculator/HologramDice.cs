using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HologramDice : MonoBehaviour
{
    private int row1;
    private int row2;

    public TextMeshProUGUI TextDice1;
    public TextMeshProUGUI TextDice2;

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (Physics.Raycast(ray, out RaycastHit hit) && hit.transform == transform)
            {
                RunAnimation();
            }
        }
    }
    
    public void RunAnimation()
    {
        StartCoroutine(RollDiceAnimation());
    }

    private IEnumerator RollDiceAnimation()
    {
        for (int i = 0; i < 10; i++)
        {
            int row1 = Random.Range(1, 7);
            int row2 = Random.Range(1, 8);

            TextDice1.text = row1.ToString();
            TextDice2.text = row2.ToString();

            yield return new WaitForSeconds(0.05f);
        }

        // Final result
        int finalRow1 = Random.Range(1, 7);
        int finalRow2 = Random.Range(1, 7);

        TextDice1.text = finalRow1.ToString();
        TextDice2.text = finalRow2.ToString();
    }
}
