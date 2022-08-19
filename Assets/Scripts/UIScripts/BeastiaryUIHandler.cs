using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory {
    public class BeastiaryUIHandler : MonoBehaviour {
        private static BeastiaryUIHandler Instance;
        
        bool active = false;

        private void Awake() {
            Instance = this;
        }

        public void Hide() {
            if (!active) return;

            LTDescr tweenObject;
            tweenObject = LeanTween.move(gameObject.GetComponent<RectTransform>(), new Vector3(0, 0, 0), 0.3f);
            tweenObject.setEase(LeanTweenType.easeOutQuad);
            active = false;
        }

        public void Show() {
            if (active) return;

            LTDescr tweenObject;
            tweenObject = LeanTween.move(gameObject.GetComponent<RectTransform>(), new Vector3(150, 0, 0), 0.3f);
            tweenObject.setEase(LeanTweenType.easeOutQuad);
            active = true;
        }

        public static void HideBeastiary() { Instance.Hide(); }
        public static void ShowBeastiary() { Instance.Show(); }
    }
}
