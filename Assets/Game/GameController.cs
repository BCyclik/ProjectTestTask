using UnityEngine.SceneManagement;
using Photon.Realtime;
using UnityEngine;
using Photon.Pun;

namespace Game {
    public class GameController : MonoBehaviourPunCallbacks
    {
        public static bool IsDebug = false;
        [SerializeField] private Transform parentSpawns;
        private static GameController instance;
        public static GameController Instance
        {
            get { 
                if (instance == null) instance = FindObjectOfType<GameController>();
                return instance;
            }
        }
        private void Awake()
        {
            SpawnPlayer();
        }
        private void SpawnPlayer()
        {
            if (IsDebug && PhotonNetwork.NetworkClientState == ClientState.PeerCreated)
            {
                PhotonNetwork.ConnectUsingSettings();
                return;
            }
            Vector3 pos = parentSpawns.GetChild(Random.Range(0, parentSpawns.childCount)).position; // �������� ������� ���������� ������
            Player.LocalPlayer = PhotonNetwork.Instantiate("Player", pos, Quaternion.identity).GetComponent<Player>(); // ���������� ������
        }
        public override void OnConnectedToMaster()
        {
            PhotonNetwork.JoinOrCreateRoom("Game", new RoomOptions(), TypedLobby.Default); // ����������� � ������� �������
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