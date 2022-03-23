using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public class UtilsClass
    {
        /// <summary>
        /// Возвращает рандомное направление
        /// </summary>
        public static Vector3 GetRandomDirection()
        {
            return new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        }

        /// <summary>
        /// Находит игрока в радиусе от objectPosition
        /// </summary>
        /// <param name="objectPosition"></param>
        /// <param name="range"></param>
        /// <returns>true, если игрок найден в радиусе</returns>
        public static bool DetectPlayerInRange(Vector2 objectPosition, float range)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(objectPosition, range);
            foreach (Collider2D collider in colliders)
            {
                if (collider.TryGetComponent<Player>(out Player player))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Определяет видимость игрока мобу
        /// </summary>
        /// <param name="enemyPosition"></param>
        /// <param name="playerPosition"></param>
        /// <returns>false - игрок находится за чем-нибудь что мешает мобу видеть игрока</returns>
        public static bool CanSeePlayer(Vector2 enemyPosition, Vector2 playerPosition)
        {
            RaycastHit2D hit = Physics2D.Linecast(enemyPosition, playerPosition);
            Debug.DrawLine(enemyPosition, playerPosition);
            if (hit.collider != null)
            {
                if (hit.collider.TryGetComponent<Player>(out Player player))
                    return true;
            }
            return false;
        }

        
    }
}

