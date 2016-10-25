using UnityEngine;
using System.Collections;

public class WeaponCollision : MonoBehaviour {
	
    public int weaponDamage = 10;
	
	// Use this for initialization
	void Start () {
	}
	
    void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Enemy")
        {
            EnemyHealth enemy = collider.gameObject.GetComponent<EnemyHealth>();
            Debug.Log("Enemy Hit");
            enemy.TakeDamage(weaponDamage);

        }
    }
}


