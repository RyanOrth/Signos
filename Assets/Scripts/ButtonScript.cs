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

    }

    public void SkeletonHandSelect()
    {

    }

    public void GlowHandSelect()
    {

    }

    public void GhostHandSelect()
    {

    }
}
