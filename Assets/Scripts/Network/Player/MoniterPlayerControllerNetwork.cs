using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class MoniterPlayerControllerNetwork : NetworkBehaviour
{
    [SerializeField] public Image AP_1;
    [SerializeField] public Image AP_2;
    [SerializeField] public Image AP_3;

    [SerializeField] public TextMeshProUGUI dayText;
    [SerializeField] public TextMeshProUGUI workingPointText;
}
