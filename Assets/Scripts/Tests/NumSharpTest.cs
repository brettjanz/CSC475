using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NumSharp;

public class NumSharpTest : MonoBehaviour
{
    // Frequencies for different notes
    float c5_freq = 523f;
    float e5_freq = 659f;
    float f5_freq = 698f;
    float g5_freq = 784f;
    float a5_freq = 880f;
    float b5_freq = 988f;
    float c6_freq = 1047f;
    float d6_freq = 1175f;
    float e6_freq = 1319f;

    // Start is called before the first frame update
    void Start()
    {
        // Convert frequencies to numpy arrays
        NDArray c5_data = Sinusoids.CreateSinusoid(c5_freq);
        NDArray e5_data = Sinusoids.CreateSinusoid(e5_freq);
        NDArray f5_data = Sinusoids.CreateSinusoid(f5_freq);
        NDArray g5_data = Sinusoids.CreateSinusoid(g5_freq);
        NDArray a5_data = Sinusoids.CreateSinusoid(a5_freq);
        NDArray b5_data = Sinusoids.CreateSinusoid(b5_freq);
        NDArray c6_data = Sinusoids.CreateSinusoid(c6_freq);
        NDArray d6_data = Sinusoids.CreateSinusoid(d6_freq);
        NDArray e6_data = Sinusoids.CreateSinusoid(e6_freq);

        // Add individual notes to form chords
        NDArray c_chord = c5_data + e5_data + g5_data;
        NDArray g_chord = g5_data + b5_data + d6_data;
        NDArray aM_chord = a5_data + c6_data + e6_data;
        NDArray f_chord = f5_data + a5_data + c6_data;

        // Concatenate the chords into a (ugly sounding) chord progression
        NDArray melody = np.hstack(c_chord, g_chord, aM_chord, f_chord);

        // Convert this numpy array to a 4 second audio clip unity can use
        AudioClip audioClip = Sinusoids.ToAudioClip(melody, "My Melody", 4);
        
        // Set the audio source's clip to the clip we just generated
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClip;

        // Play the clip
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
