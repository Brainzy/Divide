using IncommingTileScripts;
using UnityEngine;

namespace GameTIleScripts
{
	public class TileDroppedOnBoardHandler : MonoBehaviour
	{
		[SerializeField] private DropableTileLocations dropableTileLocations;
		public void TileDroppedOnBoard(GameTile droppedTile, Transform transformDropedOn)
		{
			var emptyTileScript = dropableTileLocations.ReturnEmptyTileBasedOnLocation(transformDropedOn.position);
			emptyTileScript.numberOnMySlot = droppedTile;
			emptyTileScript.FindAndDivideNeighbours();
		}
	}
}
