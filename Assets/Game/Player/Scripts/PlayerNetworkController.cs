using Photon.Realtime;
using UnityEngine;
using Photon.Pun;

namespace Game
{
    public class PlayerNetworkController : MonoBehaviourPunCallbacks, IPunObservable
    {
        private Player playerController;
        private Player PlayerController
        {
            get
            {
                if (!playerController) playerController = GetComponent<Player>(); // Получить компонент в переменную
                return playerController;
            }
        }
        private void Awake()
        {
            
        }
        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            stream.Serialize(ref PlayerController.health); // Синхроизировать здоровье
        }
    }
}