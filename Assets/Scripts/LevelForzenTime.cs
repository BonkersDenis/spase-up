using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Match3
{
    public class LevelForzenTime : Level
    {
        public int numMoves;
        public int targetScore; // Добавляем переменную для задания цели по очкам
        public PieceType[] obstacleTypes;
        public PieceType[] Frozen;

        private const int ScorePerPieceCleared = 1000;
        // время до следующего замораживания блоков в секундах
        public float freezeInterval;
        // количество блоков, которые нужно заморозить за один раз
        public int numberOfPiecesToFreeze;
        private int _movesUsed = 0;
        private int _numObstaclesLeft;
        private int _numFrozenBlocksLeft;


        private void Start()
        {
            type = LevelType.coldTimer;

            // Добавляем переменную для общего количества фишек на доске
            StartCoroutine(FreezeBlocksOverTime());
            for (int i = 0; i < obstacleTypes.Length; i++)
            {
                _numObstaclesLeft += gameGrid.GetPiecesOfType(obstacleTypes[i]).Count;
            }

            for (int i = 0; i < Frozen.Length; i++)
            {
                _numFrozenBlocksLeft += gameGrid.GetPiecesOfType(Frozen[i]).Count;
            }

            hud.SetLevelType(type);
            hud.SetScore(currentScore);
            hud.SetTarget(targetScore); // Обновляем цель по очкам
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
        private IEnumerator FreezeBlocksOverTime()
        {
            while (true) // бесконечный цикл, после которого будет запущена пауза
            {
                yield return new WaitForSeconds(freezeInterval); // ожидание заданного интервала времени

                gameGrid.FreezeRandomPieces(numberOfPiecesToFreeze); // замораживаем блоки
                _numFrozenBlocksLeft += numberOfPiecesToFreeze;

                if (_numFrozenBlocksLeft >= 30)
                {
                    
                    GameLose();
                    yield break; 
                }
            }
        }

    }
}