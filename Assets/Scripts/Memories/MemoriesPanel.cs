using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemoriesPanel : MonoBehaviour
{
    public static MemoriesPanel instance;
    public GameObject panelRoot;
    public GameObject MemoryCategoryMenu;
    public GameObject MemoriesInfoPanel;
    public GameObject switchButton;
    public MemoriesInfo MemoriesInfoScript;
    public GameObject MemoriesBackButton;
    public GameObject choicesPanel;
    public GameObject musicPlayer;
    public AudioSource audioComponent;
    
    public void Show()
    {
        panelRoot.SetActive(true);
        MemoryCategoryMenu.SetActive(true);
        MemoriesInfoPanel.SetActive(false);
        MemoriesBackButton.SetActive(false);
        switchButton.SetActive(true);
        choicesPanel.SetActive(false);
    }
    
    public void Hide()
    {
        MemoryCategoryMenu.SetActive(false);
        MemoriesInfoPanel.SetActive(false);
        MemoriesBackButton.SetActive(false);
        switchButton.SetActive(false);
        choicesPanel.SetActive(true);
        panelRoot.SetActive(false);
    }

    public void showFlashbacks()
    {
        DetailView();
        audioComponent.Play();
        MemoriesInfoScript.LoadFlashbacks();
    }

    public void showPeople()
    {
        DetailView();
        audioComponent.Play();
        MemoriesInfoScript.LoadPeople();
    }

    public void showItems()
    {
        DetailView();
        audioComponent.Play();
        MemoriesInfoScript.LoadItems();
    }

    public void DetailView()
    {
        MemoryCategoryMenu.SetActive(false);
        MemoriesInfoPanel.SetActive(true);
        MemoriesBackButton.SetActive(true);
        switchButton.SetActive(false);
        MemoriesInfoScript.clearDetails();
    }

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        MemoriesInfoPanel.SetActive(false);
        audioComponent = this.GetComponent<AudioSource>();
        Hide();
    }
}
