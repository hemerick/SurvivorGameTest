using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    private static SoundPlayer instance;

    [SerializeField] private AudioSource deathAudio;

    float initialPitch;

    //PERMET AUX AUTRES ENTIT�S D'ALLER CHERCHER R�F�RENCE AU SOUNDPLAYER
    public static SoundPlayer GetInstance() => instance;

    private void Awake()
    {
        instance = this;
        initialPitch = deathAudio.pitch;
    }

    //JOUE LE SON DEATH DE BASE
    public void PlayDeathAudio()
    {
        deathAudio.pitch= initialPitch;
        deathAudio.Play();
    }

    //JOUE LE SON DEATH, PLUS AIGUE
    public void PlayHurtAudio()
    {
        deathAudio.pitch = 1.5f;
        deathAudio.Play();
    }

}