using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using CodeMonkey.Utils;
using TMPro;

namespace Manapotion.UI
{
    public enum ContextMenuType { Tooltip, ContextMenu }

    public class ContextMenuHandler : MonoBehaviour
    {
        public static ContextMenuHandler Instance;
        public ContextMenuType contextMenuType { get; private set; } = ContextMenuType.ContextMenu;
        
        public string title { get; private set; }
        public string subtitle { get; private set; }
        public string body { get; private set; }

        public string[] options { get; private set; }
        public Action[] optionEvents { get; private set; }

        [SerializeField]
        private Transform optionsContainer;
        [SerializeField]
        private Transform optionTemplate;
        [field: SerializeField]
        public List<ContextMenuOption> optionsList { get; private set; }

        [SerializeField]
        private RectTransform canvasTransform;
        private RectTransform myRectTransform;
        [SerializeField]
        private TextMeshProUGUI _title;
        [SerializeField]
        private TextMeshProUGUI _subtitle;
        [SerializeField]
        private TextMeshProUGUI _body;
        [SerializeField]
        private RectTransform background;

        private const float TEXT_PADDING_LEFT = 3f;
        private const float TEXT_PADDING_TOP = 3f;

        private Vector2 _topRightCorner;
        private const float BOUNDS_X = 416f, BOUNDS_Y = 234f;

        [SerializeField]
        private Vector2 _titlePreferredValues;
        [SerializeField] 
        private Vector2 _subtitlePreferredValues;
        [SerializeField]
        private Vector2 _bodyPreferredValues;

        private RectTransform _rtTitle;
        private RectTransform _rtSubtitle;
        private RectTransform _rtBody;

        private bool isActive = false;
        public bool contextMenuOpen { get; private set; } = false;
        private bool mouseInBounds = false;
        
        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
            }

            optionsList = new List<ContextMenuOption>();
            myRectTransform = GetComponent<RectTransform>();

            _rtTitle = _title.GetComponent<RectTransform>();
            _rtSubtitle = _subtitle.GetComponent<RectTransform>();
            _rtBody = _body.GetComponent<RectTransform>();

