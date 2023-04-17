using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VideoOption : MonoBehaviour
{
    List<Resolution> _resolutions = new List<Resolution>(); // 해상도 리스트
    FullScreenMode _screenMode;                             // 전체화면 체크 여부를 저장
    public int _resolutionNum;                               // 선택한 해상도를 저장할 리스트
    public Toggle fullscreenBtn;                            // 전체화면 체크박스
    public TMP_Dropdown resolutionDropdown;                 // 해상도 선택 드롭다운

    void Start()
    {
        InitUI();
    }

    void InitUI()
    {
        _resolutions.Clear();

        // 유니티에서 지원하는 읽기 전용 해상도 값들을 읽어들여서
        for (int i = 0; i < Screen.resolutions.Length; i++)
        {
            // 읽어들인 해상도 값의 프레임이 60헤르츠면
            if (Screen.resolutions[i].refreshRate == 60)
                _resolutions.Add(Screen.resolutions[i]); // 해상도 리스트에 저장
        }

        // 드롭다운에 저장된 값 비우기
        resolutionDropdown.options.Clear();

        int optionNum = 0;

        // 해상도 리스트에 저장된 해상도 값들을 읽어들여서
        foreach (Resolution item in _resolutions)
        {
            // 해상도 선택 드롭다운에 저장
            TMP_Dropdown.OptionData option = new TMP_Dropdown.OptionData();
            option.text = item.width + "x" + item.height;
            resolutionDropdown.options.Add(option);

            if (item.width == Screen.width && item.height == Screen.height)
            {
                resolutionDropdown.value = optionNum;
                // _resolutionNum = optionNum;
            }

            optionNum++;
        }

        resolutionDropdown.RefreshShownValue();

        // _screenMode = Screen.fullScreenMode;
        fullscreenBtn.isOn = Screen.fullScreenMode.Equals(FullScreenMode.FullScreenWindow) ? true : false;
    }

    public void FullScreenBtn(bool isFull) // 체크박스에 연결
    {
        _screenMode = isFull ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
    }

    public void DropboxOptionChange(int x) // 드롭다운에 연결
    {
        _resolutionNum = x;
    }

    public void SaveVideoSettings()
    {
        Screen.SetResolution(_resolutions[_resolutionNum].width,
            _resolutions[_resolutionNum].height,
            _screenMode);
    }
}