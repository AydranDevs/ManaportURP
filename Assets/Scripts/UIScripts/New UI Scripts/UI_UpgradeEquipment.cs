using UnityEngine;
using Manapotion.UI;

public class UI_UpgradeEquipment : InventoryUIBase 
{
    public override void Hide()
    {
        if (showing)
        {
            return;
        }
        if (!active)
        {
            return;
        }
        hiding = true;

        LTDescr tweenObject;
        tweenObject = LeanTween.move(attachedObjects[0].GetComponent<RectTransform>(), new Vector3(0, 234, 0), 0.3f);
        tweenObject.setEase(LeanTweenType.easeOutQuad);
        if (tweenObject != null)
        {
            tweenObject.setOnComplete(() => { hiding = false; });
        }
        active = false;
    }

    public override void Show()
    {
        if (hiding)
        {
            return;
        }
        if (active)
        {
            return;
        }
        showing = true;

        LTDescr tweenObject;
        tweenObject = LeanTween.move(attachedObjects[0].GetComponent<RectTransform>(), new Vector3(0, 0, 0), 0.3f);
        tweenObject.setEase(LeanTweenType.easeOutQuad);
        if (tweenObject != null)
        {
            tweenObject.setOnComplete(() => { showing = false; });
        }
        active = true;
    }
}