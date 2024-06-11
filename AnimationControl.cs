using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AnimationControl : MonoBehaviour
{
    public GameObject Model;

    private Transform[] Models;

    private int currentStage_;

    public int CurrentStage
    {
        get { return currentStage_; }
        set
        {
            if (value > -1 && value < Models.Length)
                currentStage_ = value;
        }
    }

    // Start is called before the first frame update
    public void Start()
    {
        FindDetails();

        GameObject.Find(">").GetComponent<Button>().onClick.AddListener(() => Next());
        GameObject.Find("<").GetComponent<Button>().onClick.AddListener(() => Previous());
        GameObject.Find("Play").GetComponent<Button>().onClick.AddListener(() => PlayAnim());
    }

    public void Next()
    {
        EventSystem.current.SetSelectedGameObject(null);

        if (++CurrentStage < Models.Length)
            Models[CurrentStage].gameObject.SetActive(true);
        UpdateText();
    }

    public void Previous()
    {
        EventSystem.current.SetSelectedGameObject(null);

        Models[CurrentStage--].gameObject.SetActive(false);
        UpdateText();
    }

    public void PlayAnim()
    {
        EventSystem.current.SetSelectedGameObject(null);

        Models[CurrentStage].gameObject.SetActive(false);
        Models[CurrentStage].gameObject.SetActive(true);
    }

    private void FindDetails()
    {
        Models = Model.FindChildren();
        foreach (var model in Models)
            model.gameObject.SetActive(false);
    }

    private void UpdateText() => this.gameObject.GetComponentsInChildren<TextMeshProUGUI>()[0].text = $"{CurrentStage + 1}/{Models.Length}";
}
