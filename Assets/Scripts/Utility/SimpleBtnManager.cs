using UnityEngine;
using UnityEngine.SceneManagement;

public class SimpleBtnManager : MonoBehaviour
{
    public enum ButtonType
    {
        ChangeScene,
        OpenPanel,
        ClosePanel,
        QuitGame
    }

    public ButtonType currentType;
    public GameObject _panel;
    public GameObject _exception01;
    public GameObject _exception02;
    public string _sceneName;

    public void SimpleBtn()
    {
        switch (currentType)
        {
            case ButtonType.ChangeScene:
                SceneManager.LoadScene(_sceneName);
                Debug.Log("Change Scene");
                break;

            case ButtonType.OpenPanel:
                _panel.SetActive(true);
                _exception01.SetActive(false);
                _exception02.SetActive(false);
                Debug.Log("Open Panel");
                break;

            case ButtonType.ClosePanel:
                _panel.SetActive(false);
                Debug.Log("Close Panel");
                break;

            case ButtonType.QuitGame:
                Application.Quit();
                Debug.Log("Quit Game");
                break;
        }
    }
}