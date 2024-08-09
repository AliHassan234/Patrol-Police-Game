using System.Collections;
using System.Collections.Generic;
using EmeraldAI;
using UnityEngine;
using UnityEngine.AI;

public class characterstopper : MonoBehaviour
{
    //private void OnCollisionEnter(Collision collision)
    //{
    //    Debug.LogError("Collision Enter Working");
    //    if (collision.gameObject.tag == "Randomperson")
    //    {
    //        Debug.LogError("Stopping Players On Collision");
    //        GetComponent<NavMeshAgent>().enabled = false;
    //        GetComponent<EmeraldAISystem>().enabled = false;
    //        GetComponent<Animator>().Play("Idle");
    //    }
    //}

    //private void OnCollisionExit(Collision collision)
    //{
    //    GetComponent<NavMeshAgent>().enabled = true;
    //    GetComponent<EmeraldAISystem>().enabled = true;
    //    GetComponent<Animator>().Play("Movement");

    //}

    public bool isplayerarrested=false;



    public void StopPlayer()
    {
        Debug.LogError("Stopping Players");
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<EmeraldAISystem>().enabled = false;
        GetComponent<Animator>().Play("Idle");
        RaycastFromCamera.issomeplayerstopped = true;
        //Stopping all Since they Passed from Inside
        //foreach (var item in GameManager.Instance.AllCharacters)
        //{
        //    item.GetComponent<NavMeshAgent>().enabled = false;

        //    item.GetComponent<EmeraldAISystem>().enabled = false;
        //    item.GetComponent<Animator>().Play("Idle");
        //}
    }

    public void ResumePlayer()
    {
        if (isplayerarrested)
        {
            RaycastFromCamera.issomeplayerstopped = false;
            this.gameObject.tag = "Untagged";
            //this.gameObject.GetComponent<BoxCollider>().enabled = false;
            //RaycastFromCamera.Instance.Hand.SetActive(false);
            
            Invoke(nameof(RestPlayerStateAndStartsRun),40f);
            return;
        }
        GetComponent<EmeraldAISystem>().enabled = true;
        RaycastFromCamera.issomeplayerstopped = false;
        GetComponent<NavMeshAgent>().enabled = true;
        GetComponent<Animator>().Play("LeftTurn");

        Invoke(nameof(enablenavmeshagent), 1f);
        //Resuming all Since they Passed from Inside

        //foreach (var item in GameManager.Instance.AllCharacters)
        //{
        //    item.GetComponent<EmeraldAISystem>().enabled = true;
        //    item.GetComponent<NavMeshAgent>().enabled = true;

        //    item.GetComponent<Animator>().Play("Movement");
        //}
    }

    public void enablenavmeshagent()
    {
        GetComponent<Animator>().Play("Movement");

    }

    public void RestPlayerStateAndStartsRun()
    {
        this.gameObject.tag = "Emerald AI";
        //this.gameObject.GetComponent<BoxCollider>().enabled = true;

        GetComponent<EmeraldAISystem>().enabled = true;
        //RaycastFromCamera.issomeplayerstopped = false;
        GetComponent<NavMeshAgent>().enabled = true;
        GetComponent<Animator>().Play("LeftTurn");
        this.transform.Find("Bip01").transform.Find("Handcuff").transform.gameObject.SetActive(false);

        Invoke(nameof(enablenavmeshagent), 1f);
    }
}
