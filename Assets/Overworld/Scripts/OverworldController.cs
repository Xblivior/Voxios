using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class OverworldController : MonoBehaviour 
{
	public void LoadDesertLevel()
	{
		SceneManager.LoadScene("Desert");
	}

	public void LoadCityLevel()
	{
		SceneManager.LoadScene("City");
	}

	public void LoadForestLevel()
	{
		SceneManager.LoadScene("Forest");
	}

	public void LoadMainMenu()
	{
		SceneManager.LoadScene("MainMenu");
	}
}
