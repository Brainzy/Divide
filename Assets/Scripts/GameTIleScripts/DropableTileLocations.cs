using UnityEngine;

public class DropableTileLocations : MonoBehaviour
{
	public EmptyTileScript[] emptyTileScripts;
	public Transform keepTileLocationMarker;

	public bool DroppedOnKeep(Transform droppedOn)
	{
		print("provera " + droppedOn.name + " " + keepTileLocationMarker.name);
		return droppedOn == keepTileLocationMarker;
	}





}
