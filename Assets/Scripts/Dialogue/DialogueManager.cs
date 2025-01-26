using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI text;
    public int chosenSentence;
    public string[] sentences;
    public Canvas dialogueCanvas;
    public Narrations narrator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    public void ShowDialogue()
    {
        text.text = narrator.sentences[narrator.chosenSentence];
        dialogueCanvas.gameObject.SetActive(true);
    }

    public void CloseDialogue()
    {
        dialogueCanvas.gameObject.SetActive(false);
    }

   
}
