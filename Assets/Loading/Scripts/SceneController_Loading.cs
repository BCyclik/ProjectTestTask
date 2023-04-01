using UnityEngine.SceneManagement;
using UnityEngine;
using Photon.Pun;
using TMPro;

namespace SceneLoading
{
    public class SceneController_Loading : MonoBehaviourPunCallbacks
    {
        [SerializeField] private PreloaderController _preloader;
        [SerializeField] private TMP_Text status_text; // 
        private void Awake()
        {
            Debug.Log("[SceneController_Loading] Awake");
            PhotonNetwork.AutomaticallySyncScene = true; // Синхронизация сцен
            PhotonNetwork.ConnectUsingSettings(); // Подключиться к серверу photon
        }
        /* Функция после подключения к мастер серверу photon */
        public override void OnConnectedToMaster()
        {
            Debug.Log("[SceneController_Loading] OnConnectedToMaster");
            base.OnConnectedToMaster();

            PhotonNetwork.JoinLobby(); // Подключиться к лобби
        }
        /* Функция после подключения к лобби */
        public override void OnJoinedLobby()
        {
            Debug.Log("[SceneController_Loading] OnJoinedLobby");
            base.OnJoinedLobby();

            SceneManager.LoadScene("Lobby"); // Перейти на сцену лобби
        }
    }
}