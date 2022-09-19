using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
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

        private const float TEXT_PADDING = 3f;

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
            myRectTransform = GetComponent<RectTransform>();

            _rtTitle = _title.GetComponent<RectTransform>();
            _rtSubtitle = _subtitle.GetComponent<RectTransform>();
            _rtBody = _body.GetComponent<RectTransform>();

            Hide();

            SetTitle("Training Spelltome");
            // SetSubtitle("a Spelltome gifted to students of MUAA");
            // SetBody("- +50 ATK \n- +120 DEF \n- +20 INTL \n- +999 FLRTXT");
        }

        private void Update()
        {
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
        }

    #region Static Methods
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
            Instance.body = s;
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

        public static void Show(ContextMenuType cmt)
        {
            Instance.GetComponent<Image>().enabled = true;
            Instance._title.enabled = true;
            Instance._subtitle.enabled = true;
            Instance._body.enabled = true;
            Instance.contextMenuType = cmt; 
        }
        public static void Hide()
        {
            Instance.GetComponent<Image>().enabled = false;
            Instance._title.enabled = false;
            Instance._subtitle.enabled = false;
            Instance._body.enabled = false;
        }
    #endregion

        public void SetTitleText(string s)
        {
            _title.text = s;
            // if (_title.preferredWidth > _subtitle.preferredWidth || _title.preferredWidth > _body.preferredWidth)
            // {
            //     bgSize = new Vector2(_title.preferredWidth + textPadding * 2, background.sizeDelta.y);
            // }
            // else
            // {
            //     bgSize = new Vector2(background.sizeDelta.x, background.sizeDelta.y);
            // }
            // background.sizeDelta = bgSize;
            Format();
            
        }
        public void SetSubtitleText(string s)
        {
            _subtitle.text = s;
            // if (_subtitle.preferredWidth > _title.preferredWidth || _subtitle.preferredWidth > _body.preferredWidth)
            // {
            //     bgSize = new Vector2(_subtitle.preferredWidth + textPadding * 2, background.sizeDelta.y + _subtitle.preferredHeight);
            // }
            // else
            // {
            //     bgSize = new Vector2(background.sizeDelta.x, background.sizeDelta.y + _subtitle.preferredHeight);
            // }
            // background.sizeDelta = bgSize;
            Format();
        }
        public void SetBodyText(string s)
        {
            _body.text = s;
            // if (_body.preferredWidth > _title.preferredWidth || _body.preferredWidth > _subtitle.preferredWidth)
            // {
            //     bgSize = new Vector2(_body.preferredWidth + textPadding * 2, background.sizeDelta.y + _body.preferredHeight);
            // }
            // else
            // {
            //     bgSize = new Vector2(background.sizeDelta.x, background.sizeDelta.y + _body.preferredHeight);
            // }
            // background.sizeDelta = bgSize;
            Format();
        }

        private void Format()
        {
            _titlePreferredValues = _title.GetPreferredValues(_title.text, float.PositiveInfinity, float.PositiveInfinity);
            _subtitlePreferredValues = _subtitle.GetPreferredValues(_subtitle.text, float.PositiveInfinity, float.PositiveInfinity);
            _bodyPreferredValues = _body.GetPreferredValues(_body.text, float.PositiveInfinity, float.PositiveInfinity);
            
            SetTextSizeDelta(_rtTitle, _titlePreferredValues.x + TEXT_PADDING * 2, _titlePreferredValues.y + TEXT_PADDING * 2);
            SetTextSizeDelta(_rtSubtitle, _subtitlePreferredValues.x + TEXT_PADDING * 2, _subtitlePreferredValues.y + TEXT_PADDING * 2);
            SetTextSizeDelta(_rtBody, _bodyPreferredValues.x + TEXT_PADDING * 2, _bodyPreferredValues.y + TEXT_PADDING * 2);

            _rtSubtitle.anchoredPosition = new Vector2(_rtSubtitle.anchoredPosition.x, GetHeight() - (_rtTitle.rect.height + TEXT_PADDING));
            _rtBody.anchoredPosition = new Vector2(_rtBody.anchoredPosition.x, GetHeight() - (_rtTitle.rect.height + _rtSubtitle.rect.height - TEXT_PADDING * 2));

            var width = Math.Max(Math.Max(_titlePreferredValues.x, _subtitlePreferredValues.x), _bodyPreferredValues.x) + TEXT_PADDING * 2;
            var height = _titlePreferredValues.y + _subtitlePreferredValues.y +_bodyPreferredValues.y + TEXT_PADDING * 2;
            
            background.sizeDelta = new Vector2(width, height);
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
            rt.sizeDelta = new Vector2(width + TEXT_PADDING * 2, height + TEXT_PADDING * 2);
        }
    }
}

