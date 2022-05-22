using System.Collections.Generic;
using DG.Tweening;
using IncommingTileScripts;
using UnityEngine;

namespace GameTIleScripts
{
	public class PlayerDroppedTileHandler : MonoBehaviour
	{
		[SerializeField] private DropableTileLocations dropableTileLocations;
		[SerializeField] private float dropAnimationTime = 0.3f;
		[SerializeField] private TileDroppedOnBoardHandler tileDroppedOnBoardHandler;
		[SerializeField] private IncommingTileSpawnerAndMover incommingTileSpawnerAndMover;
		public Vector2 scaleForKeepPosition = new Vector2(1.1f,1.1f);
		public Vector2 normalScale = new Vector2(1f,1f);
		public static PlayerDroppedTileHandler inst;

		private GameTile _onKeepTile;
	
		private void Awake()
		{
			inst = this;
		}

		public bool IncommingTileDroppedSuccessCheck(GameTile droppedTile, List<Transform> hitObjectList)
		{
			if (hitObjectList.Count > 1) return false; // means occupied slot
			if (hitObjectList.Count == 0) return false; // means dragged nowhere
			if (dropableTileLocations.DroppedOnKeep(hitObjectList[0]) && _onKeepTile != null) return false; // dropped on keep but something is already there
			SuccessfullyDropedTile(droppedTile,hitObjectList[0]);
			return true;
		}

		private void SuccessfullyDropedTile(GameTile droppedTile, Transform transformDropedOn)
		{
			if (dropableTileLocations.DroppedOnKeep(transformDropedOn))
			{
				DropOnKeepTile(droppedTile, transformDropedOn);
			}
			else
			{
				DropOnGameTile(droppedTile, transformDropedOn);
			}
			incommingTileSpawnerAndMover.MoveTilesAndSpawnNewOne();
		}

		private void DropOnGameTile(GameTile droppedTile, Transform transformDropedOn)
		{
			if (droppedTile.isOnKeep)
			{
				incommingTileSpawnerAndMover.skipSpawningKeepTileWasUsed = true;
				droppedTile.isOnKeep = false;
				_onKeepTile = null;
			}
			droppedTile.transform.DOLocalMove(transformDropedOn.localPosition, dropAnimationTime);
			droppedTile.myRectTransform.DOScale(normalScale, dropAnimationTime);
			droppedTile.dragable = false;
			tileDroppedOnBoardHandler.TileDroppedOnBoard(droppedTile,transformDropedOn);
		}

		private void DropOnKeepTile(GameTile droppedTile, Transform transformDropedOn)
		{
			_onKeepTile = droppedTile;
			droppedTile.transform.DOLocalMove(transformDropedOn.localPosition, dropAnimationTime);
			droppedTile.myRectTransform.DOScale(scaleForKeepPosition, dropAnimationTime);
			droppedTile.dragable = true;
			droppedTile.isOnKeep = true;
		}
	}
}
