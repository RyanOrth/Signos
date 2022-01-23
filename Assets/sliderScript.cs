using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sliderScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveVolumeLevel()
    {
        System.IO.File.WriteAllText(Application.persistentDataPath + "/volumeLevel.txt", "" + gameObject.GetComponent<Slider>().value);
    }
}
