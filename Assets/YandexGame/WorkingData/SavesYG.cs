
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



        // Обшие данные
        public int savedCoins = 500;
        public bool sounds = true;
        public bool music = true;
    
        public bool[] unlockedVehicles = new bool[2];
        // ...




        //  Minecart4x4
        public int[] minecart_4x4_UpgradeLevels = new int[9];
        public float[] minecart4x4Part = new float[5];

        //

        //  Minecart6x6
        public int[] minecart_6x6_UpgradesLevel = new int[4];
        public float[] minecart6x6Upgrades = new float[5];

    }
}
