namespace Match3
{
    public class LevelMoves : Level
    {

        public int numMoves;
        public int targetScore;
        public PieceType[] obstacleTypes;
        private int _movesUsed = 0;
        private int _numObstaclesLeft;
        private void Start()
        {
            type = LevelType.Moves;
            for (int i = 0; i < obstacleTypes.Length; i++)
            {
                _numObstaclesLeft += gameGrid.GetPiecesOfType(obstacleTypes[i]).Count;
            }
            hud.SetLevelType(type);
            hud.SetScore(currentScore);
            hud.SetTarget(targetScore);
            hud.SetRemaining(numMoves);
        }

        public override void OnMove()
        {
            _movesUsed++;

            hud.SetRemaining(numMoves - _movesUsed);

            if (numMoves - _movesUsed != 0) return;
        
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
