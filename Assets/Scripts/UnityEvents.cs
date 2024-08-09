using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UnityEvents : MonoBehaviour
{
    public UnityEvent onenable, ondisable;

    private void OnEnable()
    {
        if (onenable != null)
        {
            onenable?.Invoke();
        }

    }

    private void OnDisable()
    {
        if (ondisable != null)
        {
            ondisable?.Invoke();
        }
    }
}
