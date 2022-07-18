using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIAnimationTypes
{
    Move,
    Scale,
    ScaleX,
    ScaleY,
    Fade
}

public class UITweener : MonoBehaviour {
    public GameObject objectToAnimate;
    private RectTransform objectToAnimateRectTransform;

    public UIAnimationTypes animationType;
    public LeanTweenType easeType;
    public float duration;
    public float delay;

    public bool loop;
    public bool pingpong;
    public bool startPosOffset;
    public Vector3 from;
    public Vector3 to;

    private LTDescr _tweenObject;
    
    public bool showOnEnable;
    public bool workOnDisable;

    public void OnEnable() {
        if (showOnEnable) Show();
    }

    public void Show() {
        HandleTween();
    }

    public void HandleTween() {
        if (objectToAnimate == null) objectToAnimate = gameObject;
        objectToAnimateRectTransform = objectToAnimate.GetComponent<RectTransform>();

        switch (animationType) {
            case UIAnimationTypes.Move:
                MoveAbsolute();
                break;
            case UIAnimationTypes.Scale:
                Scale();
                break;
            case UIAnimationTypes.ScaleX:
                Scale();
                break;
            case UIAnimationTypes.ScaleY:
                Scale();
                break;
            case UIAnimationTypes.Fade:
                Fade();
                break;
        }
        
        _tweenObject.setDelay(delay);
        _tweenObject.setEase(easeType);

        if (loop) _tweenObject.loopCount = 999999;
        if (pingpong) _tweenObject.setLoopPingPong();
    }

    public void MoveAbsolute() {
        
        objectToAnimateRectTransform.anchoredPosition = from;

        _tweenObject = LeanTween.move(objectToAnimateRectTransform, to, duration);
    }

    public void Scale() {
        if (startPosOffset) objectToAnimateRectTransform.localScale = from;
        _tweenObject = LeanTween.scale(objectToAnimate, to, duration);
    }

    public void Fade() {
        if (gameObject.GetComponent<CanvasGroup>() == null) gameObject.AddComponent<CanvasGroup>();
        
        if (startPosOffset) objectToAnimate.GetComponent<CanvasGroup>().alpha = from.x;
        _tweenObject = LeanTween.alphaCanvas(objectToAnimate.GetComponent<CanvasGroup>(), to.x, duration);
    }

    void SwapDirection() {
        var temp = from;
        from = to;
        to = temp;
    }

    public void Disable() {
        SwapDirection();
        HandleTween();

        _tweenObject.setOnComplete(() => {
            SwapDirection();
            gameObject.SetActive(false);
        });
    }
}
