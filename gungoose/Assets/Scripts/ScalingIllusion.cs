using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalingIllusion : MonoBehaviour
{
    GameObject scalingTarget;

    [SerializeField] float scalingSize;
    float minScale;
    //float maxScale;

    void Start()
    {
        scalingTarget = GameObject.FindGameObjectWithTag("ScalingTarget");

        minScale = 0.5f;
        //maxScale = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Mathf.Abs(scalingTarget.transform.position.y - transform.position.y);
        float newScale = Mathf.Max(scalingSize / distance, minScale);

        // Set the new size
        transform.localScale = new Vector3(newScale, newScale, 1f);
    }
}
