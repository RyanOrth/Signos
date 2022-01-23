using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    public GameObject panel;
    public Animator animator;
    public GameObject progressBar;
    public GameObject handRender;

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
}
