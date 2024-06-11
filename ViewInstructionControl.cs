using UnityEngine;

public class ViewInstructionControl : MonoBehaviour
{
    public static string ModelID;

    public static void GetCurrentModel(string id) => ModelID = id;
}
