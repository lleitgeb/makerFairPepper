using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindDeactivError : MonoBehaviour
{
    private bool previousActiveState;

    void Start()
    {
        previousActiveState = gameObject.activeSelf;
        Debug.Log("prevState: " + previousActiveState);
    }

    private void OnDisable()
    {
        if (gameObject.activeSelf != previousActiveState)
        {
            Debug.Log("z.activeSelf changed to: " + gameObject.activeSelf);
            Debug.Log("Stack Trace:\n" + Environment.StackTrace);
            previousActiveState = gameObject.activeSelf;
        }
    }

    void Update()
    {
        if (gameObject.activeSelf != previousActiveState)
        {
            Debug.Log("z.activeSelf changed to: " + gameObject.activeSelf);
            Debug.Log("Stack Trace:\n" + Environment.StackTrace);
            previousActiveState = gameObject.activeSelf;
        }
    }
}
