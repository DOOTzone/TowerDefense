using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnButton : MonoBehaviour
{
    public void ForceSpawnCurrentWave()
    {
        GameManager.Instance.ChangeGameState(GameManager.GameState.SpawnEnemies);
    }
}
