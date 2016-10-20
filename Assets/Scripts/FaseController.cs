using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class FaseController : MonoBehaviour {

	/*
	 *  Suddene,
		Dundeya,
		Callista,
		Salistick,
		Oldeom
	 * 
	*/

	void OnEnable()
	{
		//Tell our 'OnLevelFinishedLoading' function to start listening for a scene change as soon as this script is enabled.
		SceneManager.sceneLoaded += OnLevelFinishedLoading;
	}

	void OnDisable()
	{
		//Tell our 'OnLevelFinishedLoading' function to stop listening for a scene change as soon as this script is disabled. Remember to always have an unsubscription for every delegate you subscribe to!
		SceneManager.sceneLoaded -= OnLevelFinishedLoading;
	}

	void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
	{
		Debug.Log("Level Loaded");
		/*Debug.Log(scene.name);
		Debug.Log(mode);*/
		Time.timeScale = 1;
	}

	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public void LoadScene(string sceneName)
	{
		SceneManager.LoadScene (sceneName);
	}
}
