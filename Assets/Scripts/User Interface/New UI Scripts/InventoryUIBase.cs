using UnityEngine;

namespace Manapotion.UI
{
    public class InventoryUIBase
    {
        public GameObject[] attachedObjects;
        public bool active = false;

        public bool hiding { get; set; } = false;
        public bool showing { get; set; } = false;

        public MainUIManager main;

        public virtual void Hide()
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
            tweenObject = LeanTween.move(attachedObjects[0].GetComponent<RectTransform>(), new Vector3(0, 0, 0), 0.3f);
            tweenObject.setEase(LeanTweenType.easeOutQuad);
            if (tweenObject != null)
            {
                tweenObject.setOnComplete(() => { hiding = false; });
            }
            active = false;
        }

        public virtual void Show()
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
            tweenObject = LeanTween.move(attachedObjects[0].GetComponent<RectTransform>(), new Vector3(150, 0, 0), 0.3f);
            tweenObject.setEase(LeanTweenType.easeOutQuad);
            if (tweenObject != null)
            {
                tweenObject.setOnComplete(() => { showing = false; });
            }
            active = true;
        }
    
        public virtual void Refresh() { }
    }
}
