using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader sceneManager { get; private set; }

    public const int MAIN_MENU_INDEX = 0;
    public const int WORLD_INDEX = 1;
    public const int WIN_SCREEN_INDEX = 2;

    private IEnumerator result;

    void Awake()
    {
        if (sceneManager != null && sceneManager != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            sceneManager = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    // The below functionsn Load our scenes setup in build settings through Index number

    public void RunMainMenu() { SceneManager.LoadScene(MAIN_MENU_INDEX); }

    public void RunWorld() { SceneManager.LoadScene(WORLD_INDEX); }

    public void RunWinScreen() { SceneManager.LoadScene(WIN_SCREEN_INDEX); }

    public void Quitter() { StartCoroutine(Quit()); }

    public IEnumerator Quit()
    {
        if (Application.isEditor)
        {
            Application.Quit(); // Build Quit
            yield return result;
        }     

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Editor Quit
        yield return result;
        #endif
    }
}