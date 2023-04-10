using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temple : MonoBehaviour
{
    [SerializeField]
    private HUD Hud;

    private readonly QuestType[] stepQuests = { QuestType.FirstQuest, QuestType.SecondQuest, QuestType.ThirdQuest, QuestType.FinalQuest };
    private int idxCurrentQuest = 0;
    private bool playerOnRange = false;
    private bool onQuest = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerOnRange && Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log("ENTERING QUEST");
            
        }
    }

    private void EnteringQuest()
    {
        onQuest = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Hud.OpenMessagePanel("- Press G to Enter Quest " +
                stepQuests[idxCurrentQuest] + " - ");
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
}
