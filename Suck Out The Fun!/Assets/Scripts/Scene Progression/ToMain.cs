using System.Collections.Generic;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine;

public class ToMain : MonoBehaviour
{   
    [SerializeField] private Button mainReturn;
    private SceneLoader sceneLoader;
    private UnityAction toMain;

    public AudioClip click;

    void Start()
    {
        sceneLoader = SceneLoader.sceneManager;
        toMain += sceneLoader.RunMainMenu;
        mainReturn.onClick.AddListener(sceneLoader.RunMainMenu);
    }

    public void Click()
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.PlayOneShot(click);
        StartCoroutine(Press());
    }

    IEnumerator Press()
    {
        yield return new WaitForSeconds(click.length);
    }
}