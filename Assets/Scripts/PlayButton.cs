using UnityEngine;

public class PlayButton : MonoBehaviour
{
    public GameObject[] uiButtons;
    public GameObject InfoButton;
    public GameObject ModelButton;
    public ObjectRotation objectRotation;

    private bool isPlaying = false;
    private bool isObjectVisible = true;

    private void Start()
    {
        ToggleUI();
    }

    public void ToggleUI()
    {
        isPlaying = !isPlaying;

        foreach (GameObject button in uiButtons)
        {
            button.SetActive(isPlaying);
        }

        ModelButton.SetActive(!isPlaying);
        InfoButton.SetActive(!isPlaying);
        objectRotation.enabled = isPlaying;

        if (!isPlaying)
        {
            objectRotation.ResetObject();
            SetObjectVisibility(true);
        }
        else
        {
            SetObjectVisibility(isObjectVisible);
        }
    }

    public void SetObjectVisibility(bool isVisible)
    {
        isObjectVisible = isVisible;
        objectRotation.gameObject.SetActive(isObjectVisible);
    }
}