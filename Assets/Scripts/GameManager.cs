using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	[SerializeField] private GameObject gameCanvas;
	[SerializeField] private GameObject loseCanvas;
	[SerializeField] private TextMeshProUGUI scoreFromGameCanvas;
	[SerializeField] private TextMeshProUGUI scoreOnLoseCanvas;
	
	public void BoardIsFull() // game over
	{
		gameCanvas.SetActive(false);
		loseCanvas.SetActive(true);
		scoreOnLoseCanvas.SetText(scoreFromGameCanvas.text);
	}

	public void RestartGame()
	{
		SceneManager.LoadScene(0);
	}
}
