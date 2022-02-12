using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    [SerializeField]
    GameObject nodePrefab;

    [SerializeField, Range(10, 100)]
    int resolution = 10;

    [SerializeField, Range(1, 1000)]
    int frequency = 1;

    [SerializeField, Range(1, 5)]
    int amplitude = 1;

    [SerializeField]
    int domainStart = 0;

    [SerializeField]
    int domainEnd = 10;

    Transform[] points;

    private void Awake()
    {
        float step = (domainEnd - domainStart) / (float)resolution;
        Vector2 position = Vector2.zero;
        Vector2 scale = Vector2.one * step;

        points = new Transform[resolution];
        for (int i = 0; i < points.Length; i++)
        {
            Transform point = points[i] = Instantiate(nodePrefab).transform;
            position.x = ((i + 0.5f) * step) + domainStart;
            point.localPosition = position;
            point.localScale = scale;

            point.SetParent(transform, false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float time = Time.time;
        for (int i = 0; i < points.Length; i++)
        {
            Transform point = points[i];
            Vector2 position = point.localPosition;
            position.y = amplitude * Mathf.Sin(Mathf.PI * frequency * (position.x + time));
            point.localPosition = position;
        }
    }
}
