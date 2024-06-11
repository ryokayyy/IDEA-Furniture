using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SideMenuControl : MonoBehaviour
{
    private DatabaseModule db;

    public SideMenuReference SMRef;

    // OnEnable is called when the object becomes enabled and active
    public void OnEnable()
    {
        SMRef = GameObject.Find("ReferenceController").GetComponent<SideMenuReference>();
    }

    // OnDisable is called when the behaviour becomes disabled
    public void OnDisable()
    {
        SMRef = null;
    }

    public void OpenMenu()
    {
        SMRef.SideMenu.SetActive(true);
        SMRef.InstructionButton.SetActive(true);
    }

    public void HideMenu()
    {
        if (!IsPointerOverObject("InstructionButton"))
        {
            SMRef.SideMenu.SetActive(false);
            SMRef.InstructionButton.SetActive(false);
        }
    }

    public void FillInfo()
    {
        string model_name = this.transform.name;

        var model_properties = SMRef.SideMenu.GetComponentsInChildren<TextMeshProUGUI>();
        ResetInfo(model_properties);
        model_properties[0].text = model_name;

        db = new($"{Application.dataPath}/StreamingAssets/main_db.bytes");

        IList<string> records = db.GetRecords(String.Format("SELECT material, color, weight, width, depth, height, guarantee, country, details, image_ref" +
                "\r\nFROM model" +
                "\r\nWHERE (name = '{0}')", model_name));
        IList<string> id = db.GetRecords(String.Format("SELECT id" +
                "\r\nFROM model" +
                "\r\nWHERE (name = '{0}')", model_name));

        db.Dispose();

        ViewInstructionControl.GetCurrentModel(id[0]);

        var properties_text = records[0].Split(",");
        var img = Resources.Load<Sprite>("Content/ImageData/" + properties_text.Last());
        var object_image = SMRef.SideMenu.GetComponentsInChildren<Image>();
        object_image[1].sprite = img;

        for (int i = 0; i < properties_text.Length - 1; i++)
        {
            model_properties[i + 1].text += properties_text[i];
        }
    }

    private void ResetInfo(TextMeshProUGUI[] props)
    {
        props[1].text = "Материал: ";
        props[2].text = "Цвет: ";
        props[3].text = "Вес (кг): ";
        props[4].text = "Ширина (см): ";
        props[5].text = "Глубина (см): ";
        props[6].text = "Высота (см): ";
        props[7].text = "Гарантия (мес.): ";
        props[8].text = "Страна: ";
        props[9].text = "Количество деталей: ";
    }

    private bool IsPointerOverObject(string objectName)
    {
        PointerEventData pointerEventData = new(EventSystem.current)
        {
            position = new Vector2(Input.mousePosition.x, Input.mousePosition.y)
        };
        List<RaycastResult> results = new();
        EventSystem.current.RaycastAll(pointerEventData, results);

        for (int i = 0; i < results.Count; i++)
        {
            if (results[i].gameObject.transform.name == objectName)
                return true;
        }

        return false;
    }
}
