using UnityEngine;

public class SideMenuReference : MonoBehaviour
{
    public GameObject SideMenu;
    public GameObject InstructionButton;

    // Start is called before the first frame update
    public void Start()
    {
        SideMenu.SetActive(false);
        InstructionButton.SetActive(false);
    }
}