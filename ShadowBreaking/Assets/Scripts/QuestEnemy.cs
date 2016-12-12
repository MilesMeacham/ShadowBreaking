using UnityEngine;
using System.Collections;

public class QuestEnemy : MonoBehaviour {
    public GameObject QuestNPC_collider;
    public AudioClip victorySFX;

    private EnemyHealth HP;
    private QuestDialogue questDialogue;
    private AudioSource sfx;

    // START:
    void Start () {
        sfx = GetComponent<AudioSource>();
        sfx.clip = victorySFX;
        questDialogue = QuestNPC_collider.GetComponent<QuestDialogue>();
        HP = this.GetComponent<EnemyHealth>();
    }

	// UPDATE:
	void Update () {
        if (HP.currentHealth <= 0) {
            QuestComplete();
            sfx.Play();
        }
	}

    // QUEST COMPLETE:
    void QuestComplete() {
        questDialogue.questComplete = true;
    }
}