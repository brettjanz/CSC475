using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NumSharp;

public class Plotter : MonoBehaviour
{
    // Data holders
    public NDArray data;
    public NDArray time;

    // Script reference
    private SimplestPlot script;

    // Slicing helpers
    public int sliceSize = 100;
    private int count;
    private int indexStart;
    private int indexEnd;
    private string slice;

    private void Awake()
    {
        script = GetComponent<SimplestPlot>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (data == null)
        {
            Debug.Log("No Data");
            data = np.zeros(44100);
            time = np.zeros(44100);
        }

        // Display config
        script.ShowWarnings = true;
        script.SetResolution(new Vector2(600, 600));
        script.BackGroundColor = new Color(0.1f, 0.1f, 0.1f, 0.4f);
        script.TextColor = Color.white;
        script.SeriesPlotY.Add(new SimplestPlot.SeriesClass());
        script.SeriesPlotY[0].MyColor = Color.white;

        // Set initial values
        slice = ":" + sliceSize.ToString();
        script.SeriesPlotX = time[slice].astype(NPTypeCode.Float).ToArray<float>();
        script.SeriesPlotY[0].YValues = data[slice].astype(NPTypeCode.Float).ToArray<float>();

        // Initialize counter
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // Update counter
        count++;
        if (count + sliceSize > data.size)
        {
            count = 0;
        }

        // Set array slice string
        indexStart = count;
        indexEnd = count + sliceSize;
        slice = indexStart.ToString() + ":" + indexEnd.ToString();

        // Update values
        script.SeriesPlotX = time[slice].ToArray<float>();
        script.SeriesPlotY[0].YValues = data[slice].astype(NPTypeCode.Float).ToArray<float>();
        script.UpdatePlot();
    }
}
