using System.Linq;
using UnityEngine;

namespace GameTIleScripts
{
	public class DropableTileLocations : MonoBehaviour
	{
		public GameBoardSlotDivideActivator[] emptyTileScripts;
		public Transform keepTileLocationMarker;

		public bool DroppedOnKeep(Transform droppedOn)
		{
			return droppedOn == keepTileLocationMarker;
		}

		public GameBoardSlotDivideActivator ReturnEmptyTileBasedOnLocation(Vector3 location)
		{
			return emptyTileScripts.FirstOrDefault(t => t.transform.position == location);
		}

	}
}
