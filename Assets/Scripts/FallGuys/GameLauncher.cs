using Fusion; // Photon Fusion의 기능을 쓰기 위한 네임스페이스
using Fusion.Sockets;
using TMPro; // Text Mesh Pro의 기능을 쓰기 위한 네임스페이스
using System;
using System.Collections.Generic;
using System.Linq;
using Yukgaejang.FusionHelpers; // FusionLauncher.cs의 기능을 쓰기 위한 네임스페이스
using FallGuys.UI; // ErrorBox.cs의 기능을 쓰기 위한 네임스페이스
using UnityEngine;

// 접근할 네임스페이스 'Yukgaejang.FallGuys'로 지정
namespace Yukgaejang.FallGuys
{
    // App 엔트리 및 메인 UI 플로우 관리

    public class GameLauncher : MonoBehaviour
    {
        [SerializeField] private GameManager _gameManagerPrefab; // 게임매니저
        [SerializeField] private Player _playerPrefab;           // 플레이어
        [SerializeField] private Panel _uiMenu;                  // Menu UI를 저장
        [SerializeField] private Panel _uiLoad;                  // Load UI를 저장
        [SerializeField] private Panel _uiReady;                 // Ready UI를 저장
        [SerializeField] private Panel _uiFinish;                // Finish UI를 저장
        [SerializeField] private TMP_Text _loadMsg;           // Load Msg를 저장

        private FusionLauncher.ConnectionStatus _status = FusionLauncher.ConnectionStatus.Disconnected;
        private NetworkRunner _runner;
        private GameMode _gameMode; // 게임모드 (서버, 호스트, 클라이언트 등)
        private int MaxPlayer = 4;

        private void Awake()
        { // 싱글톤
            DontDestroyOnLoad(this);
        }

        private void Start()
        {
            OnConnectionStatusUpdate(null, FusionLauncher.ConnectionStatus.Disconnected, "");
        }

        private void Update()
        {
            
        }

        public void GetStartBtn() // Start 버튼 함수
        {
            // 게임 모드(호스트/클라이언트)를 결정해서 저장
            _gameMode = GameMode.AutoHostOrClient;

            // 퓨전 런처 생성 및 초기화
            FusionLauncher launcher = FindObjectOfType<FusionLauncher>();
            if (launcher == null)
                launcher = new GameObject("Launcher").AddComponent<FusionLauncher>();

            _loadMsg.text = $"{_runner.ActivePlayers.Count().ToString()} / {MaxPlayer.ToString()}";

            if (GateUI(_uiMenu))
                _uiLoad.SetVisible(true);
        }

        public void GetCancelBtn() // Cancel(게임 로딩 중) 버튼 함수
        {
            NetworkRunner runner = FindObjectOfType<NetworkRunner>();
            if (runner != null && !runner.IsShutdown)
            {
                runner.Shutdown(false);
            }

            _loadMsg.text = $"{_runner.ActivePlayers.Count().ToString()} / {MaxPlayer.ToString()}";

            if (GateUI(_uiLoad))
                _uiMenu.SetVisible(true);
        }

        private bool GateUI(Panel ui)
        {
            if (!ui.isShowing) // UI가 보이면
                return false; // false 반환
            ui.SetVisible(false); // UI가 보이면 안 보이게 만들고
            return true; // true 반환
        }

        private void OnConnectionStatusUpdate(NetworkRunner runner, FusionLauncher.ConnectionStatus status, string reason)
        {
            if (!this)
                return;

            Debug.Log(status);

            if (status != _status)
            {
                switch (status)
                {
                    case FusionLauncher.ConnectionStatus.Disconnected:
                        ErrorBox.Show("Disconnected!", reason, () => { });
                        break;
                    case FusionLauncher.ConnectionStatus.Failed:
                        ErrorBox.Show("Error!", reason, () => { });
                        break;
                }
            }

            _status = status;
        }
    }
}