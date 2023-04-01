using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public bool alreadyUsed = false;
    public bool triggerAtStart, triggerOnTouch;

    public Dialogue dialogue;

    private void Start()
    {
        if (triggerAtStart) TriggerDialogue();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && triggerOnTouch)
        {
            TriggerDialogue();
        }
    }
    public void TriggerDialogue()
    {
        if (alreadyUsed) return;

        alreadyUsed = true;
        FindObjectOfType<DialogueSystem>().StartDialogue(dialogue);
    }
    public void EndGame()
    {
        TriggerDialogue();
    }
}
