using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class QuestNumberEnemy
{
    private readonly Dictionary<EnemyType, int> numEnemies;

    public QuestNumberEnemy() 
    {
        numEnemies = new Dictionary<EnemyType, int>();
    }

    public QuestNumberEnemy Add(EnemyType type, int num)
    {
        numEnemies.Add(type, num);
        return this;
    }

    public int Get(EnemyType type)
    {
        if (numEnemies.TryGetValue(type, out int num))
        {
            return num;
        }
        return 0;
    }

    public void Decrement(EnemyType type)
    {
        numEnemies[type] -= 1;
    }

    public bool IsEmpty()
    {
        foreach (var enemy in numEnemies.Values)
        {
            if (enemy > 0)
            {
                Debug.Log("enemy " + enemy + " <> 0");
                return false;
            }
        }

        return true;
    }

    public QuestNumberEnemy Clone()
    {
        QuestNumberEnemy clone = new QuestNumberEnemy();
        foreach (var enemyType in numEnemies.Keys)
        {
            clone.numEnemies[enemyType] = numEnemies[enemyType];
        }

        return clone;
    }
}
