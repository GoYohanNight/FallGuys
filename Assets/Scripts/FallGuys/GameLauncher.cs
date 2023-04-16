using Fusion; // Photon Fusion�� ����� ���� ���� ���ӽ����̽�
using Fusion.Sockets;
using TMPro; // Text Mesh Pro�� ����� ���� ���� ���ӽ����̽�
using System;
using System.Collections.Generic;
using System.Linq;
using Yukgaejang.FusionHelpers; // FusionLauncher.cs�� ����� ���� ���� ���ӽ����̽�
using FallGuys.UI; // ErrorBox.cs�� ����� ���� ���� ���ӽ����̽�
using UnityEngine;

// ������ ���ӽ����̽� 'Yukgaejang.FallGuys'�� ����
namespace Yukgaejang.FallGuys
{
    // App ��Ʈ�� �� ���� UI �÷ο� ����

    public class GameLauncher : MonoBehaviour
    {
        [SerializeField] private GameManager _gameManagerPrefab; // ���ӸŴ���
        [SerializeField] private Player _playerPrefab;           // �÷��̾�
        [SerializeField] private Panel _uiMenu;                  // Menu UI�� ����
        [SerializeField] private Panel _uiLoad;                  // Load UI�� ����
        [SerializeField] private Panel _uiReady;                 // Ready UI�� ����
        [SerializeField] private Panel _uiFinish;                // Finish UI�� ����
        [SerializeField] private TMP_Text _loadMsg;           // Load Msg�� ����

        private FusionLauncher.ConnectionStatus _status = FusionLauncher.ConnectionStatus.Disconnected;
        private NetworkRunner _runner;
        private GameMode _gameMode; // ���Ӹ�� (����, ȣ��Ʈ, Ŭ���̾�Ʈ ��)
        private int MaxPlayer = 4;

        private void Awake()
        { // �̱���
            DontDestroyOnLoad(this);
        }

        private void Start()
        {
            OnConnectionStatusUpdate(null, FusionLauncher.ConnectionStatus.Disconnected, "");
        }

        private void Update()
        {
            
        }

        public void GetStartBtn() // Start ��ư �Լ�
        {
            // ���� ���(ȣ��Ʈ/Ŭ���̾�Ʈ)�� �����ؼ� ����
            _gameMode = GameMode.AutoHostOrClient;

            // ǻ�� ��ó ���� �� �ʱ�ȭ
            FusionLauncher launcher = FindObjectOfType<FusionLauncher>();
            if (launcher == null)
                launcher = new GameObject("Launcher").AddComponent<FusionLauncher>();

            _loadMsg.text = $"{_runner.ActivePlayers.Count().ToString()} / {MaxPlayer.ToString()}";

            if (GateUI(_uiMenu))
                _uiLoad.SetVisible(true);
        }

        public void GetCancelBtn() // Cancel(���� �ε� ��) ��ư �Լ�
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
            if (!ui.isShowing) // UI�� ���̸�
                return false; // false ��ȯ
            ui.SetVisible(false); // UI�� ���̸� �� ���̰� �����
            return true; // true ��ȯ
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