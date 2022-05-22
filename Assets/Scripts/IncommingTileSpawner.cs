using System.Collections.Generic;
using UnityEngine;

public class IncommingTileSpawner : MonoBehaviour
{
	[SerializeField] private IncommingTilePositions incommingTilePositions;
	[SerializeField] private Transform incommingPrefab;
	[SerializeField] private Transform gameCanvas;
	[SerializeField] private int minIncommingValue = 2;
	[SerializeField] private int maxIncommingValue = 24;
	public Vector2 scaleForFirstTwoPositions = new Vector2(40,40);
	public Vector2 scaleForLastPosition = new Vector2(100,100);
	public Vector2 scaleForKeepPosition = new Vector2(110,110);

	public List<IncommingTileScript> incommingTileScripts = new List<IncommingTileScript>();

	private void Start()
	{
		SpawnInitialIncommingTiles();
	}

	private void SpawnInitialIncommingTiles()
	{
		for (int i = 0; i < incommingTilePositions.incommingPositions.Count; i++)
		{
			var spawnedIncommingTile = Instantiate(incommingPrefab,gameCanvas);
			spawnedIncommingTile.localPosition = incommingTilePositions.incommingPositions[i];
			var spawnedIncommingScript = spawnedIncommingTile.GetComponent<IncommingTileScript>();
			incommingTileScripts.Add(spawnedIncommingScript);
			var randomValue= Random.Range(minIncommingValue, maxIncommingValue);
			spawnedIncommingScript.myValue = randomValue;
			spawnedIncommingScript.myText.SetText(randomValue.ToString());
			if (i == incommingTilePositions.incommingPositions.Count - 1) // if last postiion
			{
				spawnedIncommingScript.myRectTransform.sizeDelta = scaleForLastPosition;
				spawnedIncommingScript.dragable = true;
			} 
		}
	}
	
	
	
}
