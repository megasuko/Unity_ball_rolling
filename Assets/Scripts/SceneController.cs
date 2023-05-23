using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;
    public void OnClickGameStartButton(string level)
    {
        switch (level)
        {
            case "Easy":
                SceneManager.LoadScene("MainScene");
                break;
            case "Hard":
                SceneManager.LoadScene("MainScene");
                break;
            default:
                SceneManager.LoadScene("MainScene");
                break;
        }
    }

    public void OnClickToTitleButton()
    {
        SceneManager.LoadScene("TitleScene");
    }

    public void ResultFade()
    {
        Initiate.Fade("ResultScene", Color.black, 1.0f);

    }
}