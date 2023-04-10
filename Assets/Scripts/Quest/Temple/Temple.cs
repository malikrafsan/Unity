using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Temple : MonoBehaviour
{
    [SerializeField]
    private HUD Hud;

    [SerializeField]
    private EnemyManager enemyManager;

    private readonly QuestType[] stepQuests = { QuestType.FirstQuest, QuestType.SecondQuest, QuestType.ThirdQuest, QuestType.FinalQuest };
    private int idxCurrentQuest = 0;
    private bool playerOnRange = false;
    private bool onQuest = false;
    private QuestNumberEnemy questNumberEnemy = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerOnRange && Input.GetKeyDown(KeyCode.G))
        {
            ToastManager.Instance.ShowToast("ENTERING QUEST",3);
            EnteringQuest();
        }
    }

    private void EnteringQuest()
    {
        if (onQuest) return;

        onQuest = true;
        questNumberEnemy = QuestConfig.GetNumberEnemy(stepQuests[idxCurrentQuest]).Clone();
        enemyManager.gameObject.SetActive(true);
    }

    private void ExitingQuest()
    {
        onQuest = false;
        questNumberEnemy = null;
        enemyManager.gameObject.SetActive(false);
        var enemies = FindObjectsOfType<EnemyHealth>();
        foreach (var enemy in enemies)
        {
            Destroy(enemy.gameObject);
        }
        idxCurrentQuest++;
        ToastManager.Instance.ShowToast("Quest " + idxCurrentQuest + " is Completed!", 3);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (onQuest)
            {
                ToastManager.Instance.ShowToast("- Good Luck with your Quest -",3);
            }
            else
            {
                ToastManager.Instance.ShowToast("- Press G to Enter Quest " +
                    (idxCurrentQuest+1) + " - ",3);
            }
            playerOnRange = true;
        }   
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Hud.CloseMessagePanel();
            playerOnRange = false;
        }
    }

    private int GetIdxQuestType(QuestType type)
    {
        switch (type)
        {
            case QuestType.FirstQuest: return 0;
            case QuestType.SecondQuest: return 1;
            case QuestType.ThirdQuest: return 2;
            case QuestType.FinalQuest: return 3;
            default: return -1;
        }
    }

    public void OnDeathEnemy(EnemyType enemyType)
    {
        if (!onQuest) return;

        Debug.Log("OnDeathEnemy");
        questNumberEnemy.Decrement(enemyType);
        Debug.Log("enemyType: " + enemyType + " > " + questNumberEnemy.Get(enemyType));
        Debug.Log("isEmpty: " + questNumberEnemy.IsEmpty());
        if (questNumberEnemy.IsEmpty())
        {
            ExitingQuest();
        }
    }
}
