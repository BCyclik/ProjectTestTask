using Photon.Realtime;
using UnityEngine;
using Photon.Pun;

namespace Game
{
    [RequireComponent(typeof(PhotonView))]
    public class PlayerNetworkController : MonoBehaviourPunCallbacks, IPunObservable
    {
        //[Header("Скорость сглаживания позиции и поворота")]
        //[SerializeField] private float speedLerp = 1.0f;
        //private Quaternion rot = Quaternion.identity;
        //private Vector3 pos = Vector3.zero;

        private Player playerController;
        private Player PlayerController
        {
            get
            {
                if (!playerController) playerController = GetComponent<Player>(); // Получить компонент
                return playerController;
            }
        }
        private void Awake()
        {
            
        }
        private void Update()
        {
            //if (!photonView.IsMine)
            //{
            //    transform.rotation = Quaternion.Lerp(transform.rotation, rot, speedLerp * Time.deltaTime);
            //    transform.position = Vector3.Lerp(transform.position, pos, speedLerp * Time.deltaTime);
            //    return;
            //}
            //rot = transform.rotation;
            //pos = transform.position;
        }
        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            //stream.Serialize(ref rot);
            //stream.Serialize(ref pos);
            stream.Serialize(ref PlayerController.coins); // Синхроизировать монеты

            if (stream.IsWriting)
            {
                stream.SendNext(PlayerController.Health); // Отправить здоровье
            }
            else
            {
                PlayerController.Health = (float)stream.ReceiveNext(); // Считать здоровье из потока
            }
        }
    }
}