using UnityEngine;
using System.Collections;

public class QuestItem : MonoBehaviour {
    public GameObject QuestNPC_collider;
    QuestDialogue questDialogue;

    public AudioClip collectSFX;
    AudioSource sfx;


    // START:
    void Start() {
        sfx = GetComponent<AudioSource>();
        sfx.clip = collectSFX;
        questDialogue = QuestNPC_collider.GetComponent<QuestDialogue>();
    }


    // UPDATE:
    void Update() { }


    /// ON TRIGGER ENTER (2D):
    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "Player") {
            Debug.Log("Player Collided!");
            sfx.Play();
            questDialogue.questComplete = true;
            this.GetComponent<Collider2D>().enabled = false;
            this.GetComponent<SpriteRenderer>().enabled = false;
            Destroy(gameObject, 0.5f);
        }
    }
}