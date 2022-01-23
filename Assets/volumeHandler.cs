using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class volumeHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (System.IO.File.Exists(Application.persistentDataPath + "/volumeLevel.txt"))
        {
            gameObject.GetComponent<AudioSource>().volume = Convert.ToSingle(System.IO.File.ReadAllText(Application.persistentDataPath + "/volumeLevel.txt"));
        }
        

    // Update is called once per frame
    void Update()
    {
        
    }
}
