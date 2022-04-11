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
    private float sinusoidCost;

    // UI
    public TextMeshProUGUI cashDisplay;
    public GameObject sinusoidDisplay;
    public GameObject rowPrefab;
    public TextMeshProUGUI addSinusoidButton;

    private void Awake()
    {
        plotter = plot.GetComponent<Plotter>();
        audioController = audioObject.GetComponent<AudioController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        cash = 0;
        sinusoidCost = 10000f;
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
        updateSignal();
    }

    public void Button_AddSinusoid()
    {
        if (cash > sinusoidCost)
        {
            sinusoidCost *= 10;
            int n = sinusoids.Count;
            AddSinusoid(new Sinusoid(1f, 440f + (220f * n)));
        }
    }

    public void upgradeSinusoid(int n)
    {
        Sinusoid s = sinusoids[n - 1];
        if (cash > s.UpgradeCost)
        {
            // Cost multiplier = 1 + level * 0.25
            float costMult = 1 + (s.Amplitude * 0.25f);
            s.Amplitude += 1;            
            s.UpgradeCost *= costMult;
            updateSignal();
        }
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

        return result * sinusoids.Count;
    }

    private void updateDisplay()
    {
        // Update cash
        cashDisplay.text = "Cash: " + cash.ToString("G3");

        // Update sinusoid cost
        addSinusoidButton.text = "Add Sinusoid: $" + sinusoidCost.ToString("G3");
        
        // Update sinusoid rows
        foreach (Sinusoid s in sinusoids)
        {
            // Add a row if there is a new sinusoid
            if (s.Row == null)
            {
                int number = sinusoids.Count;
                GameObject row = Instantiate(rowPrefab, sinusoidDisplay.transform);
                TextMeshProUGUI sinusoid = getAttributeText(row, "Sinusoid");
                sinusoid.text = number.ToString();
                TextMeshProUGUI frequency = getAttributeText(row, "Frequency");
                frequency.text = s.Frequency.ToString("G3");
                TextMeshProUGUI amplitude = getAttributeText(row, "Amplitude");
                amplitude.text = s.Amplitude.ToString("G3");
                Button upgrade = row.transform.Find("Upgrade").GetComponent<Button>();
                upgrade.onClick.AddListener(() => upgradeSinusoid(number));
                TextMeshProUGUI upgradeText = getAttributeText(row, "Upgrade");
                upgradeText.text = "Upgrade: $" + s.UpgradeCost;

                s.Row = row;
            }
            // Otherwise update the amplitude and upgrade costs
            else
            {
                TextMeshProUGUI amplitude = getAttributeText(s.Row, "Amplitude");
                amplitude.text = s.Amplitude.ToString("G3");
                TextMeshProUGUI upgradeText = getAttributeText(s.Row, "Upgrade");
                upgradeText.text = "Upgrade: $" + s.UpgradeCost.ToString("G3");
            }            
        }
    }

    private TextMeshProUGUI getAttributeText(GameObject row, string attr)
    {
        if (attr == "Upgrade")
        {
            return row.transform.Find(attr).GetComponentInChildren<TextMeshProUGUI>();
        }
        else
        {
            return row.transform.Find(attr).GetComponent<TextMeshProUGUI>();
        }
    }
}
