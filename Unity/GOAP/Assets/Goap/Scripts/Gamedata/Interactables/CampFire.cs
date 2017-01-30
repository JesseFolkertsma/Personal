using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampFire : Interactable {

    public float liveTime = 30f;
    public float timeToDeath;
    float endTime;

    void Start()
    {
        endTime = Time.time + liveTime;
    }

	void Update () {
        IUpdate();
        if (GetLiveTime())
        {
            Destroy(gameObject);
        }
	}

    bool GetLiveTime()
    {
        timeToDeath = endTime - Time.time;
        if(timeToDeath < 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
