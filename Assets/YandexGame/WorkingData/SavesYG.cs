
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
        public bool Music_sdk;
        public bool Sounds_sdk;
        public int Coins_sdk = 1000;
        public int LastSelectedCar_sdk = 0;
        public bool[] FreeCaras_sdk = new bool[9];

        public int[] Car0_Upgrades = new int[5];
        public int[] Car1_Upgrades = new int[5];
        public int[] Car2_Upgrades = new int[5];
        public int[] Car3_Upgrades = new int[5];
        public int[] Car4_Upgrades = new int[5];
        public int[] Car5_Upgrades = new int[5];
        public int[] Car6_Upgrades = new int[5];
        public int[] Car7_Upgrades = new int[5];
        public int[] Car8_Upgrades = new int[5];
        public int[] Car9_Upgrades = new int[5];
        // ...

        public SavesYG()
        {
            Car0_Upgrades[3] = 5;
            FreeCaras_sdk[0] = true;
        }
    }
}
