using System.Collections.Generic;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager masterMixer { get; private set; }

    [SerializeField] private AudioMixer mixer;
    [SerializeField] private AudioSource source;


    void Awake()
    {
        if (masterMixer != null && masterMixer != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            masterMixer = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    void Update() { }

    public void AdjustMasterVolume(float newVol) { mixer.SetFloat("MasterVolume", newVol); }

    public void AdjustUIVolume(float newVol) { mixer.SetFloat("UIVolume", newVol); }

    public void AdjustAmbientVolume(float newVol) { mixer.SetFloat("AmbientVolume", newVol); }

    public void AdjustCombatVolume(float newVol) { mixer.SetFloat("CombatVolume", newVol); }

    public void AdjustPickupVolume(float newVol) { mixer.SetFloat("PickupVolume", newVol); }

    public void PlayMusic() { source.Play(); }

    public void StopMusic() { source.Stop(); }
}