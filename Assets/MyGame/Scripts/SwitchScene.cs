using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameScenes
{
    ModiDesktop,
    PlayerInfoDesktop,
    NetworkConnectInfoDesktop
}

public class SwitchScene : MonoBehaviour
{
    public GameScenes nextScene;

    public void SwitchTheScene()
    {
        SceneManager.LoadScene(nextScene.ToString());
    }
}
