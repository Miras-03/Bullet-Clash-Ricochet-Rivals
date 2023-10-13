using UnityEngine;

public class RoomItemButton : MonoBehaviour
{
    [HideInInspector] public string roomName;

    public void OnButtonPressed() => RoomList.Instance.JoinRoomByName(roomName);
}   
