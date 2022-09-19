using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class TooltipHandler : MonoBehaviour
{
    private static TooltipHandler Instance;

    public TextMeshProUGUI tooltip;
    public RectTransform background;
    
    private void Awake()
    {
        Instance = this;
        HideTooltip();
    }

    public void OnMouseMove(InputAction.CallbackContext context)
    {
        transform.position = context.ReadValue<Vector2>();
    }

    private void UpdateTooltip(string tooltipText) {
        tooltip.text = tooltipText;
        float textPadding = 4f;
        Vector2 bgSize = new Vector2(tooltip.preferredWidth + textPadding * 2, tooltip.preferredHeight + textPadding * 2);
        background.sizeDelta = bgSize;
    }

    private void ShowTooltip(string tooltipText)
    {
        gameObject.SetActive(true);

        tooltip.text = tooltipText;
        float textPadding = 4f;
        Vector2 bgSize = new Vector2(tooltip.preferredWidth + textPadding * 2, tooltip.preferredHeight + textPadding * 2);
        background.sizeDelta = bgSize;
    }

    private void HideTooltip()
    {
        gameObject.SetActive(false);
    }

    private void EnableInspectCursor()
    {
        GameStateManager.Instance.ChangeCursor(GameStateManager.Instance.inspectCursor);
    }

    private void DisableInspectCursor() {
        GameStateManager.Instance.ChangeCursor(GameStateManager.Instance.defaultCursor);
    }

    public static void UpdateTooltip_Static(string tooltipText) {
        Instance.UpdateTooltip(tooltipText);
    }

    public static void ShowTooltip_Static(string tooltipText) {
        Instance.ShowTooltip(tooltipText);
    }

    public static void HideTooltip_Static() {
        Instance.HideTooltip();
    }

    public static void EnableInspectCursor_Static() {
        Instance.EnableInspectCursor();
    }

    public static void DisableInspectCursor_Static() {
        Instance.DisableInspectCursor();
    }
}
