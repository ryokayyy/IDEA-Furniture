using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SearchModule : MonoBehaviour
{
    public GameObject List;

    public GameObject FiltersMenu;

    public void ApplySearch()
    {
        IList<string> searchResults = Search();
        IList<string> filterResults = FiltersMenu.GetComponent<FiltersModule>().Filter();

        IList<string> results = CheckFilters(searchResults, filterResults);
        List.GetComponent<ListControl>().ShowResults(results);
    }

    public IList<string> Search() => FindPartialMatch(this.GetComponent<InputField>().text, FindAll());

    public IList<string> FindAll()
    {
        var objList = List.GetComponent<ListControl>().ListElements;
        List<string> results = new();

        foreach (var i in objList)
        {
            results.Add(i.name);
        }

        return results;
    }

    private IList<string> FindPartialMatch(string text, IList<string> list)
    {
        if (String.IsNullOrEmpty(text))
            return list;

        List<string> results = new();
        foreach (var i in list)
        {
            if (i.ToLower().Contains(text.ToLower()))
                results.Add(i);
        }

        return results;
    }

    private IList<string> CheckFilters(IList<string> searchResults, IList<string> filterResults)
    {
        List<string> results = new();
        for (int i = 0; i < searchResults.Count; i++)
        {
            if (filterResults.Contains(searchResults[i]))
                results.Add(searchResults[i]);
        }

        return results;
    }
}
