using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NumSharp;
using TMPro;

public class Game : MonoBehaviour
{
    // Plotting
    public GameObject plot;
    private Plotter plotter;

    // Audio
    public GameObject audioObject;
    private AudioController audioController;

    // Signal
    private List<Sinusoid> sinusoids;
    private NDArray time;

    // Incremental
    private float cash;

    // UI
    public TextMeshProUGUI cashDisplay;

    private void Awake()
    {
        plotter = plot.GetComponent<Plotter>();
        audioController = audioObject.GetComponent<AudioController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        cash = 0;
        sinusoids = new List<Sinusoid>();
        AddSinusoid(new Sinusoid(1f, 440f));
    }

    // Update is called once per frame
    void Update()
    {
        cash += calculateCashPerSecond() * Time.deltaTime;

        updateDisplay();
    }

    public void AddSinusoid(Sinusoid newSinusoid)
    {
        sinusoids.Add(newSinusoid);
        Debug.Log("Added sinusoid: f=" + newSinusoid.Frequency + " A=" + newSinusoid.Amplitude);
        updateSignal();
    }

    public void Button_AddSinusoid()
    {
        int n = sinusoids.Count;
        AddSinusoid(new Sinusoid(1f, 440f + (220f * n)));
    }

    private void updateSignal()
    {
        // Clear result down to zeros (Assuming length of all sinusoid arrays is 44100 samples)
        NDArray result = np.zeros(44100);

        // Add every active sinusoid to the signal
        foreach (Sinusoid s in sinusoids)
        {
            NDArray temp = Sinusoids.CreateSinusoid(out time, s.Frequency, s.Amplitude);
            result += temp;
        }

        // Normalize to [-1, 1]
        result = (2 * (result - np.min(result)) / (np.max(result) - np.min(result))) - 1;

        // Set display
        plotter.data = result;
        plotter.time = time;

        // Set audio
        audioController.updateAudio(result);
    }

    private float calculateCashPerSecond()
    {
        float result = 0;
        foreach (Sinusoid s in sinusoids)
        {
            result += s.Frequency * s.Amplitude;
        }

        return result;
    }

    private void updateDisplay()
    {
        cashDisplay.text = "Cash: " + cash.ToString("G3");
    }
}
