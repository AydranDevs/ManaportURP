using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory;

public class MainUIHandler : MonoBehaviour
{
    public static MainUIHandler Instance;

    [SerializeField] private DimmerHandler dimmer;

    private bool statsHidden = false;

    private void Start() {
        Instance = this;
        GameStateManager.Instance.OnGameStateChanged += GameStateChanged;
    }

    public void HideStats() {
        StatsUIHandler.Instance.Hide();
        statsHidden = true;
    }
    public void ShowStats() {
        StatsUIHandler.Instance.Show();
        statsHidden = false;
    }

    public void GameStateChanged(object sender, GameStateManager.OnGameStateChangedArgs e) {
        if (GameStateManager.Instance.state != GameState.Main) {
            HideStats();
            dimmer.FadeIn();
        }else {
            if (statsHidden) ShowStats();
            dimmer.FadeOut();
        }
    }
}
