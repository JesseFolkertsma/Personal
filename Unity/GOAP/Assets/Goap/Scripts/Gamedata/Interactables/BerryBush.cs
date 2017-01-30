using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BerryBush : Interactable {

    int foodAmount = 1;
    bool hasBerries = true;

    [SerializeField]
    GameObject berries;

    public bool CanHarvest
    {
        get
        {
            if (foodAmount > 0 && hasBerries)
                return true;
            else
                return false;
        }
    }

    void Update()
    {
        IUpdate();
    }

    public void Harvest()
    {
        hasBerries = false;
        berries.SetActive(false);
    }
}
