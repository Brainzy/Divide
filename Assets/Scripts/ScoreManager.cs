using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI text;

	private int currentScore;
	public void AddScore(int amount)
	{
		currentScore += amount;
		text.SetText(currentScore.ToString());
	}	
}
