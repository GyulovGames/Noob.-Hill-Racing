
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
        public bool Music_sdk = true;
        public bool Sounds_sdk = true;
        public int Coins_sdk = 500;
        public int LastSelectedCar_sdk = 0;
        public int LastSelectedTrail_sdk = 1;
        public bool[] FreeCaras_sdk = new bool[6];
        public bool[] FreeTrails_sdk = new bool[9];

        public int[] Car0_Upgrades = new int[5];
        public int[] Car1_Upgrades = new int[5];
        public int[] Car2_Upgrades = new int[5];
        public int[] Car3_Upgrades = new int[5];
        public int[] Car4_Upgrades = new int[5];
        public int[] Car5_Upgrades = new int[5];
        public int[] Car6_Upgrades = new int[5];
        // ...

        public SavesYG()
        {
            FreeCaras_sdk[0] = true;
            FreeTrails_sdk[1] = true;

            Car0_Upgrades[0] = 1;
            Car0_Upgrades[1] = 1;
            Car0_Upgrades[2] = 1;
            Car0_Upgrades[3] = 1;
            Car0_Upgrades[4] = 1;

            Car1_Upgrades[0] = 1;
            Car1_Upgrades[1] = 1;
            Car1_Upgrades[2] = 1;
            Car1_Upgrades[3] = 1;
            Car1_Upgrades[4] = 1;

            Car2_Upgrades[0] = 1;
            Car2_Upgrades[1] = 1;
            Car2_Upgrades[2] = 1;
            Car2_Upgrades[3] = 1;
            Car2_Upgrades[4] = 1;

            Car3_Upgrades[0] = 1;
            Car3_Upgrades[1] = 1;
            Car3_Upgrades[2] = 1;
            Car3_Upgrades[3] = 1;
            Car3_Upgrades[4] = 1;

            Car4_Upgrades[0] = 1;
            Car4_Upgrades[1] = 1;
            Car4_Upgrades[2] = 1;
            Car4_Upgrades[3] = 1;
            Car4_Upgrades[4] = 1;

            Car5_Upgrades[0] = 1;
            Car5_Upgrades[1] = 1;
            Car5_Upgrades[2] = 1;
            Car5_Upgrades[3] = 1;
            Car5_Upgrades[4] = 1;

            Car6_Upgrades[0] = 1;
            Car6_Upgrades[1] = 1;
            Car6_Upgrades[2] = 1;
            Car6_Upgrades[3] = 1;
            Car6_Upgrades[4] = 1;
        }
    }
}
