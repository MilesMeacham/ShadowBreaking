using UnityEngine;
using System.Collections;

public class HealerStopPointAI : MonoBehaviour
{

    public HealerAI healerAI;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            healerAI.moveTowardsAlly = false;
        }
    }
}
