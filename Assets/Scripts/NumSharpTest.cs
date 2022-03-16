using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NumSharp;

public class NumSharpTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var test = np.arange(12);
        test += 2;

        Debug.Log(test);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
