using UnityEngine.SceneManagement;
using UnityEngine;
using Photon.Pun;


namespace Lobby
{
    public class SceneController_Lobby : MonoBehaviourPunCallbacks
    {
        [Tooltip("Переменная контроллера создания комнаты")]
        [SerializeField] private CreateRoomController createRoomController;
        [Tooltip("Переменная контроллера поиска комнаты")]
        [SerializeField] private FindRoomController findRoomController;

        private static SceneController_Lobby instance;
        public static SceneController_Lobby Instance
        {
            get
            {
                if (instance == null) instance = FindObjectOfType<SceneController_Lobby>();
                return instance;
            }
        }
        private void Awake()
        {
            Debug.Log("[SceneController_Lobby] Awake");
            createRoomController.Init(this); // Вызвать инит у контроллера создания комнаты
            findRoomController.Init(this); // Вызвать инит у контроллера поиска комнаты
        }
        /* Функция создания новой комнаты */
        public void CreateNewRoom(string nameRoom)
        {
            Debug.Log("[SceneController_Lobby] CreateNewRoom");
            PhotonNetwork.CreateRoom(nameRoom); // Создать комнату
        }
        /* Функция поиска комнаты по названию */
        public void FindRoom(string nameRoom)
        {
            Debug.Log("[SceneController_Lobby] FindRoom");
            PhotonNetwork.JoinRoom(nameRoom); // Подключиться к комнате
        }
        public override void OnJoinedRoom()
        {
            Debug.Log("[SceneController_Lobby] OnJoinedRoom");
            base.OnJoinedRoom();

            SceneManager.LoadScene("Game"); // Перейти в другую сцену
        }
    }
}