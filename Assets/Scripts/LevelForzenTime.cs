using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Match3
{
    public class LevelForzenTime : Level
    {
        public int numMoves;
        public int targetScore; // ��������� ���������� ��� ������� ���� �� �����
        public PieceType[] obstacleTypes;
        public PieceType[] Frozen;

        private const int ScorePerPieceCleared = 1000;
        // ����� �� ���������� ������������� ������ � ��������
        public float freezeInterval;
        // ���������� ������, ������� ����� ���������� �� ���� ���
        public int numberOfPiecesToFreeze;
        private int _movesUsed = 0;
        private int _numObstaclesLeft;
        private int _numFrozenBlocksLeft;


        private void Start()
        {
            type = LevelType.coldTimer;

            // ��������� ���������� ��� ������ ���������� ����� �� �����
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
            hud.SetTarget(targetScore); // ��������� ���� �� �����
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
            while (true) // ����������� ����, ����� �������� ����� �������� �����
            {
                yield return new WaitForSeconds(freezeInterval); // �������� ��������� ��������� �������

                gameGrid.FreezeRandomPieces(numberOfPiecesToFreeze); // ������������ �����
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