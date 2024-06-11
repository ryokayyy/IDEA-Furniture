using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FiltersModule : MonoBehaviour
{
    private DatabaseModule db;

    public GameObject List;

    public GameObject SearchBar;

    public GameObject WeightFilter;
    public GameObject WidthFilter;
    public GameObject DepthFilter;
    public GameObject HeightFilter;
    public GameObject MaterialFilter;
    public GameObject ColorFilter;

    public void ApplyFilters()
    {
        EventSystem.current.SetSelectedGameObject(null);

        IList<string> filterResults = Filter();
        IList<string> searchResults = SearchBar.GetComponent<SearchModule>().Search();

        IList<string> results = CheckSearch(filterResults, searchResults);
        List.GetComponent<ListControl>().ShowResults(results);
    }

    public void ResetFilters()
    {
        EventSystem.current.SetSelectedGameObject(null);

        Reset(WeightFilter);
        Reset(WidthFilter);
        Reset(DepthFilter);
        Reset(HeightFilter);
        Reset(MaterialFilter);
        Reset(ColorFilter);

        void Reset(GameObject obj)
        {
            var values = obj.GetComponentsInChildren<InputField>();

            for (int i = 0; i < values.Length; i++)
            {
                values[i].text = String.Empty;
            }
        }
    }

    public IList<string> Filter() => FindMatches(GetFiltersQueries(), SearchBar.GetComponent<SearchModule>().FindAll());

    private IList<string> FindMatches(IList<string> filters, IList<string> list)
    {
        if (filters.Count == 0)
            return list;

        db = new($"{Application.dataPath}/StreamingAssets/main_db.bytes");

        string query = "SELECT name\r\nFROM model\r\nWHERE (";
        for (int i = 0; i < filters.Count; i++)
        {
            query += filters[i];
            if (i < filters.Count - 1)
                query += " AND ";
        }
        query += ")";

        IList<string> results = db.GetRecords(query);
        db.Dispose();

        return results;
    }

    private IList<string> GetFiltersQueries()
    {
        List<string> filters = new();

        // Numeric filters

        AddFilter("weight>=", GetNumericFilter(WeightFilter)[0], String.Empty);
        AddFilter("weight<=", GetNumericFilter(WeightFilter)[1], String.Empty);
        AddFilter("width>=", GetNumericFilter(WidthFilter)[0], String.Empty);
        AddFilter("width<=", GetNumericFilter(WidthFilter)[1], String.Empty);
        AddFilter("depth>=", GetNumericFilter(DepthFilter)[0], String.Empty);
        AddFilter("depth<=", GetNumericFilter(DepthFilter)[1], String.Empty);
        AddFilter("height>=", GetNumericFilter(HeightFilter)[0], String.Empty);
        AddFilter("height<=", GetNumericFilter(HeightFilter)[1], String.Empty);

        // Text filters

        AddFilter("material='", GetTextFilter(MaterialFilter), "'");
        AddFilter("color='", GetTextFilter(ColorFilter), "'");

        return filters;

        void AddFilter(string pre_str, string str, string post_str)
        {
            if (String.IsNullOrEmpty(str))
                return;

            filters.Add(pre_str + str + post_str);
        }
    }

    private string[] GetNumericFilter(GameObject numericFilter)
    {
        var values = numericFilter.GetComponentsInChildren<InputField>();
        return new string[] { values[0].text, values[1].text };
    }

    private string GetTextFilter(GameObject textFilter) => textFilter.GetComponentInChildren<InputField>().text;

    private IList<string> CheckSearch(IList<string> filterResults, IList<string> searchResults)
    {
        List<string> results = new();
        for (int i = 0; i < filterResults.Count; i++)
        {
            if (searchResults.Contains(filterResults[i]))
                results.Add(filterResults[i]);
        }

        return results;
    }
}
