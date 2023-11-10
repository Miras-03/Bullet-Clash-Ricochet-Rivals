using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UISpace
{
    public sealed class UIRoomObserver : MonoBehaviour, IRoomObserver
    {
        [Header("UI Images")]
        [SerializeField] private Image[] uiImages;

        [Header("UI Texts")]
        [SerializeField] private TextMeshProUGUI[] texts;

        public void Execute()
        {
            EnableImages(uiImages);
            EnableTexts();
        }

        private void EnableImages(Image[] images)
        {
            foreach (Image image in images)
                image.enabled = true;
        }

        private void EnableTexts()
        {
            foreach (TextMeshProUGUI text in texts)
                text.enabled = true;
        }
    }
}