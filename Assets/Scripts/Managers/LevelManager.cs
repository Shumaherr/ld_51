using UnityEngine;

namespace Managers
{
    public class LevelManager : MonoBehaviour
    {
        public LevelGenerator levelGenerator; // Reference to the LevelGenerator script
        public Game game; // Reference to the Game script

        private void Start()
        {
            GenerateLevel();
        }

        public void GenerateLevel()
        {
            // Generate the map using the LevelGenerator script
            levelGenerator.GenerateMap();

            // Initialize the scene in the Game script
            game.InitScene();
        }

        public void RestartLevel()
        {
            // Restart the level in the Game script
            game.RestartLevel();
        }
    }
}