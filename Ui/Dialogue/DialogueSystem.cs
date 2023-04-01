using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    public Animator animator;
    public GameObject enterToSkipTxt;

    public Text nameTxt;
    public Text dialogueTxt;

    public List<string> sentences;
    public int currentSentence = 0;
    public bool allowSentenceSkip = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && allowSentenceSkip)
        {
            DisplayNextSentence();
        }

        if (allowSentenceSkip) enterToSkipTxt.SetActive(true);
        else enterToSkipTxt.SetActive(false);
    }
    public void StartDialogue(Dialogue dialogue)
    {
        currentSentence = 0;
        animator.SetBool("isOpen", true);

        Debug.Log(dialogue.sentences[0]);

        nameTxt.text = dialogue.name;

        sentences.Clear();

        Debug.Log(dialogue.sentences[0]);

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Add(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (currentSentence + 1 > sentences.Count)
        {
            EndDialogue();
            currentSentence = 0;
            return;
        }

        StopAllCoroutines();
        allowSentenceSkip = false;
        StartCoroutine(TypeSentence(sentences[currentSentence]));
        currentSentence++;
    }

    IEnumerator TypeSentence (string sentence)
    {
        dialogueTxt.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueTxt.text += letter;
            yield return new WaitForSeconds(0.012f);
        }

        allowSentenceSkip = true;
    }

    public void EndDialogue()
    {
        currentSentence = 0;

        animator.SetBool("isOpen", false);
    }

}
