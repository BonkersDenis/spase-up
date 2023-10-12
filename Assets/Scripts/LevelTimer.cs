using UnityEngine;

namespace Match3
{
    public class LevelTimer : Level
    {

        public int timeInSeconds;
        public int targetScore;
        public PieceType[] obstacleTypes;
        private float _timer;
        private int _numObstaclesLeft;
        private void Start ()
        {

            type = LevelType.Timer;
            for (int i = 0; i < obstacleTypes.Length; i++)
            {
                _numObstaclesLeft += gameGrid.GetPiecesOfType(obstacleTypes[i]).Count;
            }
            hud.SetLevelType(type);
            hud.SetScore(currentScore);
            hud.SetTarget(targetScore);
            hud.SetRemaining($"{timeInSeconds / 60}:{timeInSeconds % 60:00}");
        }

        private void Update()
        {
            _timer += Time.deltaTime;
            hud.SetRemaining(
                $"{(int) Mathf.Max((timeInSeconds - _timer) / 60, 0)}:{(int) Mathf.Max((timeInSeconds - _timer) % 60, 0):00}");

            if (timeInSeconds - _timer <= 0)
            {
                if (currentScore >= targetScore)
                {
                    GameWin();
                }
                else
                {
                    GameLose();
                }
            }
        }
	
    }
}
