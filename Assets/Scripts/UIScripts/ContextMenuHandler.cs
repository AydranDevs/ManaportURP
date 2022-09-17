using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ContextMenuHandler : MonoBehaviour
{
    public static ContextMenuHandler Instance;
    
    public string title { get; private set; }
    public string subtitle { get; private set; }
    public string body { get; private set; }

    public string[] options { get; private set; }
    public Action[] optionEvents { get; private set; }

    [SerializeField]
    private TextMeshProUGUI _title;
    [SerializeField]
    private TextMeshProUGUI _subtitle;
    [SerializeField]
    private TextMeshProUGUI _body;
    [SerializeField]
    private RectTransform background;

    private const float textPadding = 4f;

    void Awake()
    {
        if (Instance != null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

        SetTitle("this is a test title");
    }

    public static void SetTitle(string s)
    {
        Instance.title = s;
        Instance.SetTitleText(Instance.title);
    }
    public static void SetSubtitle(string s)
    {
        Instance.subtitle = s;
        Instance.SetSubtitleText(Instance.subtitle);
    }
    public static void SetBody(string s)
    {
        Instance.subtitle = s;
        Instance.SetBodyText(Instance.body);
    }

    public static void SetOptionsAmount(int i)
    {
        Instance.options = new string[i];
        Instance.optionEvents = new Action[i];
    }
    public static void SetOptionTitle(int i, string s)
    {
        Instance.options[i] = s;
    }

    public static void Show()
    {
        Instance.gameObject.SetActive(true);
    }
    public static void Hide()
    {
        Instance.gameObject.SetActive(false);
    }

    public void SetTitleText(string s)
    {
        _title.text = s;
        Vector2 bgSize = new Vector2(_title.preferredWidth + textPadding * 2, _title.preferredHeight + textPadding * 2);
        background.sizeDelta = bgSize;
    }
    public void SetSubtitleText(string s)
    {
        _subtitle.text = s;
    }
    public void SetBodyText(string s)
    {
        _subtitle.text = s;
    }
}
