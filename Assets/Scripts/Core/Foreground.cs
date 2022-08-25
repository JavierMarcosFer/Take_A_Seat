using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Foreground : MonoBehaviour
{
    public static Foreground instance;
    public Sprite roysDeath;
    public Sprite fakePhoto;
    public Sprite horsePhoto;
    public Sprite blackBackground;
    private Image imageComponent;
    public GameObject textContainer;
    public CreditsButton creditsButton;
    private Text textComponent;
    private DialogueManager dialogueManager;
    public MemoriesMenuButton memoriesButton;
    public GameObject memoryButtonContainer;
    public Coroutine fading;

    // Collection of images
    public enum imageID
    {
        RoysDeath,
        FakePhoto,
        HorsePhoto,
        BlackBackground,
        Tutorial
    }
    // True if speaking coroutine is currently running
    public bool isFading { get { return fading != null; } }

    // Make sure that foreground shows nothing at first
    private void Awake()
    {
        instance = this;
        fading = null;
        imageComponent = this.GetComponent<Image>();
        imageComponent.color = new Color(255, 255, 255, 0);
        textComponent = textContainer.GetComponent<Text>();
        textComponent.color = new Color(29, 72, 188, 0);
    }

    // Get dialogueManager
    private void Start()
    {
        dialogueManager = DialogueManager.instance;
        // Make menu button transparent at first
        Image buttonImg = memoriesButton.GetComponent<Image>();
        Color c = buttonImg.color;
        c.a = 0f;
        buttonImg.color = c;
    }

    // 
    public void fadeOut()
    {
        fading = StartCoroutine(FadeImage(true));
    }
    // Fade in the image with the corresponding ID
    public void showImage(imageID id)
    {
        if (id == imageID.FakePhoto)
        {
            imageComponent.sprite = fakePhoto;
            fading = StartCoroutine(FadeImage(false));
        }
        else if (id == imageID.HorsePhoto)
        {
            imageComponent.sprite = horsePhoto;
            fading = StartCoroutine(FadeImage(false));
        }
        else if (id == imageID.RoysDeath)
        {
            imageComponent.sprite = roysDeath;
            fading = StartCoroutine(FadeImage(false));
        }
        else if (id == imageID.BlackBackground)
        {
            imageComponent.sprite = blackBackground;
            fading = StartCoroutine(FadeImage(false));
        }
    }

    
    // Detect when a the left mouse button is pressed, but no buttons are pressed
    // ( Clicked the screen to advance dialogue )
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !memoriesButton.isMouseOverButton() && !memoriesButton.isShowingMemoriesMenu && !ChoicePanel.isShowingChoices && !creditsButton.isMouseOver)
        {
            dialogueManager.AdvanceDialogue();
        }
    }

    // Fade foreground images in and out over a given time in seconds
    // if fadeAway is true, the image will vanish
    // default duration is 1 second
    IEnumerator FadeImage(bool fadeAway, double fadeDuration = 0.6)
    {
        float timeElpased = 0;
        Color c = imageComponent.color;
        // Check wether to fade image in or out
        if (fadeAway)
        {
            // Fade out
            while(timeElpased < fadeDuration)
            {
                // Track time elapsed
                timeElpased += Time.deltaTime;
                double progress = 1 - (timeElpased / fadeDuration);
                // Change color component
                c.a = (float) progress;
                imageComponent.color = c;
                yield return null;
            }
        }
        else
        {
            // Fade in
            while (timeElpased < fadeDuration)
            {
                // Track time elapsed
                timeElpased += Time.deltaTime;
                double progress = timeElpased / fadeDuration;
                // Change color component
                c.a = (float)progress;
                imageComponent.color = c;
                yield return null;
            }
        }
        // Advance dialogue when fadeout ends
        dialogueManager.AdvanceDialogue();
    }

    // Function similar to FadeImage, but specifically adjusted to show the tutorial prompt
    IEnumerator FadeTutorial(bool fadeAway, double fadeDuration = 0.6)
    {
        // Set black background as image
        imageComponent.sprite = blackBackground;
        // We want the final background to still be somewhat transparent
        double targetAplha = 0.70;
        float timeElpased = 0;
        Color c = imageComponent.color;
        Color tc = textComponent.color;
        // Check wether to fade image in or out
        if (fadeAway)
        {
            // Fade out
            while (timeElpased < fadeDuration)
            {
                // Track time elapsed
                timeElpased += Time.deltaTime;
                double progress = 1 - (timeElpased / fadeDuration);
                // Change color component
                c.a = (float) (progress * targetAplha);
                imageComponent.color = c;
                tc.a = (float)progress;
                textComponent.color = tc;
                yield return null;
            }
        }
        else
        {
            // Fade in
            while (timeElpased < fadeDuration)
            {
                // Track time elapsed
                timeElpased += Time.deltaTime;
                double progress = timeElpased / fadeDuration;
                // Change color component
                c.a = (float) (progress * targetAplha);
                imageComponent.color = c;
                tc.a = (float)progress;
                textComponent.color = tc;
                yield return null;
            }
        }
        // Advance dialogue when fadeout ends
        dialogueManager.AdvanceDialogue();
    }

    public void ShowTutorial(bool fadeAway, double fadeDuration = 0.6)
    {
        // if showing the tutorial, reveal menu button
        if (!fadeAway)
        {
            // Make menu button transparent at first
            Image buttonImg = memoriesButton.GetComponent<Image>();
            Color c = buttonImg.color;
            c.a = 1f;
            buttonImg.color = c;
        }
        fading = StartCoroutine(FadeTutorial(fadeAway, fadeDuration));
    }
}
