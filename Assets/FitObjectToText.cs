using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FitObjectToText : MonoBehaviour
{
    private void OnEnable()
    {
        GetComponent<RectTransform>().sizeDelta = GetComponent<TextMeshProUGUI>().GetPreferredValues(GetComponent<TextMeshProUGUI>().text);
    }
}
