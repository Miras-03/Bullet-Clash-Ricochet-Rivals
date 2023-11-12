namespace UISpace
{
    public sealed class UIRoomObserver : IRoomObserver
    {
        private ImageSingleton imageSingleton;
        private TextMeshProSingleton textMeshProSingleton;
        private Crosshair crosshair;

        public UIRoomObserver()
        {
            imageSingleton = ImageSingleton.Instance;
            textMeshProSingleton = TextMeshProSingleton.Instance;
            crosshair = Crosshair.Instance;
        }

        public void Execute()
        {
            EnableImages();
            EnableTexts();
        }

        private void EnableImages()
        {
            imageSingleton.HealthAmount.enabled = true;
            imageSingleton.HealthBG.enabled = true;
            imageSingleton.AmmoAmountInCircle.enabled = true;
            imageSingleton.AmmoBGInCircle.enabled = true;

            crosshair.DisableCrosshairs();
        }

        private void EnableTexts()
        {
            textMeshProSingleton.AmmoQuantityInText.enabled = true;
            textMeshProSingleton.MagQuantityInText.enabled = true;
        }
    }
}