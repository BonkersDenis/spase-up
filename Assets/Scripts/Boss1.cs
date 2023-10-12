using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Match3
{
    public class Boss1 : MonoBehaviour
    {
        public int initialSpawnCount = 5;  // Сколько элементов заморозки босс размещает в начале
        public float spawnInterval = 3.0f;  // Частота спавна замороженных элементов в секундах
        public int spawnRadius = 1; // Радиус спауна замороженных элементов вокруг босса

        private GameGrid _gameGrid;
        private bool _isActive = true;

        private void Start()
        {
            // Предположим, что GameGrid - это основной компонент вашего главного объекта
            _gameGrid = GameObject.FindObjectOfType<GameGrid>();

            int centerX = _gameGrid.xDim / 2;
            int centerY = _gameGrid.yDim / 2;

            SpawnFrozenPiecesAround(centerX, centerY, initialSpawnCount);
            StartCoroutine(SpawnFrozenPiecesPeriodically(centerX, centerY));
        }

        private void SpawnFrozenPiecesAround(int x, int y, int count)
        {
            for (int i = 0; i < count; i++)
            {
                int deltaX = Random.Range(-spawnRadius, spawnRadius + 1);
                int deltaY = Random.Range(-spawnRadius, spawnRadius + 1);

                int targetX = Mathf.Clamp(x + deltaX, 0, _gameGrid.xDim - 1);
                int targetY = Mathf.Clamp(y + deltaY, 0, _gameGrid.yDim - 1);

             
            }
        }

        private IEnumerator SpawnFrozenPiecesPeriodically(int x, int y)
        {
            while (_isActive)
            {
                yield return new WaitForSeconds(spawnInterval);
                SpawnFrozenPiecesAround(x, y, 1);
            }
        }

        // Вызывают этот метод, когда босс неактивный или побеждён
        public void DeactivateBoss()
        {
            _isActive = false;
        }
    }
}