            Hide();
        }

        private void Update()
        {
            if ((Mouse.current.leftButton.isPressed || Mouse.current.rightButton.isPressed) && !mouseInBounds)
            {
                if (isActive)
                {
                    Clear();
                    Hide();
                }
                return;
            }

            if (contextMenuType == ContextMenuType.Tooltip)
            {
                myRectTransform.position = Mouse.current.position.ReadValue();
            }
            
            _topRightCorner = new Vector2(myRectTransform.anchoredPosition.x + GetWidth(), myRectTransform.anchoredPosition.y + GetHeight());
            if (_topRightCorner.x >= BOUNDS_X)
            {
                float delta = _topRightCorner.x - BOUNDS_X;
                myRectTransform.anchoredPosition = new Vector2(myRectTransform.anchoredPosition.x - delta, myRectTransform.anchoredPosition.y);
            }
            if (_topRightCorner.y >= BOUNDS_Y)
            {
                float delta = _topRightCorner.y - BOUNDS_Y;
                myRectTransform.anchoredPosition = new Vector2(myRectTransform.anchoredPosition.x, myRectTransform.anchoredPosition.y - delta);
            }
        }

        public void ChangeMouseInBoundsState(bool b)
        {
            mouseInBounds = b;
        }

    #region Static Methods
        public static void Clear()
        {
            Instance.title = "";
            Instance.subtitle = "";
            Instance.body = "";
            foreach (RectTransform child in Instance.optionsContainer)
            {
                Destroy(child.gameObject);
            }
            Instance.optionsList = new List<ContextMenuOption>();

            Instance.SetTitleText(Instance.title);
            Instance.SetSubtitleText(Instance.subtitle);
            Instance.SetBodyText(Instance.body);

            Instance.Format();
        }
        
        /// <summary>
        /// Set the title of the tooltip or context menu object.
        /// </summary>
        /// <param name="s">String to set as the title.</param>
        public static void SetTitle(string s)
        {
            Instance.title = s;
            Instance.SetTitleText(Instance.title);
        }

        /// <summary>
        /// Set the title of the tooltip or context menu object.
        /// </summary>
        /// <param name="s">String to set as the title.</param>
        /// <param name="labelsToOverride">Array that translates to which labels to set to null.</param>
        public static void SetTitle(string s, bool[] labelsToOverride)
        {
            if (labelsToOverride[0])
            {
                Instance.title = "";
                Instance.SetTitleText("");
            }
            if (labelsToOverride[1])
            {
                Instance.subtitle = "";
                Instance.SetSubtitleText("");
            }
            if (labelsToOverride[2])
            {
                Instance.body = "";
                Instance.SetBodyText("");
            }
            Instance.title = s;
            Instance.SetTitleText(Instance.title);
        }

        /// <summary>
        /// Set the subtitle of the tooltip or context menu object.
        /// </summary>
        /// <param name="s">String to set as the subtitle.</param>
        public static void SetSubtitle(string s)
        {
            Instance.subtitle = s;
            Instance.SetSubtitleText(Instance.subtitle);
        }

        /// <summary>
        /// Set the subtitle of the tooltip or context menu object.
        /// </summary>
        /// <param name="s">String to set as the subtitle.</param>
        /// <param name="labelsToOverride">Array that translates to which labels to set to null.</param>
        public static void SetSubtitle(string s, bool[] labelsToOverride)
        {
            if (labelsToOverride[0])
            {
                Instance.title = "";
                Instance.SetTitleText("");
            }
            if (labelsToOverride[1])
            {
                Instance.subtitle = "";
                Instance.SetSubtitleText("");
            }
            if (labelsToOverride[2])
            {
                Instance.body = "";
                Instance.SetBodyText("");
            }
            Instance.subtitle = s;
            Instance.SetSubtitleText(Instance.subtitle);
        }

        /// <summary>
        /// Set the body of the tooltip or context menu object.
        /// </summary>
        /// <param name="s">String to set as the body.</param>
        public static void SetBody(string s)
        {
            Instance.body = s;
            Instance.SetBodyText(Instance.body);
        }

        /// <summary>
        /// Set the body of the tooltip or context menu object.
        /// </summary>
        /// <param name="s">String to set as the body.</param>
        /// <param name="labelsToOverride">Array that translates to which labels to set to null.</param>
        public static void SetBody(string s, bool[] labelsToOverride)
        {
            if (labelsToOverride[0])
            {
                Instance.title = "";
                Instance.SetTitleText("");
            }
            if (labelsToOverride[1])
            {
                Instance.subtitle = "";
                Instance.SetSubtitleText("");
            }
            if (labelsToOverride[2])
            {
                Instance.body = "";
                Instance.SetBodyText("");
            }
            Instance.body = s;
            Instance.SetBodyText(Instance.body);
        }

        public static void AddOption(string optionName, Action funcToCallOnClick)
        {
            Instance.optionsList.Add(new ContextMenuOption { name = optionName, funcOnClick = funcToCallOnClick });

            foreach (Transform child in Instance.optionsContainer)
            {
                if (child != Instance.optionTemplate)
                {
                    Destroy(child.gameObject);
                }
            }

            foreach (var option in Instance.optionsList)
            {
                option.myOptionGameObject = Instantiate(Instance.optionTemplate, Instance.optionsContainer);
                option.myOptionGameObject.Find("Text").GetComponent<TextMeshProUGUI>().SetText(option.name);
                option.myOptionGameObject.GetComponent<Button_UI>().ClickFunc += option.funcOnClick;
                option.myOptionGameObject.gameObject.SetActive(true);
            }
        }

        /// <summary>
        /// Show the context menu or tooltip
        /// </summary>
        /// <param name="cmt"> the type of meny for the menu to operate as </param>
        public static void Show(ContextMenuType cmt)
        {
            Clear();
            Instance.isActive = true;
            Instance.GetComponent<Image>().enabled = true;
            Instance._title.enabled = true;
            Instance._subtitle.enabled = true;
            Instance._body.enabled = true;
            Instance.contextMenuType = cmt;
            if (Instance.contextMenuOpen)
            {
                return;
            }

            if (cmt == ContextMenuType.ContextMenu)
            {
                Instance.Show_ContextMenu();
            }
            else if (cmt == ContextMenuType.Tooltip)
            {
                Instance.Show_Tooltip();
            }
        }
        public static void Hide()
        {
            Instance.contextMenuOpen = false;
            Instance.isActive = false;
            Instance.GetComponent<Image>().enabled = false;
            Instance._title.enabled = false;
            Instance._subtitle.enabled = false;
            Instance._body.enabled = false;
            foreach (RectTransform child in Instance.optionsContainer)
            {
                child.gameObject.SetActive(false);
            }
        }
    #endregion

        public void Show_ContextMenu()
        {
            GetComponent<Image>().raycastTarget = true;
            myRectTransform.position = Mouse.current.position.ReadValue();
            _topRightCorner = new Vector2(myRectTransform.anchoredPosition.x + GetWidth(), myRectTransform.anchoredPosition.y + GetHeight());
            if (_topRightCorner.x >= BOUNDS_X)
            {
                float delta = _topRightCorner.x - BOUNDS_X;
                myRectTransform.anchoredPosition = new Vector2(myRectTransform.anchoredPosition.x - delta, myRectTransform.anchoredPosition.y);
            }
            if (_topRightCorner.y >= BOUNDS_Y)
            {
                float delta = _topRightCorner.y - BOUNDS_Y;
                myRectTransform.anchoredPosition = new Vector2(myRectTransform.anchoredPosition.x, myRectTransform.anchoredPosition.y - delta);
            }
            contextMenuOpen = true;
        }

        public void Show_Tooltip()
        {
            GetComponent<Image>().raycastTarget = false;
        }

        public void SetTitleText(string s)
        {
            _title.text = s;
            Format();
            
        }
        public void SetSubtitleText(string s)
        {
            _subtitle.text = s;
            Format();
        }
        public void SetBodyText(string s)
        {
            _body.text = s;
            Format();
        }

        private void Format()
        {
            _titlePreferredValues = _title.GetPreferredValues(_title.text, float.PositiveInfinity, float.PositiveInfinity);
            _subtitlePreferredValues = _subtitle.GetPreferredValues(_subtitle.text, float.PositiveInfinity, float.PositiveInfinity);
            _bodyPreferredValues = _body.GetPreferredValues(_body.text, float.PositiveInfinity, float.PositiveInfinity);

            SetTextSizeDelta(_rtTitle, _titlePreferredValues.x, _titlePreferredValues.y);
            SetTextSizeDelta(_rtSubtitle, _subtitlePreferredValues.x, _subtitlePreferredValues.y);
            SetTextSizeDelta(_rtBody, _bodyPreferredValues.x, _bodyPreferredValues.y);
        }

        private float GetWidth()
        {
            return myRectTransform.rect.width;
        }

        private float GetHeight()
        {
            return myRectTransform.rect.height;
        }

        private void SetTextSizeDelta(RectTransform rt, float width, float height)
        {
            rt.sizeDelta = new Vector2(width, height);
        }
    }

    [Serializable]
    public class ContextMenuOption
    {
        public string name;
        public Transform myOptionGameObject; 
        public Action funcOnClick;
    }
}

