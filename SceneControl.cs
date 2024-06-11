using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{
    // Start is called before the first frame update
    public void Start() => Screen.SetResolution(1552, 720, false);

    public void SwitchScene(string sceneName)
    {
        if (IsExists(sceneName))
        {
            SceneManager.LoadScene(sceneName);
            Debug.Log($"Scene {sceneName} has been loaded");
        }
        else if (IsExists(sceneName + ViewInstructionControl.ModelID))
        {
            SceneManager.LoadScene(sceneName + ViewInstructionControl.ModelID);
            Debug.Log($"Instruction {sceneName} has been loaded");
        }
    }

    private bool IsExists(string sceneName)
    {
        if (SceneUtility.GetBuildIndexByScenePath(sceneName) > -1)
            return true;
        return false;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
