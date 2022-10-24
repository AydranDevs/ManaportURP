using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manapotion.UI
{
    public class UI_Beastiary : UI_InventoryBase
    {
        protected override void Abstract_Show()
        {
            main.dimmer.FadeIn();

            LTDescr tweenObject;
            tweenObject = LeanTween.move(transforms[0], new Vector3(-22, 0, 0), 0.3f);
            tweenObject.setEase(LeanTweenType.easeOutQuad);
            if (tweenObject != null)
            {
                tweenObject.setOnComplete(() => { uiState = UIState.Shown; });
            }
        }

        protected override void Abstract_Hide()
        {
            main.dimmer.FadeOut();
            
            LTDescr tweenObject;
            tweenObject = LeanTween.move(transforms[0], new Vector3(-172, 0, 0), 0.3f);
            tweenObject.setEase(LeanTweenType.easeOutQuad);
            if (tweenObject != null)
            {
                tweenObject.setOnComplete(() => { uiState = UIState.Hidden; });
            }
        }
    }
}
