using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoicePanel : MonoBehaviour
{
    public static ChoicePanel instance;
    public static DialogueManager dialogueManager;
    public GameObject panelRoot;
    public ChoiceButton prefabButton;           // Default button, used as reference when adding dialogue options
    static List<ChoiceButton> optionList = new List<ChoiceButton>();    // List of currently available buttons
    public VerticalLayoutGroup vLayoutGroup;    // Manages the arrangement of buttons on the screen

    private void Awake()
    {
        instance = this;
    }

    // Initialize as singleton
    void Start()
    {
        dialogueManager = DialogueManager.instance;
        Hide();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Delete all current options
    public static void ClearAllCurrentChoices()
    {
        foreach (ChoiceButton button in optionList)
        {
            DestroyImmediate(button.gameObject);
        }
        optionList.Clear();
    }

    public static void Show(params string[] choices)
    {
        // Activate root
        instance.panelRoot.SetActive(true);

        // Cleanup
        if (isShowingChoices)
        {
            instance.StopCoroutine(showingChoices);
        }
        ClearAllCurrentChoices();

        showingChoices = instance.StartCoroutine(ShowingChoices(choices));
    }

    public static void Hide()
    {
        
        // Stop chowing choices coroutine
        if (isShowingChoices)
        {
            instance.StopCoroutine(showingChoices);
        }
        showingChoices = null;

        ClearAllCurrentChoices();

        // Hide root panel
        instance.panelRoot.SetActive(false);
    }

    public static bool isWaitingForChoice { get { return isShowingChoices && !lastChoice.madeSelection; } }
    public static bool isShowingChoices { get { return showingChoices != null; } }

    static Coroutine showingChoices = null;
    public static IEnumerator ShowingChoices(params string[] choices)
    {
        yield return new WaitForEndOfFrame();
        lastChoice.Reset();

        // Add all options to menu
        for (int i = 0; i < choices.Length; i++)
        {
            AddChoice(choices[i], i);
        }

        // Wait for user to make a choice
        while (isWaitingForChoice)
        {
            yield return new WaitForEndOfFrame();
        }

        // Reach out to Dialogue Manager to take it from here
        dialogueManager.NextDialogue();

        Hide();
    }

    static void AddChoice(string text, int index)
    {
        // Create a new button out of the Prefab and activate it
        GameObject o = Instantiate(instance.prefabButton.gameObject, instance.prefabButton.transform.parent);
        o.SetActive(true);

        ChoiceButton newButton = o.GetComponent<ChoiceButton>();
        // Add info to button
        newButton.text = text;
        newButton.choiceIndex = index;

        optionList.Add(newButton);
    }

    // Used to keep track of the player's last choice
    [System.Serializable]
    public class CHOICE
    {
        // Returns true if any choices are stored.
        public bool madeSelection { get { return !(text == "" && index == -1); } }

        // Used to reference which choices are made
        public string text = "";
        public int index = -1;

        // Resets values back to default, causing madeSelection to return "false".
        public void Reset()
        {
            text = "";
            index = -1;
        }
    }
    public CHOICE choice = new CHOICE();
    public static CHOICE lastChoice { get { return instance.choice;} }

   public void MakeChoice(ChoiceButton button)
    {
        lastChoice.text = button.text;
        lastChoice.index = button.choiceIndex;
    }
}
