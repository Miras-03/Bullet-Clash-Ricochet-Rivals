using Photon.Pun;
using TMPro;
using Zenject;

namespace Nickname
{
    public sealed class NicknameChanger : MonoBehaviourPunCallbacks
    {
        private TMP_InputField pickName;

        [Inject]
        public void Constructor(TMP_InputField pickName) => this.pickName = pickName;

        public void SetNickname() => PhotonNetwork.NickName = pickName.text;

        public string Nickname
        {
            get => PhotonNetwork.NickName;
            set => PhotonNetwork.NickName = pickName.text;
        }
    }
}