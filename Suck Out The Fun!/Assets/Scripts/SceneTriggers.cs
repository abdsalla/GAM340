using System.Collections.Generic;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine;

public class SceneTriggers : MonoBehaviour
{
    private GameManager instance;

    public SceneLoader sceneLoader;
    public enum Scenes {MainMenu, World, WinScreen, Exit};
    public Scenes scene = Scenes.MainMenu;
    public Button startButton;
    public Button quitButton;

    private UnityAction sceneSwitcher;
    private UnityAction exiter;


    void Start()
    {
        instance = GameManager.Instance;
        sceneSwitcher += SceneSwitch;
        exiter += sceneLoader.Quitter;

        if (scene == Scenes.Exit && instance.activeScene.name == "MainMenu")
        {
            Button qBtn = quitButton.GetComponent<Button>();
            qBtn.onClick.AddListener(exiter);
        }
        else if (scene == Scenes.World && instance.activeScene.name == "MainMenu")
        {
            Button sBtn = startButton.GetComponent<Button>();
            sBtn.onClick.AddListener(sceneSwitcher);
        }
    }

    void OnEnable()
    {
        exiter = null;
        SceneTriggers quitTrigger = quitButton.gameObject.GetComponent<SceneTriggers>();
        quitTrigger.sceneLoader = SceneLoader.sceneManager;
        exiter += sceneLoader.Quitter;
        Button qBtn = quitButton.GetComponent<Button>();
        qBtn.onClick.AddListener(exiter);
    }

    void OnDisable() { quitButton.onClick.RemoveAllListeners(); }

    public void SceneSwitch()
    {
        switch (scene)
        {
            case Scenes.MainMenu:
                sceneLoader.RunMainMenu();
                break;
            case Scenes.World: 
                sceneLoader.RunWorld();
                break;
            case Scenes.WinScreen:
                sceneLoader.RunWinScreen();
                break;
        }
    }
}