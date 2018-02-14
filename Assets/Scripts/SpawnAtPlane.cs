using System;
using System.Collections.Generic;

namespace UnityEngine.XR.iOS
{
	public class SpawnAtPlane : MonoBehaviour
	{
		
		public float distance;

		public DepthPlayer depthmanager;

		//Method for checking intersections
		bool HitTestWithResultType (ARPoint point, ARHitTestResultType resultTypes)
		{
			List<ARHitTestResult> hitResults = UnityARSessionNativeInterface.GetARSessionNativeInterface ().HitTest (point, resultTypes);
			if (hitResults.Count > 0) {
				foreach (var hitResult in hitResults) {
					Debug.Log ("Hit a surface!");

					//Calculate the direction
					Vector3 N = Camera.main.transform.position - gameObject.transform.position;

					//Create a look rotation to always face the camera
					gameObject.transform.rotation = Quaternion.LookRotation(N);


					return true;
				}
			}
			return false;
		}

		void Awake(){
			depthmanager = GetComponent<DepthPlayer> ();
		}

		// Update is called once per frame
		void Update () {
			if (Input.touchCount > 0 && gameObject.transform != null)
			{
				var touch = Input.GetTouch(0);
				if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved)
				{
					var screenPosition = Camera.main.ScreenToViewportPoint(touch.position);
					ARPoint point = new ARPoint {
						x = screenPosition.x,
						y = screenPosition.y
					};

					// prioritize reults types
					ARHitTestResultType[] resultTypes = {
						ARHitTestResultType.ARHitTestResultTypeExistingPlaneUsingExtent, 
						ARHitTestResultType.ARHitTestResultTypeHorizontalPlane, 
						ARHitTestResultType.ARHitTestResultTypeFeaturePoint
					}; 

					foreach (ARHitTestResultType resultType in resultTypes)
					{
						if (HitTestWithResultType (point, resultType))
						{

							if (!depthmanager.isPlaying ()) {
								depthmanager.play ();
							}
							return;
						}
					}
				}
			}
		}


	}
}

