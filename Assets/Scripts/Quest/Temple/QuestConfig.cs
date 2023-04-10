using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;

public class QuestConfig
{
    public class QuestNumberEnemy
    {
        private readonly Dictionary<EnemyType, int> numEnemies;

        public QuestNumberEnemy() { }

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
    }

    public static QuestNumberEnemy GetNumberEnemy(QuestType type)
    {
        return type switch
        {
            QuestType.FirstQuest => new QuestNumberEnemy()
                                .Add(EnemyType.Zombunny, 5)
                                .Add(EnemyType.ZomBear, 3)
                                .Add(EnemyType.Hellephant, 2),
            QuestType.SecondQuest => new QuestNumberEnemy()
                                .Add(EnemyType.Zombunny, 10)
                                .Add(EnemyType.ZomBear, 5)
                                .Add(EnemyType.Hellephant, 3),
            QuestType.ThirdQuest => new QuestNumberEnemy()
                                .Add(EnemyType.Zombunny, 10)
                                .Add(EnemyType.ZomBear, 10)
                                .Add(EnemyType.Hellephant, 5),
            QuestType.FinalQuest => new QuestNumberEnemy()
                                .Add(EnemyType.Zombunny, 10)
                                .Add(EnemyType.ZomBear, 10)
                                .Add(EnemyType.Hellephant, 5)
                                .Add(EnemyType.FinalBoss, 1),
            _ => throw new Exception("Invalid Enemy Type"),
        };
    }       
    
}
