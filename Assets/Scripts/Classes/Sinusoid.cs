using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sinusoid
{
    private float amplitude;
    private float frequency;
    private GameObject row;
    private float upgradeCost;

    public Sinusoid(float amplitude, float frequency)
    {
        this.amplitude = amplitude;
        this.frequency = frequency;
        this.Row = null;
        this.UpgradeCost = 100f;
    }

    public float Amplitude { get => amplitude; set => amplitude = value; }
    public float Frequency { get => frequency; set => frequency = value; }
    public GameObject Row { get => row; set => row = value; }
    public float UpgradeCost { get => upgradeCost; set => upgradeCost = value; }
}
