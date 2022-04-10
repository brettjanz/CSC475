using UnityEngine;
using NumSharp;

public static class Sinusoids
{

    // Creates a sinusoid and returns a numpy array containing the data
    public static NDArray CreateSinusoid(float frequency = 440f, float duration = 1f, float samplingRate = 44100f, float amplitude = 1f, float phase = 0f)
    {
        NDArray t = np.linspace(0, duration, (int)(samplingRate * duration));
        NDArray data = amplitude * np.sin(2 * np.pi * frequency * t + phase);
        return data;
    }

    // Creates a sinusoid and returns a numpy array containing the data
    public static NDArray CreateSinusoid(out NDArray time, float frequency = 440f, float duration = 1f, float samplingRate = 44100f, float amplitude = 1f, float phase = 0f)
    {
        NDArray t = np.linspace(0, duration, (int)(samplingRate * duration));
        time = t;
        NDArray data = amplitude * np.sin(2 * np.pi * frequency * t + phase);
        return data;
    }

    // Converts a numpy array of samples into an AudioClip that a unity AudioSource can use
    public static AudioClip ToAudioClip(NDArray data, string name = "Sinusoid", float duration = 1f, float samplingRate = 44100f)
    {
        // Convert from NDArray to regular float[] array
        float[] samples = data.ToArray<float>();

        // Use Unity's AudioClip constructor
        AudioClip audioClip = AudioClip.Create(name, (int)(samplingRate * duration), 1, (int)samplingRate, false);

        // Set the AudioClip's data to the array of samples we created
        audioClip.SetData(samples, 0);

        return audioClip;
    }
}
