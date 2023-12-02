using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class LockPanel : MonoBehaviour
{
    [SerializeField]
    private GameObject _panel;

    public bool IsVisible => _panel.activeSelf;

    private void OnValidate()
    {
        Image image = GetComponent<Image>();

        if(!image.raycastTarget)
        {
            image.raycastTarget = true;
        }
    }

    public void SetVisible(bool isVisible)
    {
        _panel.SetActive(isVisible);
    }
}
