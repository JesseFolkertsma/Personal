using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cave : Interactable {
	void Update () {
        IUpdate();
	}

    void OnTriggerEnter(Collider col)
    {
        if(col.transform.root.tag == "Caveman")
        {
            col.transform.root.GetComponent<Caveman>().sheltered = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.transform.root.tag == "Caveman")
        {
            col.transform.root.GetComponent<Caveman>().sheltered = false;
        }
    }
}
