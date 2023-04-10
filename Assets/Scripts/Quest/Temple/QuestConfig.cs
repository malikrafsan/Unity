using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;

public class QuestConfig
{
    public static QuestNumberEnemy GetNumberEnemy(QuestType type)
    {
        return type switch
        {
            QuestType.FirstQuest => new QuestNumberEnemy()
                                .Add(EnemyType.Zombunny, 1)
                                .Add(EnemyType.ZomBear, 1)
                                .Add(EnemyType.Hellephant, 1),
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
