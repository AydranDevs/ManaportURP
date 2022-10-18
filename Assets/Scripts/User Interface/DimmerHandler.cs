using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class DimmerHandler : MonoBehaviour {
    private CanvasGroup canvasGroup;

    [SerializeField] private bool startAtAlphaZero = true;
    [SerializeField] private float fadeInTime = 0.3f;
    [SerializeField] private float fadeOutTime = 0.3f; 

    private void Start() {
        canvasGroup = gameObject.GetComponent<CanvasGroup>();

        if (startAtAlphaZero) {
            canvasGroup.alpha = 0;
        }else {
            canvasGroup.alpha = 1;
        }
    }

    public void FadeIn() {
        LTDescr tweenObject;
        tweenObject = LeanTween.alphaCanvas(canvasGroup, 1, fadeInTime);
        Debug.Log("dimmer activated");
    }

    public void FadeOut() {
        LTDescr tweenObject;
        tweenObject = LeanTween.alphaCanvas(canvasGroup, 0, fadeOutTime);
        Debug.Log("dimmer deactivated");
    }
}
