using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manapotion.Utilities
{
    public static class CamHelper
    {
        public static Camera GetMainCamera()
        {
            return Camera.main;
        }

        public static void ZoomInOut(Camera cam, Action<float,float> action, float to, float time)
        {
            LTDescr l;
            l = LeanTween.value(cam.gameObject, cam.orthographicSize, to, time);
        }
        public static void ZoomInOut(Camera cam, Action<float,float> action, float to, float time, LeanTweenType tweenType)
        {
            LTDescr l;
            l = LeanTween.value(cam.gameObject, cam.orthographicSize, to, time);
            l.setEase(tweenType);
        }
    }
}

