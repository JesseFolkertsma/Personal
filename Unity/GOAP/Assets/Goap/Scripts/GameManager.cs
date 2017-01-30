using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance;
    public List<Animal> animalsInScene;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public KeyValuePair<string,object>[] GetRealWorldData(Transform _agent)
    {
        List<KeyValuePair<string, object>> List = new List<KeyValuePair<string, object>>();

        //Check for fires
        List.Add(new KeyValuePair<string, object> ("FireExists", FindObjectOfType<CampFire>() != null));

        //Check for carcasses
        List.Add(new KeyValuePair<string, object> ("CarcassExists", FindObjectOfType<Carcass>() != null));

        return List.ToArray();
    }
}
