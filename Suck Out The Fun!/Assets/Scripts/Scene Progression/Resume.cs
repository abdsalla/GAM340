using System.Collections.Generic;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine;

public class Resume : MonoBehaviour
{
    [SerializeField] private Button resumeBtn;
    private GameManager instance;
    private UnityAction resume;

    public AudioClip click;

    void Start()
    {
        instance = GameManager.Instance;
        resume += instance.UnPause;
        resumeBtn.onClick.AddListener(instance.UnPause);
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