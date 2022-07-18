using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsUIHandler : MonoBehaviour
{
    public static StatsUIHandler Instance;

    private void Start() {
        Instance = this;
    }

    public void Hide() {
        LTDescr tweenObject;
        tweenObject = LeanTween.move(gameObject.GetComponent<RectTransform>(), new Vector3(-70, 0, 0), 0.3f);
        tweenObject.setEase(LeanTweenType.easeOutQuad);
    }

    public void Show() {
        LTDescr tweenObject;
        tweenObject = LeanTween.move(gameObject.GetComponent<RectTransform>(), new Vector3(0, 0, 0), 0.3f);
        tweenObject.setEase(LeanTweenType.easeOutQuad);
    }
}
