using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine;

public class Resume : MonoBehaviour
{
    private GameManager instance;

    public AudioClip click;

    void Start() { instance = GameManager.Instance; }

    public void Click()
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.PlayOneShot(click);
        StartCoroutine(Press());
    }

    public void ResumeWorld()
    {
        instance.UnPause();
    }

    public void OptToWorld()
    {
        instance.sceneLoader.RunWorld();
        instance.UnPause();
    }

    IEnumerator Press()
    {
        yield return new WaitForSeconds(click.length);
    }
}