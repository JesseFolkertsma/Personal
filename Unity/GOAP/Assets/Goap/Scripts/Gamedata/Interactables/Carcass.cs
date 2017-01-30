using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carcass : Interactable {

    public int uses = 3;

    void Update()
    {
        IUpdate();
    }

    public void Loot()
    {
        transform.localScale /= 1.5f;
        uses--;

        if(uses < 1)
        {
            Destroy(gameObject);
        }
    }
}
