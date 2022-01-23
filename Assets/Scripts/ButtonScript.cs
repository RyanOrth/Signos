using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    public GameObject panel;
    public Animator animator;
    public GameObject progressBar;
    public GameObject handRender;
    public GameObject handSettings;

    public void StartGame()
    { 
        SceneManager.LoadScene("Prefab Making");
    }

    public void TogglePanel()
    {
        panel.SetActive(!panel.activeSelf);
    }

    public void MenuReturn()
    {
        SceneManager.LoadScene("Menu");
    }

    public void CloseModeSelect()
    {
        transform.parent.gameObject.SetActive(false);
        progressBar.SetActive(!progressBar.activeSelf);
        handRender.SetActive(!handRender.activeSelf);
    }

    public void ChangeHandSelect()
    {
        handSettings.SetActive(!handSettings.activeSelf);
    }

    public void LowPolyHandSelect()
    {
        System.IO.File.WriteAllText(Application.persistentDataPath + "/handType.txt", "0");
    }

    public void SkeletonHandSelect()
    {
        System.IO.File.WriteAllText(Application.persistentDataPath + "/handType.txt", "1");
    }

    public void GlowHandSelect()
    {
        System.IO.File.WriteAllText(Application.persistentDataPath + "/handType.txt", "2");
    }

    public void GhostHandSelect()
    {
        System.IO.File.WriteAllText(Application.persistentDataPath + "/handType.txt", "3");
    }
}
