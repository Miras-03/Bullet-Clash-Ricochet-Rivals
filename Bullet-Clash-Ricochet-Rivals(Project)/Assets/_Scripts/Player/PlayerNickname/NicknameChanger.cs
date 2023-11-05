using Photon.Pun;
using TMPro;
using UnityEngine;

public class NicknameChanger : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_InputField pickName;

    public void SetNickname() => PhotonNetwork.NickName = pickName.text;

    public string Nickname 
    {
        get => PhotonNetwork.NickName;
        set => PhotonNetwork.NickName = pickName.text;
    }
}
