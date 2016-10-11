using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour 
{

	public void NewGame()
	{
		SceneManager.LoadScene ("NewCity");
	}

	public void LoadGame()
	{

	}

	public void Options()
	{

	}
		
	public void Credits()
	{

	}
		
	public void QuitGame()
	{
		Application.Quit();
	}
}
