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
            QuestType.FirstQuest => new QuestNumberEnemy(100)
                                .Add(EnemyType.Zombunny, 1)
                                .Add(EnemyType.ZomBear, 1)
                                .Add(EnemyType.Hellephant, 1),
            QuestType.SecondQuest => new QuestNumberEnemy(200)
                                .Add(EnemyType.Zombunny, 10)
                                .Add(EnemyType.ZomBear, 5)
                                .Add(EnemyType.Hellephant, 3),
            QuestType.ThirdQuest => new QuestNumberEnemy(300)
                                .Add(EnemyType.Zombunny, 10)
                                .Add(EnemyType.ZomBear, 10)
                                .Add(EnemyType.Hellephant, 5),
            QuestType.FinalQuest => new QuestNumberEnemy(400)
                                .Add(EnemyType.Zombunny, 10)
                                .Add(EnemyType.ZomBear, 10)
                                .Add(EnemyType.Hellephant, 5)
                                .Add(EnemyType.FinalBoss, 1),
            _ => throw new Exception("Invalid Enemy Type"),
        };
    }       
    
    public static string QuestName(QuestType type)
    {
        return type switch
        {
            QuestType.FirstQuest => "First Quest",
            QuestType.SecondQuest => "Second Quest",
            QuestType.ThirdQuest => "Third Quest",
            QuestType.FinalQuest => "Final Quest",
            _ => throw new Exception("Unimplemented Quest Name"),
        };
    }
}
