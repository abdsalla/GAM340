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
    public const int OPTIONS_INDEX = 3;

    private IEnumerator result;
    private GameManager instance;
    private SoundManager mixer;

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

    void Start()
    {
        instance = GameManager.Instance;
        mixer = instance.mixer;
    }

    // The below functions Load our scenes setup in build settings through Index number

    public void RunMainMenu()
    {
        SceneManager.LoadScene(MAIN_MENU_INDEX);
        mixer.StopMusic();
    }

    public void RunWorld()
    {
        SceneManager.LoadScene(WORLD_INDEX);
        mixer.PlayMusic();
    }

    public void RunWinScreen()
    {
        SceneManager.LoadScene(WIN_SCREEN_INDEX);
        mixer.StopMusic();
    }

    public void RunOptions() { SceneManager.LoadSceneAsync(OPTIONS_INDEX); }

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