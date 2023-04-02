using UnityEngine.SceneManagement;
using UnityEngine;
using Photon.Pun;
using TMPro;

namespace Loading
{
    public class SceneController_Loading : MonoBehaviourPunCallbacks
    {
        [Header("Контроллер прелоудера")]
        [SerializeField] private PreloaderController _preloader;
        [Header("Текст статуса подключения")]
        [SerializeField] private TMP_Text status_text;
        [Header("Панель записи имени игрока")]
        [SerializeField] private NickNamePlayerPanel nickNamePlayerPanel;
        private void Awake()
        {
            Debug.Log("[SceneController_Loading] Awake");
            nickNamePlayerPanel.gameObject.SetActive(false);

            status_text.SetText("Подключение к серверу..."); // Вывести статус подключения
            PhotonNetwork.AutomaticallySyncScene = true; // Синхронизация сцен
            PhotonNetwork.ConnectUsingSettings(); // Подключиться к серверу photon
        }
        /* Функция после подключения к мастер серверу photon */
        public override void OnConnectedToMaster()
        {
            Debug.Log("[SceneController_Loading] OnConnectedToMaster");
            status_text.SetText("Подключение к лобби..."); // Вывести статус подключения
            base.OnConnectedToMaster();

            PhotonNetwork.JoinLobby(); // Подключиться к лобби
        }
        /* Функция после подключения к лобби */
        public override void OnJoinedLobby()
        {
            Debug.Log("[SceneController_Loading] OnJoinedLobby");
            status_text.SetText("Вход..."); // Вывести статус подключения
            base.OnJoinedLobby();

            if (PlayerPrefs.HasKey("NickName"))
            {
                PhotonNetwork.NickName = PlayerPrefs.GetString("NickName"); // Вытащить nickname игрока
                SceneManager.LoadScene("Lobby"); // Перейти на сцену лобби
            } 
            else
            {
                nickNamePlayerPanel.Init(); // Открыть окно выбора nickname
            }
        }
    }
}