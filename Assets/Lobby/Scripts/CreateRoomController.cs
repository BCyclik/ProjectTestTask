using UnityEngine.UI;
using UnityEngine;
using TMPro;
namespace Lobby
{
    public class CreateRoomController : MonoBehaviour
    {
        [SerializeField] private string NameRoom = "None";
        [Space]
        [Header("Кнопка создания новой комнаты")]
        [SerializeField] private Button CreateRoom_Button;
        [Header("Поле для ввода названия комнаты")]
        [SerializeField] private TMP_InputField CreateRoom_InputField;

        public void Init(SceneController_Lobby lobbyController)
        {
            Debug.Log("[CreateRoomController] Init");
            CreateRoom_InputField.onValueChanged.AddListener(ChangeNameRoom);
            CreateRoom_Button.onClick.AddListener(() => lobbyController.CreateNewRoom(NameRoom));
        }
        /* Функция изменения текста в поле ввода названия комнаты */
        private void ChangeNameRoom(string nameRoom)
        {
            NameRoom = nameRoom;
        }
    }
}