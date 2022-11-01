using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(RectTransform))]
public class FitObjectToChildPreferredValues : MonoBehaviour
{
    [SerializeField]
    private RectTransform _childObject;
    private RectTransform _myRectTransform;

    private void OnEnable()
    {
        _myRectTransform = GetComponent<RectTransform>();
        var text = _childObject.GetComponent<TextMeshProUGUI>();

        _myRectTransform.sizeDelta = new Vector2(text.GetPreferredValues().x, 12f);
    }
}
