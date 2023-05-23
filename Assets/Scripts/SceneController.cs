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
                SceneManager.LoadScene("EasyScene");
                break;
            case "Hard":
                SceneManager.LoadScene("HardScene");
                break;
            default:
                SceneManager.LoadScene("TitleScene");
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