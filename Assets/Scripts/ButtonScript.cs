using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    public GameObject panel;

    public void StartGame()
    { 
        SceneManager.LoadScene("SampleScene");
    }

    public void TogglePanel()
    {
        panel.SetActive(!panel.activeSelf);
    }
}