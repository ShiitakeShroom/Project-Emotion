using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    public CharacterStatus enemyStatus;

/*    public void Start()
    {
        DestroyWhenDead();
    }

    public void DestroyWhenDead()
    {
        if (LevelLoader.instance.playerWins)
        {
            if (LevelLoader.instance.charaStatus == enemyStatus && enemyStatus.health <= 0)
            {
                Destroy(this.gameObject);
                //DestroyObjectTracker.MarkObjectAsDestroyed(this.gameObject);
            }
        }
        else
        {
            enemyStatus.health = enemyStatus.maxHealth;
        }
    }*/
}
