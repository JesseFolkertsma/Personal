using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {

    public bool isFound = false;
    public float findingDistance = 20f;
    Caveman caveman;

    void Awake()
    {
        caveman = FindObjectOfType<Caveman>();
    }

    public void IUpdate()
    {
        if (!isFound)
        {
            if(Vector3.Distance(transform.position, caveman.transform.position) < findingDistance)
            {
                isFound = true;
            }
        }
    }
}
