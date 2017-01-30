using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : Interactable {

    public int foodAmount = 1;
    public bool isDead = false;
    public float moveSpeed;
    
    public void Die()
    {
        GetComponent<Renderer>().material.color = Color.red;
        isDead = true;
        GameObject prefab = Resources.Load<GameObject>("Carcass");
        Instantiate(prefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }
    void Update()
    {
        IUpdate();
    }
}
