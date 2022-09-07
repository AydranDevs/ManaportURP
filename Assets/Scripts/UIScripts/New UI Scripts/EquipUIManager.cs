using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manapotion.PartySystem.Inventory;
using Manapotion.PartySystem;
using Manapotion.Utilities;

namespace Manapotion.UI
{
    public class EquipUIManager : InventoryUIBase
    {
        private enum CamState { ZoomingIn, ZoomedIn, ZoomingOut, ZoomedOut }
        private CamState camState = CamState.ZoomedOut;

        private const float DEFAULT_CAMERA_SIZE = 7.3125f;
        private MainUIManager _main;

        public EquipUIManager(MainUIManager main)
        {
            _main = main;
        }

        public override void Show()
        {
            if (active)
            {
                return;
            }

            LTDescr tweenObject1;
            tweenObject1 = LeanTween.move(attachedObjects[0].GetComponent<RectTransform>(), new Vector3(0, 0, 0), 0.3f);
            tweenObject1.setEase(LeanTweenType.easeOutQuad);

            LTDescr tweenObject2;
            tweenObject2 = LeanTween.move(attachedObjects[1].GetComponent<RectTransform>(), new Vector3(-172, 0, 0), 0.3f);
            tweenObject2.setEase(LeanTweenType.easeOutQuad);
            active = true;

            camState = CamState.ZoomingIn;
            Manapotion.ManaBehaviour.OnUpdate += UpdateCam;
        }

        public override void Hide()
        {
            if (!active)
            {
                return;
            }

            LTDescr tweenObject1;
            tweenObject1 = LeanTween.move(attachedObjects[0].GetComponent<RectTransform>(), new Vector3(-172, 0, 0), 0.3f);
            tweenObject1.setEase(LeanTweenType.easeOutQuad);

            LTDescr tweenObject2;
            tweenObject2 = LeanTween.move(attachedObjects[1].GetComponent<RectTransform>(), new Vector3(0, 0, 0), 0.3f);
            tweenObject2.setEase(LeanTweenType.easeOutQuad);
            active = false;

            camState = CamState.ZoomingOut;
            Manapotion.ManaBehaviour.OnUpdate += UpdateCam; 
        }

        private void UpdateCam()
        {
            if (camState == CamState.ZoomingIn)
            {
                Camera.main.orthographicSize = Mathf.MoveTowards(Camera.main.orthographicSize, 2f, 20f * Time.deltaTime);
                if (Camera.main.orthographicSize == 2f)
                {
                    camState = CamState.ZoomedIn;
                    Manapotion.ManaBehaviour.OnUpdate -= UpdateCam; 
                }
            }

            if (camState == CamState.ZoomingOut)
            {
                Camera.main.orthographicSize = Mathf.MoveTowards(Camera.main.orthographicSize, DEFAULT_CAMERA_SIZE, 20f * Time.deltaTime);
                if (Camera.main.orthographicSize == DEFAULT_CAMERA_SIZE)
                {
                    camState = CamState.ZoomedOut;
                    Manapotion.ManaBehaviour.OnUpdate -= UpdateCam; 
                }
            }
        }
    }
}