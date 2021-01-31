using HitIt.Other;
using HitIt.Storage;

namespace HitIt.Ecs
{
    public class ScoreHandler : IInizializable
    {
        private GameMenuUI gameUI;
        private MainMenuUI menuUI;
        
        private ScoreSettings settings;

        private int score;

        public void Inizialize()
        {
            score = 0;           
            settings = StorageFacility.Instance.GetStorageByType<ScoreSettings>();
            gameUI = StorageFacility.Instance.GetInterface(InterfaceObject.GameUI).GetComponent<GameMenuUI>();
            menuUI = StorageFacility.Instance.GetInterface(InterfaceObject.MainMenuUI).GetComponent<MainMenuUI>();
        }

        public void AddScore()
        {
            score++;
        }
        
        public void ShowScore()
        {
            gameUI.Score = score.ToString();
            menuUI.MaxScore = settings.MaxScore.ToString();
        }

        public void HandleNewScore()
        {
            if(settings.MaxScore < score) settings.MaxScore = score;
        }

        public void ResetScore()
        {
            score = 0;
        }
    }
}