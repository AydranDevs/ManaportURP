using UnityEngine;

namespace Manapotion.UI
{
    public class InventoryUIBase
    {
        public GameObject[] attachedObjects;
        public bool active = false;

        public virtual void Hide()
        {
            if (!active)
            {
                return;
            }

            LTDescr tweenObject;
            tweenObject = LeanTween.move(attachedObjects[0].GetComponent<RectTransform>(), new Vector3(0, 0, 0), 0.3f);
            tweenObject.setEase(LeanTweenType.easeOutQuad);
            active = false;
        }

        public virtual void Show()
        {
            if (active)
            {
                return;
            }

            LTDescr tweenObject;
            tweenObject = LeanTween.move(attachedObjects[0].GetComponent<RectTransform>(), new Vector3(150, 0, 0), 0.3f);
            tweenObject.setEase(LeanTweenType.easeOutQuad);
            active = true;
        }
    }
}
