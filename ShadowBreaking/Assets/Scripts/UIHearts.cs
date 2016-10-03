// Author: Miles
// Purpose: Attach this script to the individual heart game objects. The HeartManager will call these functions.

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIHearts : MonoBehaviour {

    public Sprite heartFull, heartHalf, heartEmpty;

    private Image sprite;

    void Start()
    {
        sprite = GetComponent<Image>();
    }

    public void DisplayFullHeart()
    {
        sprite.sprite = heartFull;
    }

    public void DisplayHalfHeart()
    {
        sprite.sprite = heartHalf;
    }

    public void DisplayEmptyHeart()
    {
        sprite.sprite = heartEmpty;
    }

    // These will be used when the player gets a max health up (Gets an extra heart container)
    public void TurnRendererOff()
    {
        sprite.enabled = false;
    }

    public void TurnRendererOn()
    {
        sprite.enabled = true;
    }

}
