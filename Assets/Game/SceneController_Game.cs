using UnityEngine.SceneManagement;
using Photon.Realtime;
using UnityEngine;
using Photon.Pun;

namespace Game {
    public class SceneController_Game : MonoBehaviourPunCallbacks
    {
        public static bool IsDebug = true; // выключить переменную при билде
        [SerializeField] private Transform parentSpawns; // Родительский объект всех мест спавна
        private static SceneController_Game instance;
        public static SceneController_Game Instance
        {
            get { 
                if (instance == null) instance = FindObjectOfType<SceneController_Game>();
                return instance;
            }
        }
        private void Awake()
        {
            SpawnPlayer();
        }
        private void SpawnPlayer()
        {
            if (IsDebug && PhotonNetwork.NetworkClientState == ClientState.PeerCreated) // Если мы не подключены к серверу и комнате, то подключаемся
            {
                PhotonNetwork.ConnectUsingSettings();
                return;
            }
            Vector3 pos = parentSpawns.GetChild(Random.Range(0, parentSpawns.childCount)).position;
            Player.LocalPlayer = PhotonNetwork.Instantiate("Player", pos, Quaternion.identity).GetComponent<Player>();
        }
        public override void OnConnectedToMaster()
        {
            PhotonNetwork.JoinOrCreateRoom("Game", new RoomOptions(), TypedLobby.Default); // Создать или подключиться к комнате
            base.OnConnectedToMaster();
        }
        public override void OnJoinedRoom()
        {
            base.OnJoinedRoom();
            SpawnPlayer();
        }
        public override void OnLeftRoom()
        {
            SceneManager.LoadScene("Lobby");
        }
    }
}