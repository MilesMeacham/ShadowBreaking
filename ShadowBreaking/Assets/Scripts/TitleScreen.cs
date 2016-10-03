// Author: Miles

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour {

	// Loads scene 1
    // We will want to change this to load the next scene or something
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }


}
