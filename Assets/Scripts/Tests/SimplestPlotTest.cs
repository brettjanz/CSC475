using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NumSharp;

public class SimplestPlotTest : MonoBehaviour
{
    private SimplestPlot script;
    private NDArray tempX;
    private NDArray sinusoid;
    private NDArray sinusoid1;
    private NDArray sinusoid2;
    private float[] yValues;
    private float[] xValues;
    private int count;
    private int indexStart;
    private int indexEnd;
    private string slice;
    // Start is called before the first frame update
    void Start()
    {
        script = GetComponent<SimplestPlot>();
        script.ShowWarnings = true;
        script.SetResolution(new Vector2(600, 600));
        script.BackGroundColor = new Color(0.1f, 0.1f, 0.1f, 0.4f);
        script.TextColor = Color.yellow;
        script.SeriesPlotY.Add(new SimplestPlot.SeriesClass());

        sinusoid1 = Sinusoids.CreateSinusoid(out tempX, 440f);
        sinusoid2 = Sinusoids.CreateSinusoid(880f);
        sinusoid = sinusoid1 + sinusoid2;

        // Normalize
        sinusoid = sinusoid / (sinusoid.max() - 1);

        xValues = tempX[":50"].ToArray<float>();
        yValues = sinusoid[":50"].ToArray<float>();
        script.SeriesPlotX = xValues;
        script.SeriesPlotY[0].YValues = yValues;
        script.SeriesPlotY[0].MyColor = Color.white;

        count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        count++;
        if (count + 50 > sinusoid.size)
        {
            count = 0;
        }

        indexStart = count;
        indexEnd = count + 50;
        slice = indexStart.ToString() + ":" + indexEnd.ToString();
        xValues = tempX[slice].ToArray<float>();
        yValues = sinusoid[slice].ToArray<float>();
        script.SeriesPlotX = xValues;
        script.SeriesPlotY[0].YValues = yValues;
        script.UpdatePlot();
    }
}
