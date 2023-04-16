using UnityEngine;

public class SimpleBtnManager : MonoBehaviour
{
    public enum ButtonType
    {
        OpenPanel,
        ClosePanel,
        QuitGame
    }

    public ButtonType currentType;
    public Panel _panel;
    public Panel _exception01;
    public Panel _exception02;

    public void SimpleBtn()
    {
        switch (currentType)
        {
            case ButtonType.OpenPanel:
                _panel.SetVisible(true);
                _exception01.SetVisible(false);
                _exception02.SetVisible(false);
                break;

            case ButtonType.ClosePanel:
                _panel.SetVisible(false);
                break;

            case ButtonType.QuitGame:
                Application.Quit();
                Debug.Log("Quit Game");
                break;
        }
    }
}