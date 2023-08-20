using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace Wordly
{
    public static class PlayerManager
    {
        private static readonly string FilePath = $"{Application.StartupPath}\\player.dat";

        public static Player player { get; set; }

        static PlayerManager()
        {
            player = new Player();
        }

        public static void SavePlayerData()
        {
            try
            {
                using (FileStream fileStream = new FileStream(FilePath, FileMode.Create))
                {
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    binaryFormatter.Serialize(fileStream, player);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred while saving player data: " + e.Message);
            }
        }

        public static void LoadPlayerData()
        {
            try
            {
                using (FileStream fileStream = new FileStream(FilePath, FileMode.Open))
                {
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    player = (Player)binaryFormatter.Deserialize(fileStream);
                }
            }
            catch (FileNotFoundException)
            {
                // This is expected if the player data file does not exist yet
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred while loading player data: " + e.Message);
            }
        }
    }

    [Serializable]
    public class Player
    {
        public int WordLength { get; set; }
        public int Point { get; set; }

        public Player()
        {
            // Initialize properties
            WordLength = 0;
            Point = 0;
        }
    }
}