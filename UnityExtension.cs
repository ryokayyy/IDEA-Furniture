using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UnityExtension
{
    public static Transform[] FindChildren(this GameObject obj)
    {
        var objs = new Transform[obj.transform.childCount];

        for (int i = 0; i < obj.transform.childCount; i++)
        {
            objs[i] = obj.transform.GetChild(i);
        }

        return objs;
    }
}
