using UnityEngine;
using UnityEngine.EventSystems;

public class FiltersMenuControl : MonoBehaviour
{
    public GameObject FiltersMenu;

    // OnEnable is called when the object becomes enabled and active
    public void OnEnable() => FiltersMenu.SetActive(false);

    public void OpenCloseMenu()
    {
        EventSystem.current.SetSelectedGameObject(null);
        FiltersMenu.SetActive(FiltersMenu.activeSelf ? false : true);
    }
}
