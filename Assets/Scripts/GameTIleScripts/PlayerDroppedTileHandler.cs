using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerDroppedTileHandler : MonoBehaviour
{
	[SerializeField] private DropableTileLocations dropableTileLocations;
	[SerializeField] private float dropAnimationTime = 0.3f;
	public Vector2 scaleForKeepPosition = new Vector2(1.1f,1.1f);
	public Vector2 normalScale = new Vector2(1f,1f);
	public static PlayerDroppedTileHandler inst;

	private void Awake()
	{
		inst = this;
	}

	public bool IncommingTileDroppedSuccessCheck(IncommingTileScript droppedTile, List<Transform> hitObjectList)
	{
		print("drop check" + hitObjectList.Count);
		foreach (var VARIABLE in hitObjectList)
		{
			print(VARIABLE.name);
		}
		if (hitObjectList.Count > 1) return false; // means occupied slot
		if (hitObjectList.Count == 0) return false; // means dragged nowhere
		SuccessfullyDropedTile(droppedTile,hitObjectList[0]);
		return true;
	}

	private void SuccessfullyDropedTile(IncommingTileScript droppedTile, Transform transformDropedOn)
	{
		if (dropableTileLocations.DroppedOnKeep(transformDropedOn))
		{
			DropOnKeepTile(droppedTile, transformDropedOn);
		}
		else
		{
			DropOnGameTile(droppedTile, transformDropedOn);
		}
	}

	private void DropOnGameTile(IncommingTileScript droppedTile, Transform transformDropedOn)
	{
		droppedTile.transform.DOLocalMove(transformDropedOn.localPosition, dropAnimationTime);
		droppedTile.myRectTransform.DOScale(normalScale, dropAnimationTime);
		droppedTile.dragable = false;
	}

	private void DropOnKeepTile(IncommingTileScript droppedTile, Transform transformDropedOn)
	{
		droppedTile.transform.DOLocalMove(transformDropedOn.localPosition, dropAnimationTime);
		droppedTile.myRectTransform.DOScale(scaleForKeepPosition, dropAnimationTime);
		droppedTile.dragable = true;
	}
}
