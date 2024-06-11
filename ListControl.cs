using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListControl : MonoBehaviour
{
    public Transform[] ListElements;

    private DatabaseModule db;

    public GameObject List;
    public GameObject ListElementPrefab;

    // Start is called before the first frame update
    public void Start()
    {
        db = new($"{Application.dataPath}/StreamingAssets/main_db.bytes");
        IList<string> model_names = db.GetRecords("SELECT name\r\nFROM model");
        IList<string> model_images = db.GetRecords("SELECT image_ref\r\nFROM model");
        db.Dispose();

        FillList(model_names, model_images);

        ListElements = this.gameObject.FindChildren();
    }

    public void FillList(IList<string> _nameslist, IList<string> _imgslist)
    {
        for (int i = 0; i < _nameslist.Count; i++)
        {
            GameObject _objelement = Instantiate(ListElementPrefab, List.transform, worldPositionStays:false);
            _objelement.name = _nameslist[i];

            var img = Resources.Load<Sprite>("Content/ImageData/" + _imgslist[i]);
            var object_images = _objelement.GetComponentsInChildren<Image>();
            object_images[1].sprite = img;

            var txt = _objelement.GetComponentInChildren<Text>();
            txt.text = _nameslist[i];
            txt.fontSize = 40;

            // _objelement.transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    public void ShowResults(IList<string> namesList)
    {
        for (int i = 0; i < ListElements.Length; i++)
        {
            if (namesList.Contains(ListElements[i].name))
                ListElements[i].gameObject.SetActive(true);
            else
                ListElements[i].gameObject.SetActive(false);
        }
    }
}
