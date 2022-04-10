using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sinusoid
{
    private float amplitude;
    private float frequency;

    public Sinusoid(float amplitude, float frequency)
    {
        this.amplitude = amplitude;
        this.frequency = frequency;
    }

    public float Amplitude { get => amplitude; set => amplitude = value; }
    public float Frequency { get => frequency; set => frequency = value; }
}
