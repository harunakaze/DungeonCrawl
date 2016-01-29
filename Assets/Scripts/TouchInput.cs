/// <summary>
/// Simple touch input system, to detect simple gesture.
/// Will provide a simple interface that returns a boolean, that tell weather or not the user input a gesture
/// ©2015 Saka Wibawa Putra
/// </summary>
/// 
/// <license>
/// Copyright 2015 Saka Wibawa Putra
/// 
/// Licensed under the Apache License, Version 2.0 (the "License");
/// you may not use this file except in compliance with the License.
///	You may obtain a copy of the License at
///		
///		http://www.apache.org/licenses/LICENSE-2.0
///		
/// Unless required by applicable law or agreed to in writing, software
///	distributed under the License is distributed on an "AS IS" BASIS,
///	WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
///	See the License for the specific language governing permissions and
///	limitations under the License.
/// </license>
/// 
/// <usage>
/// 1) Attach this script to a gameobject, you only need ONE for every scene
/// 2) Include the namespace in your script, you can remove it if you want, i create it just to be safe.
/// 3) Call the TouchInput.GetSwap("...") method from Update() method
/// 4) Done.
/// </usage>
/// 
/// <example>
/// <code>
/// using HKExtension;
/// ...
/// if(TouchInput.GetSwap("Up"))
///   Jump();
/// ...
/// </code>
/// </example>

using UnityEngine;
using System.Collections;


namespace HKExtension {
public class TouchInput : MonoBehaviour {
	//You can tweak this variable as you need
	public float minimumSwapDistance = 100.0f;
	public float strayThreshold = 70.0f;

	public float minimumSwapTime = 0.5f;

	private float sqrMinimum;
	private Vector2 startPosition;

	private float startTime;

	private bool isHorizontalSwipe = false;
	private bool isVerticalSwipe = false;

	private bool isReseting = false;

	private enum SwipeDirection {
		none,
		left,
		right,
		up,
		down
	}

	private static SwipeDirection swipeDirection;
	
	void Start() {
		//We use sqr minimum, to save processing power
		sqrMinimum = minimumSwapDistance * minimumSwapDistance;
	}

	void Update() {
		//Reset direction every next frame, after sending the gesture to all script that need it
		ResetDirection ();

		//Exit, if no touch detected
		if (Input.touchCount != 0) {
			Touch touch = Input.GetTouch(0);

			switch (touch.phase) {
			case TouchPhase.Began:
				startPosition = touch.position;

				startTime = Time.time;

				isHorizontalSwipe = false;
				isVerticalSwipe = false;
				break;

			case TouchPhase.Moved:
				//Define the swipe direction, by calculating the current position to the start position
				//Is it far enough from the start position, to be considered starying?
				if (Mathf.Abs(touch.position.y - startPosition.y) > strayThreshold) {
					isVerticalSwipe = true;
				}

				if (Mathf.Abs(touch.position.x - startPosition.x) > strayThreshold) {
					isHorizontalSwipe = true;
				}
				break;

			case TouchPhase.Ended:
				//Get the swipe time
				float swipeTime = Time.time - startTime;

				//Does the swipe satisfy the minimum swipe time?
				if(swipeTime <= minimumSwapTime) {
					//Process horizontal swipe
					if(isHorizontalSwipe) {
						//Calculate swipe distance on X axis only
						float swipeDistance = (new Vector2(touch.position.x, 0) - new Vector2(startPosition.x, 0)).sqrMagnitude;

						//Does the swipe distance satisfy the minimum swipe distance?
						if(swipeDistance >= sqrMinimum) {
							//Calculate the swipe sign + / -
							float swipeSign = Mathf.Sign(touch.position.x - startPosition.x);

							//Positive means the swipe is to the right, vice versa.
							if(swipeSign > 0)
								swipeDirection = SwipeDirection.right;
							else
								swipeDirection = SwipeDirection.left;

							isReseting = true;
						}
					}
					//Identical to horizontal swipe
					if (isVerticalSwipe) {
						float swipeDistance = (new Vector2(0, touch.position.y) - new Vector2(0, startPosition.y)).sqrMagnitude;
						
						if(swipeDistance >= sqrMinimum) {
							float swipeSign = Mathf.Sign(touch.position.y - startPosition.y);
							
							if(swipeSign > 0)
								swipeDirection = SwipeDirection.up;
							else
								swipeDirection = SwipeDirection.down;

							isReseting = true;
						}
					}
				}
				break;
			}
		}
	}

	void ResetDirection() {
		if (isReseting) {
			swipeDirection = SwipeDirection.none;
			isReseting = false;
		}
	}

	//Works like, if(Input.GetAxis("Horizontal")) { ... }
	public static bool GetSwap(string direction) {
		if (direction.Equals ("Right")) {
			return swipeDirection == SwipeDirection.right;
		} else if (direction.Equals ("Left")) {
			return swipeDirection == SwipeDirection.left;
		} else if (direction.Equals ("Up")) {
			return swipeDirection == SwipeDirection.up;
		} else if (direction.Equals ("Down")) {
			return swipeDirection == SwipeDirection.down;
		} 
		else {
			Debug.LogError ("No Such Axis.");
			return false;
		}
	}
}
} //namespace HKExtension.