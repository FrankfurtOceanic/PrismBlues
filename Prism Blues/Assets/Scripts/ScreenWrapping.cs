
    
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWrapping : MonoBehaviour
{

	


	//We use 8 clones to make this wrapping effect 
	Transform[] clones = new Transform[8];

	float screenWidth;
	float screenHeight;
	// Start is called before the first frame update
	void Start()
	{
		var cam = Camera.main;

		var screenBottomLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, transform.position.z));
		var screenTopRight = cam.ViewportToWorldPoint(new Vector3(1, 1, transform.position.z));
		screenWidth = screenTopRight.x - screenBottomLeft.x;
		screenHeight = screenTopRight.y - screenBottomLeft.y;

		createClones();
	}

	// Update is called once per frame
	void Update()
	{
		ScreenWrap();
	}
	private void FixedUpdate()
	{
		// All ghost ships should have the same rotation as the main ship
		for (int i = 0; i < 8; i++)
		{
			clones[i].rotation = transform.rotation;
		}
	}

	void ScreenWrap()
	{
		if (transform.position.y > screenHeight/2 || transform.position.y < -screenHeight / 2 || transform.position.x > screenWidth / 2 || transform.position.x < -screenWidth / 2 )
		{
			SwapPlayerWithClone();
		}
	}

	void createClones()
	{
		for (int i = 0; i < 8; i++)
		{
			clones[i] = Instantiate(transform, Vector3.zero, Quaternion.identity) as Transform;
			clones[i].gameObject.tag = "Untagged";
			DestroyImmediate(clones[i].GetComponent<ScreenWrapping>());
			//DestroyImmediate(clones[i].GetComponent<Shooting>());
			//DestroyImmediate(clones[i].GetComponent<ShootingAlt>());

		}

		PositionClones();
	}

	void PositionClones()
	{
		// All ghost positions will be relative to the ships (this) transform,
		// so let's star with that.
		var clonePosition = transform.position;

		// We're positioning the ghosts clockwise behind the edges of the screen.
		// Let's start with the far right.
		clonePosition.x = transform.position.x + screenWidth;
		clonePosition.y = transform.position.y;
		clones[0].position = clonePosition;

		// Bottom-right
		clonePosition.x = transform.position.x + screenWidth;
		clonePosition.y = transform.position.y - screenHeight;
		clones[1].position = clonePosition;

		// Bottom
		clonePosition.x = transform.position.x;
		clonePosition.y = transform.position.y - screenHeight;
		clones[2].position = clonePosition;

		// Bottom-left
		clonePosition.x = transform.position.x - screenWidth;
		clonePosition.y = transform.position.y - screenHeight;
		clones[3].position = clonePosition;

		// Left
		clonePosition.x = transform.position.x - screenWidth;
		clonePosition.y = transform.position.y;
		clones[4].position = clonePosition;

		// Top-left
		clonePosition.x = transform.position.x - screenWidth;
		clonePosition.y = transform.position.y + screenHeight;
		clones[5].position = clonePosition;

		// Top
		clonePosition.x = transform.position.x;
		clonePosition.y = transform.position.y + screenHeight;
		clones[6].position = clonePosition;

		// Top-right
		clonePosition.x = transform.position.x + screenWidth;
		clonePosition.y = transform.position.y + screenHeight;
		clones[7].position = clonePosition;

	}

	void SwapPlayerWithClone()
	{
		foreach (var clone in clones)
		{
			if (clone.position.x < screenWidth/2 && clone.position.x > -screenWidth/2 &&
				clone.position.y < screenHeight/2 && clone.position.y > -screenHeight/2)
			{
				transform.position = clone.position;
				PositionClones();
				break;
			}
		}
		//now we have to reposition the clones
		//Debug.LogError("Position swapped and now repositioning");
		
	}
}