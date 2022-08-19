using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Manapotion.MainMenu {
    public abstract class MainMenuButton : MonoBehaviour {
        public TextMeshProUGUI subtitleObject;
        public string subtitle;

        public abstract void OnHover();
        public abstract void OnClick();
    }
}

