using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NumSharp;

public class Game : MonoBehaviour
{
    // Plotting
    public GameObject plot;
    private Plotter plotter;

    // Signal
    private List<Sinusoid> sinusoids;
    private NDArray signal;
    private NDArray time;

    // Start is called before the first frame update
    void Start()
    {
        sinusoids = new List<Sinusoid>();
        plotter = plot.GetComponent<Plotter>();
        AddSinusoid(new Sinusoid(1f, 440f));
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddSinusoid(Sinusoid newSinusoid)
    {
        sinusoids.Add(newSinusoid);
        Debug.Log("Added sinusoid: f=" + newSinusoid.Frequency + " A=" + newSinusoid.Amplitude);
        updateSignal();
    }

    public void updateSignal()
    {
        // Clear result down to zeros (Assuming length of all sinusoid arrays is 44100 samples)
        NDArray result = np.zeros(44100);

        // Add every active sinusoid to the signal
        foreach (Sinusoid s in sinusoids)
        {
            NDArray temp = Sinusoids.CreateSinusoid(out time, s.Frequency, s.Amplitude);
            result += temp;
        }

        // Set
        plotter.data = result;
        plotter.time = time;
    }

    public void Button_AddSinusoid()
    {
        int n = sinusoids.Count;
        AddSinusoid(new Sinusoid((1f/(n+1f)), 440f + (220f * n)));
    }

}