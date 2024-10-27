using Game.Events;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class LobbyUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _lobbyCodeText;

        [SerializeField] private Button _readyButton;
        [SerializeField] private Button _startButton;

        private void OnEnable()
        {
            _readyButton.onClick.AddListener(OnReadyPressed);

            if (GameLobbyManager.Instance.IsHost)
            {
                LobbyEvents.OnLobbyReady += OnLobbyReady;
            }
        }

        private void OnDisable()
        {
            _readyButton.onClick.RemoveAllListeners();

            LobbyEvents.OnLobbyReady -= OnLobbyReady;
        }

        private void Start()
        {
            _lobbyCodeText.text = $"Lobby Code: {GameLobbyManager.Instance.GetLobbyCode()}";
        }

        private async void OnReadyPressed()
        {
            bool succeeded = await GameLobbyManager.Instance.SetPlayerReady();

            if (succeeded)
            {
                _readyButton.gameObject.SetActive(false);
            }
        }

        private void OnLobbyReady()
        {
            _startButton.gameObject.SetActive(true);
        }
    }
}
