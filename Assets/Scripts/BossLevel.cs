using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Match3
{
    public class BossLevel : Level
    {
        public int numMoves;
        public int targetScore; // The score a player needs to damage the boss
        public int bossHp; // The boss's hit points
        public PieceType[] obstacleTypes;

        private const int ScorePerPieceCleared = 1000;
        private int _movesUsed = 0;
        private int _numObstaclesLeft;
        private int _bossCurrentHp;

        private void Start()
        {
            type = LevelType.boss;

            // count the overall number of pieces on the board
       
            _bossCurrentHp = bossHp; // Set boss's current HP to max HP

            hud.SetLevelType(type);
            hud.SetScore(currentScore);
            hud.SetTarget(targetScore); // Update the score target
            hud.SetRemaining(numMoves);
            hud.SetBossHP(_bossCurrentHp);
        }

        public override void OnMove()
        {
            _movesUsed++;
            hud.SetRemaining(numMoves - _movesUsed);

            if (numMoves - _movesUsed == 0 || _bossCurrentHp <= 0)
            {
                if (currentScore >= targetScore && _bossCurrentHp <= 0)
                {
                    GameWin();
                }
                else
                {
                    GameLose();
                }
            }
        }

        public override void OnPieceCleared(GamePiece piece)
        {
            base.OnPieceCleared(piece);
            currentScore += ScorePerPieceCleared;

            while (currentScore >= targetScore && _bossCurrentHp > 0) // If player has enough score to damage the boss
            {
                _bossCurrentHp--; // decrease the boss's HP
                currentScore -= targetScore; // consume the score
                hud.SetBossHP(_bossCurrentHp);
            }
        }

    }
}