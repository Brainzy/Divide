using System.Collections;
using System.Collections.Generic;
using IncommingTileScripts;
using UnityEngine;

namespace GameTIleScripts
{
	public class TileDroppedOnBoardHandler : MonoBehaviour
	{
		[SerializeField] private DropableTileLocations dropableTileLocations;
		[SerializeField] private float waitTimeForPossibleCombo = 0.1f;
		[SerializeField] private GameManager gameManager;

		private List<GameTile> tilesOnBoard = new List<GameTile>();
		
		public void TileDroppedOnBoard(GameTile droppedTile, Transform transformDropedOn)
		{
			tilesOnBoard.Add(droppedTile);
			CheckIfBoardIsFull();
			var emptyTileScript = dropableTileLocations.ReturnEmptyTileBasedOnLocation(transformDropedOn.position);
			emptyTileScript.numberOnMySlot = droppedTile;
			emptyTileScript.FindAndDivideNeighbours();
		}

		private void CheckIfBoardIsFull()
		{
			UpdateTilesOnBoard();
			if (tilesOnBoard.Count == 9) StartCoroutine(WaitFramesToCheckBoardIsStillFull());
		}

		private void UpdateTilesOnBoard()
		{
			var listForRemoval = new List<GameTile>();
			for (int i = 0; i < tilesOnBoard.Count; i++)
			{
				if (tilesOnBoard[i] == null) listForRemoval.Add(tilesOnBoard[i]);
			}
			tilesOnBoard.RemoveAll(item => listForRemoval.Contains(item));
		}

		private IEnumerator  WaitFramesToCheckBoardIsStillFull()
		{
			yield return new WaitForSeconds(waitTimeForPossibleCombo);
			UpdateTilesOnBoard();
			if (tilesOnBoard.Count == 9) gameManager.BoardIsFull();
		}
		
	}
}
