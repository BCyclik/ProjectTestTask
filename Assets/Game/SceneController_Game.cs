using UnityEngine.SceneManagement;
using System.Collections;
using Photon.Realtime;
using GameInterface;
using UnityEngine;
using Photon.Pun;
using UnityEngine.Events;

namespace Game {
    [RequireComponent(typeof(PhotonView))]
    public class SceneController_Game : MonoBehaviourPunCallbacks, IPunObservable
    {
        private static bool IsStartGame = false;
        [Header("Родительский объект для всех мест спавна")]
        [SerializeField] private Transform parentSpawns;
        [Header("Поле")]
        [SerializeField] private Transform background;
        [Header("Задержка появления монет")]
        [SerializeField] private float delaySpawnCoint = 2.2f;
        [Header("Родительский объект для монет")]
        [SerializeField] public Transform parentCoins;
        [Header("Префаб монеты")]
        [SerializeField] private CoinController coinPrefab;

        private int CountLifePlayers = 1;

        const int MaxCoinsInMap = 10; // Максимальное кол-во монет на карте за раз
        public int CountCoinNow = 0; // Кол-во монет на карте сейчас

        private Vector2 minPos, maxPos; // Для вычисления позиции появления новой монеты

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
            GetSpawnCoinsZone();
            SpawnPlayer();
        }
        private void GetSpawnCoinsZone()
        {
            Vector2 pos1 = background.GetChild(0).position;
            Vector2 pos2 = background.GetChild(1).position;
            if (pos1.x < pos2.x)
            {
                minPos.x = pos1.x;
                maxPos.x = pos2.x;
            }
            else
            {
                minPos.x = pos2.x;
                maxPos.x = pos1.x;
            }
            if (pos1.y < pos2.y)
            {
                minPos.y = pos1.y;
                maxPos.y = pos2.y;
            }
            else
            {
                minPos.y = pos2.y;
                maxPos.y = pos1.y;
            }
        }
        private void SpawnPlayer()
        {
            Vector3 pos = parentSpawns.GetChild(Random.Range(0, parentSpawns.childCount)).position;
            Player.LocalPlayer = PhotonNetwork.Instantiate("Player", pos, Quaternion.identity).GetComponent<Player>();
        }
        public override void OnJoinedRoom()
        {
            Debug.Log("[SceneController_Game] OnJoinedRoom");
            base.OnJoinedRoom();
        }
        public override void OnLeftRoom()
        {
            Debug.Log("[SceneController_Game] OnLeftRoom");
            IsStartGame = false;
        }
        public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
        {
            Debug.Log("[SceneController_Game] OnPlayerEnteredRoom");
            base.OnPlayerEnteredRoom(newPlayer);

            if (!photonView.IsMine) return; // Если не мастер сервер - не продолжать
            CountLifePlayers++; // Добавить живого игрока

            if (IsStartGame) return; // Если игра запущена - не прдолжать
            if (PhotonNetwork.CurrentRoom.PlayerCount <= 1) return; // Если игроков не хватает - не продолжать
            StartGame(); // Запустить игру
        }
        /* Функция начала игры (боя) */
        private void StartGame()
        {
            Debug.Log("[SceneController_Game] StartGame");

            IsStartGame = true; // Показать что игра запущена

            StartCoroutine(DelaySpawnCoin()); // Запустить цикл появления монет с задержкой
            ScatterCoins(); // Разбросать начальные монеты
        }
        public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
        {
            Debug.Log("[SceneController_Game] OnPlayerLeftRoom");
            base.OnPlayerLeftRoom(otherPlayer);

            if (!photonView.IsMine) return; // Если не мастер сервер - не продолжать
            if (!IsStartGame) return; // Если игра не запущена - не прдолжать
            if (PhotonNetwork.CurrentRoom.PlayerCount > 1) return; // Если игроков хватает - не продолжать
            IsStartGame = false;
            EndGame(true); // Закончить игру
        }
        /* Функция конца игры */
        public void EndGame(bool isWin)
        {
            Debug.Log("[SceneController_Game] EndGame");

            if (isWin) GUIController.Instance.endGamePanel.ShowWinPanel(); // Показать экран победы
            else GUIController.Instance.endGamePanel.ShowLosePanel(); // Показать экран поражения
            PhotonNetwork.LeaveRoom();
        }
        /* Функция спавна монеты */
        private void SpawnCoin()
        {
            float x = Random.Range(minPos.x + 1, maxPos.x / 2 - 1); // Случайный X в диапазоне поля
            float y = Random.Range(minPos.y + 1, maxPos.y - 1); // Случайный Y в диапазоне поля
            PhotonNetwork.InstantiateRoomObject(coinPrefab.name, new Vector2(x, y), Quaternion.identity); // Создать монету

            Debug.Log($"[SceneController_Game] SpawnCoin - {x} : {y}");
        }
        /* Функция создания новых монет с задержкой */
        private IEnumerator DelaySpawnCoin()
        {
            while (IsStartGame) // Цикл действует пока игра запущена
            {
                yield return new WaitForSeconds(delaySpawnCoint); // Задержка до появления новой монеты
                if (!IsStartGame) yield return null; // отменить цикл, если игра завершена
                int countCoins = Random.Range(2, 5); // Получить случайное число монет
                ScatterCoins(countCoins); // Разбросать новые монеты
            }
        }
        /* Функция разбразывания монет */
        private void ScatterCoins(int countCoins = 5)
        {
            Debug.Log("[SceneController_Game] ScatterCoins");

            for (int i = 0; i < countCoins; i++)
            {
                if (!IsStartGame) return; // Если не запущена - не продолжать
                if (CountCoinNow >= MaxCoinsInMap) return; // Проверить кол-во монет на карте
                SpawnCoin(); // Заспавнить новую монету
            }
        }
        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            stream.Serialize(ref CountLifePlayers); // Синхронизировать переменную в сети
            stream.Serialize(ref IsStartGame); // Синхронизировать переменную в сети
        }
    }
}