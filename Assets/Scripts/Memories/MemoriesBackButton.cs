using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoriesBackButton : MonoBehaviour
{

    public AudioSource audioSource;
    public AudioClip pressSound;
    public AudioClip mouseOverSound;
    public MemoriesPanel memoriesPanel;
    public MemoriesMenuButton menuButton;

    private void Awake()
    {
        audioSource = this.GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        memoriesPanel = MemoriesPanel.instance;
    }

    public void GoBack()
    {
        menuButton.backButtonPress();
        memoriesPanel.Show();
    }

    public void MouseOver()
    {
        audioSource.volume = 1f;
        audioSource.clip = mouseOverSound;
        audioSource.Play();
    }
}
