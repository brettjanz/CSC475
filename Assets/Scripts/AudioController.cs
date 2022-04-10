using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NumSharp;

public class AudioController : MonoBehaviour
{

    private AudioSource src;
    private AudioClip clip;
    
    // Start is called before the first frame update
    void Start()
    {
        src = GetComponent<AudioSource>();
        src.loop = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateAudio(NDArray newSignal)
    {
        clip = Sinusoids.ToAudioClip(newSignal);
        src.clip = clip;
        src.Play();
    }
}
