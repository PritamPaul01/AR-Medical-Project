using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Menu : MonoBehaviour
{
    [Header("Space between menu items")]
    [SerializeField] Vector2 spacing;

    [Space]
    [Header("Main button rotation")]
    [SerializeField] float rotationDuration;
    [SerializeField] Ease rotationEase;

    [Space]
    [Header("Animation")]
    [SerializeField] float expandDuration;
    [SerializeField] float collapseDuration;
    [SerializeField] Ease expandEase;
    [SerializeField] Ease collapseEase;

    [Space]
    [Header("Fading")]
    [SerializeField] float expandFadeDuration;
    [SerializeField] float collapseFadeDuration;
    Button mainButton;
    MenuItem[] menuItems;
    bool isExpanded = false;

    Vector2 mainButtonPosition;
    int itemCount;

    void Start()
    {
        itemCount = transform.childCount - 1;
        menuItems = new MenuItem[itemCount];
        for (int i = 0; i < itemCount; i++)
        {
            menuItems[i] = transform.GetChild(i + 1).GetComponent<MenuItem>();
        }

        mainButton = transform.GetChild(0).GetComponent<Button>();
        mainButton.onClick.AddListener(ToggleMenu);
        mainButton.transform.SetAsLastSibling();

        mainButtonPosition = mainButton.transform.position;

        ResetPosition();
    }

    void ResetPosition()
    {
        for (int i = 0; i < itemCount; i++)
        {
            menuItems[i].trans.position = mainButtonPosition;
        }
    }

    void ToggleMenu()
    {
        isExpanded = !isExpanded;

        if (isExpanded)
        {
            for (int i = 0; i < itemCount; i++)
            {
                menuItems [ i ].trans.DOMove (mainButtonPosition + spacing * (i + 1), expandDuration).SetEase (expandEase) ;
                menuItems [ i ].img.DOFade (1f, expandFadeDuration).From (0f) ;
            }
        }
        else
        {
            for (int i = 0; i < itemCount; i++)
            {
                menuItems [ i ].trans.DOMove (mainButtonPosition + spacing * (i + 1), collapseDuration).SetEase (collapseEase) ;
                menuItems [ i ].img.DOFade (0f, collapseFadeDuration) ;
            }
        }

        mainButton.transform
			.DORotate (Vector3.forward * 180f, rotationDuration)
			.From (Vector3.zero)
			.SetEase (rotationEase) ;
    }

    void OnDestroy()
    {
        mainButton.onClick.RemoveListener(ToggleMenu);
    }
}
