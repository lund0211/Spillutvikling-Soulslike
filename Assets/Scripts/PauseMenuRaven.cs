
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PauseMenuRaven : MonoBehaviour
{
    [Header("Raven")]
    public RectTransform raven;
    public float moveSpeed = 10f;
    public float horizontalOffset = -80f;

    [Header("Pause Menu Buttons")]
    public Button[] buttons;

    private RectTransform target;
    private Button currentButton;

    void Start()
    {
        if (buttons.Length > 0)
        {
            SelectButton(buttons[0]);
        }
    }

    void Update()
    {
        GameObject selected =
            EventSystem.current.currentSelectedGameObject;

        if (selected != null)
        {
            Button button = selected.GetComponent<Button>();

            if (button != null && button != currentButton)
            {
                SelectButton(button);
            }
        }

        if (target != null && raven != null)
        {
            Vector3 targetPosition = target.position +
                target.TransformVector(
                    new Vector3(horizontalOffset, 0, 0));

            raven.position = Vector3.Lerp(
                raven.position,
                targetPosition,
                moveSpeed * Time.unscaledDeltaTime
            );
        }
    }

    public void SelectButton(Button button)
    {
        if (button == null) return;

        currentButton = button;
        target = button.GetComponent<RectTransform>();

        EventSystem.current.SetSelectedGameObject(
            button.gameObject);
    }
}
