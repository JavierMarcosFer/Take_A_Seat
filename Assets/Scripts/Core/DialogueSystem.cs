using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DialogueSystem : MonoBehaviour
{
    public static DialogueSystem instance;
    public CharacterSprite characterSprite;
    public ELEMENTS elements;

    // Ensures only one Dialogue System is active at any given time
    void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// Display dialogue in speech bubble. If isAdditive, provided text will be shown in addition to
    /// the previous text. Otherwise it will replace the text. If speaker is not provided, the previous
    /// speaker will be used.
    /// </summary>
    public void Say(string targetSpeech, bool isAdditive, string soundFile, string speaker, CharacterSprite.detectiveSprites sprite)
    {
        StopTalking();
        CharacterSprite.instance.changeSprite(sprite);
        speaking = StartCoroutine(Speaking(targetSpeech, isAdditive, soundFile, speaker, sprite));
    }

    // Make the character stop speaking
    void StopTalking()
    {
        if (isSpeaking)
        {
            StopCoroutine(speaking);
        }
        speaking = null;
    }

    // True if speaking coroutine is currently running
    public bool isSpeaking { get { return speaking != null; } }
    // Tracks if text has finished and input is required
    [HideInInspector] public bool isWaitingForUserInput = false;

    string targetSpeech = "";
    Coroutine speaking = null;
    // 
    IEnumerator Speaking(string speech, bool isAdditive, string soundFile, string speaker, CharacterSprite.detectiveSprites sprite)
    {
        SpeechBubble.SetActive(true);
        targetSpeech = speech;          // Text we expect to have at the end
        
        if (!isAdditive)
            // If text is not additive, delete previous text
            speechText.text = "";
        else
            // Otherwise, take previous text into account when determining targetSpeech
            targetSpeech = speechText.text + ' ' + targetSpeech;

        // Determine speaker for this text box
        string name = DetermineSpeaker(speaker);
        speakerNameText.text = name;
        AdjustForInnerThought(name);

        // Not waiting for user to continue
        isWaitingForUserInput = false;

        //Play sound file, ignoring placeholders
        if (soundFile != "FILE" && soundFile != "") 
            audioController.PlayVoiceLine(soundFile);

        // Add letters one by one until expected text is achieved
        while (speechText.text != targetSpeech)
        {
            //If input is provided beforehand, finish text immediately
            speechText.text += targetSpeech[speechText.text.Length];
            yield return new WaitForEndOfFrame();
        }

        // When text is finished, wait for user input
        isWaitingForUserInput = true;
        while (isWaitingForUserInput)
            yield return new WaitForEndOfFrame();

        StopTalking();
    }

    /// <summary>
    /// Determines who is currently speaking. If "" is passed in, the last spaker's name will be re-used
    /// </summary>
    string DetermineSpeaker(string s)
    {
        if (s == "")
        {
            return speakerNameText.text;
        }
        else return s;
    }

    /// <summary>
    /// Determines if the text is meant to be an inner thought. If it is, text is italicized and colored blue.
    /// </summary>
    public void AdjustForInnerThought(string speaker)
    {
        // If text is monologue, italizice text
        if (speaker == " ")
        {
            speechText.fontStyle = FontStyle.Italic;
            speechText.color = Color.blue;
            speakerNameBox.SetActive(false);
        }
        else
        {
            speakerNameBox.SetActive(true);
            speechText.fontStyle = FontStyle.Normal;
            speechText.color = Color.black;
        }
    }

    //Storage for all things to be referenced later
    [System.Serializable]
    public class ELEMENTS
    {
        public GameObject SpeechBubble;
        public Text speakerNameText;
        public Text speechText;
        public GameObject speakerNameBox;
        public AudioController audioController;
    }

    public GameObject SpeechBubble { get{return elements.SpeechBubble; } }
    public GameObject speakerNameBox { get { return elements.speakerNameBox; } }
    public Text speakerNameText { get { return elements.speakerNameText; } }
    public Text speechText { get { return elements.speechText; } }
    public AudioController audioController { get { return elements.audioController; } }
}
