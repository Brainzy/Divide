using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace IncommingTileScripts
{
	public class IncommingTileSpawnerAndMover : MonoBehaviour
	{
		[SerializeField] private IncommingTilePositions incommingTilePositions;
		[SerializeField] private Transform incommingPrefab;
		[SerializeField] private Transform gameCanvas;
		[SerializeField] private int minIncommingValue = 2;
		[SerializeField] private int maxIncommingValue = 24;
		[SerializeField] private float animationMovementTime = 0.3f;
		[SerializeField] private Vector3 initialSpawnPosition;
		public Vector2 scaleForFirstTwoPositions = new Vector2(0.4f,0.4f);
		public Vector2 scaleForLastPosition = new Vector2(1,1);
		public bool skipSpawningKeepTileWasUsed;
	
		public List<GameTile> activeIncommingTileScripts = new List<GameTile>();

		private void Start()
		{
			SpawnInitialIncommingTiles();
		}

		private void SpawnInitialIncommingTiles()
		{
			for (int i = 0; i < incommingTilePositions.incommingPositions.Count; i++)
			{
				var spawnedIncommingTile = SpawnIncommingTile(out var spawnedIncommingScript);
				activeIncommingTileScripts.Add(spawnedIncommingScript);
				if (i == incommingTilePositions.incommingPositions.Count - 1) // if last postiion
				{
					spawnedIncommingScript.dragable = true;
					spawnedIncommingTile.localScale = scaleForLastPosition;
				}
				else 	spawnedIncommingTile.localScale = scaleForFirstTwoPositions;
				spawnedIncommingTile.localPosition = incommingTilePositions.incommingPositions[i];
			}
		}

		private Transform SpawnIncommingTile(out GameTile spawnedGame)
		{
			var spawnedIncommingTile = Instantiate(incommingPrefab, gameCanvas);
			spawnedIncommingTile.localPosition = initialSpawnPosition;
			spawnedGame = spawnedIncommingTile.GetComponent<GameTile>();
			var randomValue = Random.Range(minIncommingValue, maxIncommingValue);
			spawnedIncommingTile.name = randomValue.ToString();
			spawnedGame.myValue = randomValue;
			return spawnedIncommingTile;
		}

		public void MoveTilesAndSpawnNewOne()
		{
			if (skipSpawningKeepTileWasUsed)
			{
				skipSpawningKeepTileWasUsed = false;
				return;
			}
			var listForMoving = new List<Transform>();
			activeIncommingTileScripts.Remove(activeIncommingTileScripts[activeIncommingTileScripts.Count - 1]); // last tile is used up
			var nextTile = activeIncommingTileScripts[activeIncommingTileScripts.Count - 1];
			nextTile.myRectTransform.DOScale(scaleForLastPosition, animationMovementTime);
			nextTile.dragable = true;
			var tileBehind = activeIncommingTileScripts[0];
			var spawnedLastTile = SpawnIncommingTile(out var spawnedIncommingScript);
			spawnedLastTile.localScale = scaleForFirstTwoPositions;
			activeIncommingTileScripts.Insert(0,spawnedIncommingScript);
			listForMoving.Add(spawnedLastTile);
			listForMoving.Add(tileBehind.transform);
			listForMoving.Add(nextTile.transform);
			for (int i = 0; i < listForMoving.Count; i++)
			{
				listForMoving[i].DOLocalMove(incommingTilePositions.incommingPositions[i], animationMovementTime);
			}
		}
	}
}
