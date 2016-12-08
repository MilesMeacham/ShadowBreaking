using UnityEngine;
using System.Collections;

public class HealerAlliesNearbyAI : MonoBehaviour {

    public HealerAI healerAI;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            healerAI.alliesNearBy.Add(other.gameObject);
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            healerAI.alliesNearBy.Remove(other.gameObject);
        }
    }
}
