using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class DimmerHandler : MonoBehaviour
{
    public static DimmerHandler Instance;
    private CanvasGroup canvasGroup;

    private void Start() {
        Instance = this;
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
    }

    public void FadeIn() {
        Debug.Log("dimmer activated");
        LTDescr tweenObject;
        tweenObject = LeanTween.alphaCanvas(canvasGroup, 1, 0.3f);
    }

    public void FadeOut() {
        Debug.Log("dimmer deactivated");
        LTDescr tweenObject;
        tweenObject = LeanTween.alphaCanvas(canvasGroup, 0, 0.3f);
    }
}
