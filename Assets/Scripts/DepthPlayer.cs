using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class DepthPlayer : MonoBehaviour {

    //References to the color and depth players
    public VideoPlayer color;
    public VideoPlayer depth;
	public VideoPlayer mask;

	//Animator attached to the game object
	public Animator animManager;

    // Use this for initialization
    void Start () {

		//Start the animation on load
		this.playAnimation ();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//API Methods
	public bool isPlaying(){
		if (color.isPlaying && depth.isPlaying && mask.isPlaying)
			return true;

		return false;
	}
	public void play(){
		if (!color.isPlaying && !depth.isPlaying && !mask.isPlaying) {
			color.Play ();
			depth.Play ();
			mask.Play ();
		} else {
			Debug.Log ("Video already playing");
		}
	}

	public void restart(){
		if (color.isPlaying && depth.isPlaying && mask.isPlaying) {
			color.Stop ();
			depth.Stop ();
			mask.Stop ();
			//Then play
			this.play ();
		} else {
			Debug.Log ("Video already stopped");
		}
	}

	public void pause(){
		if (color.isPlaying && depth.isPlaying && mask.isPlaying) {
			color.Pause ();
			depth.Pause ();
			mask.Pause ();
		} else {
			Debug.Log ("Video already paused");
		}
	}

	public void setLoop(bool shouldLoop){
		color.isLooping = shouldLoop;
		depth.isLooping = shouldLoop;
		mask.isLooping = shouldLoop;
	}

	public void playAnimation(){
		if (!animManager.isActiveAndEnabled)
			animManager.enabled = true;
	}

	public void restartAnimation(){
		if (animManager.isActiveAndEnabled)
			//Disabling and enabling restarts the animation tracks
			animManager.Play("Idle");
	}

}
