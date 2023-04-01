using UnityEngine.UI;
using UnityEngine;
using TMPro;
namespace Lobby
{
    public class FindRoomController : MonoBehaviour
    {
        [SerializeField] private string NameRoom = "None";
        [Space]
        [Tooltip("Переменная кнопки поиск комнаты")]
        [SerializeField] private Button FindRoom_Button;
        [Tooltip("Поле для ввода названия комнаты")]
        [SerializeField] private TMP_InputField FindRoom_InputField;

        public void Init(SceneController_Lobby lobbyController)
        {
            Debug.Log("[FindRoomController] Init");
            FindRoom_InputField.onValueChanged.AddListener(ChangeNameRoom);
            FindRoom_Button.onClick.AddListener(() => lobbyController.FindRoom(NameRoom));
        }
        /* Функция изменения текста в поле ввода названия комнаты */
        private void ChangeNameRoom(string nameRoom)
        {
            NameRoom = nameRoom;
        }
    }
}