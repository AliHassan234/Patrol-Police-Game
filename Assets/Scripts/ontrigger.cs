using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class ontrigger : MonoBehaviour
{
    public UnityEvent ontriggerenter,ontriggerexit;


    private void OnTriggerEnter(Collider other)
    { 
        Debug.LogError("Outside player");

        if (other.gameObject.tag == "Player")
        {
            Debug.LogError("Inside player");
            ontriggerenter?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
    }
}
