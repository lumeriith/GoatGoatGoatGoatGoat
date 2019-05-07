using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GoatBleater : MonoBehaviour {
    public List<AudioClip> bleats = new List<AudioClip>();
    public AudioClip slurp;
    private AudioSource audioSource;
    public bool isSlurping
    {
        get
        {
            return audioSource.clip == slurp && audioSource.isPlaying;
        }
    }
    private void Awake()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.loop = false;
        audioSource.volume = .7f;
    }

    public void Bleat()
    {
        audioSource.clip = bleats[Random.Range(0, bleats.Count)];
        audioSource.loop = false;
        audioSource.volume = .7f;
        audioSource.pitch = Random.Range(0.8f, 1.1f);

        audioSource.Play();
    }

    public void Bleat(float skipRatio)
    {
        audioSource.clip = bleats[Random.Range(0, bleats.Count)];
        audioSource.loop = false;
        audioSource.volume = .7f;
        audioSource.pitch = Random.Range(0.8f, 1.1f);
        audioSource.time = audioSource.clip.length * skipRatio;
        audioSource.Play();
    }

    public void StartSlurp()
    {
        if (isSlurping) return;
        audioSource.clip = slurp;
        audioSource.loop = true;
        audioSource.volume = .6f;
        audioSource.pitch = Random.Range(0.8f, 1.0f);
        audioSource.time = 0;
        audioSource.Play();
    }

    public void StopSlurp()
    {
        if(isSlurping)
        {
            audioSource.Stop();
        }
        
    }


}
