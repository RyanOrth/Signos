using System.Collections;
using System.Collections.Generic;
using Leap;
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
	public Button polyHand;
	public Button skeletonHand;
	public Button glowHand;
	public Button ghostHand;


	public void StartGame()
	{
		SceneManager.LoadScene("Prefab Making");
		// this.LowPolyHandSelect();
	}

	public void ExitGame()
	{
		Application.Quit(-1);
	}

	public void TogglePanel()
	{
		panel.SetActive(!panel.activeSelf);
	}

	public void MenuReturn()
	{
		SceneManager.LoadScene("Menu");
	}

	public void CloseModeSelect(bool speedMode)
	{
		transform.parent.gameObject.SetActive(false);
		progressBar.SetActive(!progressBar.activeSelf);
		handRender.SetActive(!handRender.activeSelf);
		if (speedMode)
		{
			panel.GetComponent<LessonHandler>().speedMode = true;
		}
	}

	public void ChangeHandSelect()
	{
		handSettings.SetActive(!handSettings.activeSelf);
	}

	public void LowPolyHandSelect()
	{
		System.IO.File.WriteAllText(Application.persistentDataPath + "/handType.txt", "0");
		polyHand.interactable = false;
		skeletonHand.interactable = true;
		glowHand.interactable = true;
		ghostHand.interactable = true;
	}

	public void SkeletonHandSelect()
	{
		System.IO.File.WriteAllText(Application.persistentDataPath + "/handType.txt", "1");
		polyHand.interactable = true;
		skeletonHand.interactable = false;
		glowHand.interactable = true;
		ghostHand.interactable = true;
	}

	public void GlowHandSelect()
	{
		System.IO.File.WriteAllText(Application.persistentDataPath + "/handType.txt", "2");
		polyHand.interactable = true;
		skeletonHand.interactable = true;
		glowHand.interactable = false;
		ghostHand.interactable = true;
	}

	public void GhostHandSelect()
	{
		System.IO.File.WriteAllText(Application.persistentDataPath + "/handType.txt", "3");
		polyHand.interactable = true;
		skeletonHand.interactable = true;
		glowHand.interactable = true;
		ghostHand.interactable = false;
	}

	public void SkipLetter()
	{
		panel.GetComponent<LessonHandler>().currentLesson++;
		panel.GetComponent<LessonHandler>().slider.value = 0;
		panel.GetComponent<LessonHandler>().animator.SetInteger("number of lessons", panel.GetComponent<LessonHandler>().currentLesson.indexOf());
		panel.GetComponent<LessonHandler>().textBox.GetComponent<Text>().text = panel.GetComponent<LessonHandler>().currentLesson.ToString();
		if (panel.GetComponent<LessonHandler>().currentLesson.indexOf() > 4)
		{
			panel.GetComponent<LessonHandler>().completedPanel.SetActive(true);
		}
	}
}
