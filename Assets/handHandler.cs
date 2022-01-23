using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        transform.Find("0").gameObject.SetActive(false);
        transform.Find("1").gameObject.SetActive(false);
        transform.Find("2").gameObject.SetActive(false);
        transform.Find("3").gameObject.SetActive(false);
        string handNumber = System.IO.File.ReadAllText(Application.persistentDataPath + "/handType.txt");
        transform.Find(handNumber).gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
