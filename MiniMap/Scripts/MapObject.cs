// ISTA 425 / INFO 525 Algorithms for Games
//
// Sample code file

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapObject : MonoBehaviour 
{
	MiniMapEntity linkedMiniMapEntity;
	MiniMapController mmc;
	GameObject owner;
	Camera mapCamera;
	Image spr;
	GameObject panelGO;

	Vector2 screenPos;
	RectTransform sprRect;
	RectTransform rt;

	Transform miniMapTarget;

	float oldPlayerRot;

	void FixedUpdate () 
	{
		if (owner == null)
			Destroy (this.gameObject);
		else
			SetTransform ();
	}

	public void SetMiniMapEntityValues(MiniMapController controller, MiniMapEntity mme, 
									   GameObject attachedGO, Camera renderCamera, GameObject parentPanelGO)
	{
		//Debug.Log(mme.size);
		linkedMiniMapEntity = mme;
		owner = attachedGO;
		mapCamera = renderCamera;
		panelGO = parentPanelGO;
		spr = gameObject.GetComponent<Image> ();
		spr.sprite = mme.icon;
		sprRect = spr.gameObject.GetComponent<RectTransform> ();
		sprRect.sizeDelta = mme.size;
		rt = panelGO.GetComponent<RectTransform> ();
		mmc = controller;
		miniMapTarget = mmc.target;
		oldPlayerRot = GameObject.Find("Player Car").transform.rotation.eulerAngles.y;
		SetTransform ();
	}

	// TODO: Implement transformation of registered map icons in MiniMap space
	void SetTransform()
	{
		transform.SetParent(panelGO.transform, false);

		screenPos = mapCamera.WorldToScreenPoint(owner.transform.position);
		screenPos = new Vector2(screenPos.x - 128.5f, screenPos.y - 128.5f);
		sprRect.anchoredPosition = screenPos;


		//sprRect.rotation = Quaternion.Euler(0, 0, 0);
		var ownerRot = owner.transform.eulerAngles;
		var camerRot = mapCamera.transform.eulerAngles;
		var sprRot = new Vector3(0, 0, 0);
		if (owner.name == "Player Car") {	// set rotation for the player car
			sprRot.z = 360 - (Mathf.Abs(camerRot.y + camerRot.z) - ownerRot.y);
			sprRect.rotation = Quaternion.Euler(sprRot);
		} else if (owner.name == "NPC Car") // set rotation for the NPC car
        {
			sprRot.z = 180 - (Mathf.Abs(camerRot.y + camerRot.z) + ownerRot.y);
			sprRect.rotation = Quaternion.Euler(sprRot);

			float newPlayerRot = GameObject.Find("Player Car").transform.rotation.eulerAngles.y;
			//Debug.Log(newPlayerRot + " " + oldPlayerRot + " " + GameObject.FindGameObjectWithTag("Player").name);
			if (newPlayerRot == oldPlayerRot)
			{
				oldPlayerRot = newPlayerRot;
			} else // attempt to cancel out the rotation of the minimap camera ##DOES NOT WOKR PROPERLY##
            {
				//Debug.Log("old: " + sprRect.rotation.eulerAngles + " " + owner.name);
				float difference = Mathf.Abs(newPlayerRot - oldPlayerRot);
				sprRot.z -= difference * 10;
				
				sprRect.rotation = Quaternion.Euler(sprRot);
				//Debug.Log("new: " + sprRect.rotation.eulerAngles + " " + difference);
				oldPlayerRot = newPlayerRot;
				//Debug.Log(difference);
			}
		} else  // set no rotation for all waypoints
        {
			sprRect.rotation= Quaternion.Euler(0, 0, 0);
        }

		//sprRect.rotation = owner.transform.rotation;
		//Debug.Log(owner.name + " " + owner.transform.eulerAngles + " " + sprRect.rotation.eulerAngles + " " + mapCamera.transform.rotation.z + " " + sprRect.parent.parent.name + " " + sprRect.parent.parent.parent.parent.rotation.eulerAngles);
		

		// Some useful variables (see definitions above and in Unity docs):
		//   screenPos
		//   sprRect.rotation
		//   sprRect.anchoredPosition

	}
}
