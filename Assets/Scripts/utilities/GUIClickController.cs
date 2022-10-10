    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.EventSystems;
    using UnityEngine.Events;
    
    [RequireComponent(typeof(Button))]
    public class GUIClickController : MonoBehaviour, IPointerClickHandler
        {
            public UnityEvent onLeft;
            public UnityEvent onRight;
            public UnityEvent onMiddle;
     
            public void OnPointerClick(PointerEventData eventData)
            {
                if (eventData.button == PointerEventData.InputButton.Left)
                {
                    onLeft.Invoke();
                }
                else if (eventData.button == PointerEventData.InputButton.Right)
                {
                    onRight.Invoke();
                }
                else if (eventData.button == PointerEventData.InputButton.Middle)
                {
                    onMiddle.Invoke();
                }
            }
        }
     
