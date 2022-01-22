using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    public GameObject settingsPanel;

    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void SettingsPanel()
    {
        settingsPanel.SetActive(!settingsPanel.activeSelf);
    }
}
