
namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        // "Технические сохранения" для работы плагина (Не удалять)
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;



        // Ваши сохранения
        public int unlocckedTrails = 1;
        public bool sounds = true;
        public bool music = true;
        // ...

        // Характеристики вагонетки
        public float minecartEnginePower = 100f;
        public float minecartFrontDirve = 0f;
        public float minecartSuspensionStab = 0f;
        public float minecartMaxFuelAmount = 100f;
        public float minecartWheelsGrip = 0.2f;
        //
    }
}
