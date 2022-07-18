using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory;

public class MainUIHandler : MonoBehaviour
{
    public static MainUIHandler Instance;

    private bool statsHidden = false;
    private bool invHidden = false;

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
            DimmerHandler.Instance.FadeIn();
        }else {
            if (statsHidden) ShowStats();
            DimmerHandler.Instance.FadeOut();
        }
    }
}
