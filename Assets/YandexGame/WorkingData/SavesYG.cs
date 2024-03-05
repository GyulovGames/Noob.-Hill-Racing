
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
        public int Coins_sdk = 500;
        public bool Sounds_sdk = true;
        public bool Music_sdk = true;
        public int SelectedCarIndex_sdk = 0;
        public bool[] Cars_sdk = new bool[9];
        // ...

        // Car1 
        public int[] Car1_UpgradeLevels = new int[9];
        public float[] Car1_Parts = new float[4];

        //Car2
        public int[] Car2_UpgradeLevels = new int[9];
        public float[] Car2_Parts = new float[4];



        public SavesYG()
        {
            Cars_sdk[0] = true;
        }
    }
}
