using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Video;
using UnityEngine.Playables;

namespace Manapotion.MainMenu {
    
    public enum MainMenuState {
        Inactive,
        Start,
        Start_Pressed,
        Main_Menu
    }
    
    /*
    class for managing manaport's main menu
    */
    public class MainMenuManager : MonoBehaviour {
        public static MainMenuManager Instance = null;

        [SerializeField] private VideoClip[] introSplashes;
        [SerializeField] private VideoPlayer videoPlayer;

        [SerializeField] private PlayableDirector playableDirector;

        [SerializeField] private DimmerHandler blackScreen;
        [SerializeField] private GameObject devSplash;

        public MainMenuState mainMenuState { get; private set; }

        [SerializeField] private GameObject startPrompt;

        private void Awake() {
            if (Instance == null) {
                Instance = this;
            }

            mainMenuState = MainMenuState.Inactive;
            startPrompt.SetActive(false);
            devSplash.SetActive(false);

            videoPlayer.targetTexture.Release();
            int rand = UnityEngine.Random.Range(0, introSplashes.Length);
            videoPlayer.clip = introSplashes[rand];
        }

        private void Start() {
            StartCoroutine(FadeOutBlackScreen());
        }

        IEnumerator FadeOutBlackScreen() {
            yield return new WaitForSeconds(2f);
            blackScreen.FadeOut();
            videoPlayer.Play();
            // StartCoroutine(WaitToPlay());
        }

        IEnumerator WaitToPlay() {
            yield return new WaitForSeconds(2f);
            videoPlayer.Play();
        }

        public void Init() {
            mainMenuState = MainMenuState.Start;
            startPrompt.SetActive(true);
        }

        public void StartPressed(InputAction.CallbackContext context) {
            if (!context.started) return;

            mainMenuState = MainMenuState.Start_Pressed;
            StartCoroutine(WaitToMoveToMainMenu());
        }

        private float startPromptTimer = 0.5f;

        private void Update() {
            if (mainMenuState == MainMenuState.Inactive) {
                if (videoPlayer.isPaused) {
                    devSplash.SetActive(true);
                    playableDirector.Play();
                }
            }else if (mainMenuState == MainMenuState.Start) {
                startPromptTimer -= Time.deltaTime;

                if (startPromptTimer <= 0f) {
                    startPrompt.SetActive(!startPrompt.activeInHierarchy);
                    startPromptTimer = 0.5f;
                }
            }else if (mainMenuState == MainMenuState.Start_Pressed) {
                startPromptTimer -= Time.deltaTime;

                if (startPromptTimer <= 0f) {
                    startPrompt.SetActive(!startPrompt.activeInHierarchy);
                    startPromptTimer = 0.05f;
                }
            }
        }

        IEnumerator WaitToMoveToMainMenu() {
            yield return new WaitForSeconds(1f);
            mainMenuState = MainMenuState.Main_Menu;
            startPrompt.SetActive(false);
        }
    }
}

