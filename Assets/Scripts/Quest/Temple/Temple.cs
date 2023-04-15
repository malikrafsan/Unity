using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Temple : MonoBehaviour
{
    [SerializeField]
    private HUD Hud;

    [SerializeField]
    private EnemyManager enemyManager;

    private TimerManager _timer;
    public TimerManager timer
    {
        get
        {
            if (_timer == null)
            {
                _timer = FindObjectOfType<TimerManager>();
            }

            return _timer;
        }
    }

    private readonly QuestType[] stepQuests = { QuestType.FirstQuest, QuestType.SecondQuest, QuestType.ThirdQuest, QuestType.FinalQuest };
    private int idxCurrentQuest = 0;
    private bool playerOnRange = false;
    private bool onQuest = false;
    private QuestNumberEnemy questNumberEnemy = null;

    public int IdxCurrentQuest
    {
        get => idxCurrentQuest;
        set
        {
            idxCurrentQuest = value;
        }
    }

    public bool OnQuest { get => onQuest; }

    private SaveDialogHandler _saveDialog;
    public SaveDialogHandler saveDialog
    {
        get
        {
            if (_saveDialog == null)
            {
                _saveDialog = GameObject.Find("HUDCanvas").GetComponentInChildren<SaveDialogHandler>();
            }

            return _saveDialog;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerOnRange && Input.GetKeyDown(KeyCode.G))
        {
            ToastManager.Instance.ShowToast("ENTERING QUEST",1);
            EnteringQuest();
        }
    }

    private void EnteringQuest()
    {
        if (onQuest) return;

        onQuest = true;
        questNumberEnemy = QuestConfig.GetNumberEnemy(stepQuests[idxCurrentQuest]).Clone();
        
        enemyManager.gameObject.SetActive(true);
        timer.StartTimer();
    }

    private void ExitingQuest()
    {
        var reward = questNumberEnemy.Reward;
        GameControl.control.addCurrency(reward);
        timer.StopTimer();
        onQuest = false;
        questNumberEnemy = null;
        enemyManager.gameObject.SetActive(false);
        var enemies = FindObjectsOfType<EnemyHealth>();
        foreach (var enemy in enemies)
        {
            Debug.Log("Killing"+ enemy);
            enemy.Death();
            //Destroy(enemy.gameObject);
        }

        idxCurrentQuest++;
        ToastManager.Instance.ShowToastQueue("Quest " + idxCurrentQuest + " is Completed! You got additional coins: " + reward, 1);

        // retrieve the time
        // add it to the global time
        // remove the timer
        var questTime = timer.TakeTime();
        ToastManager.Instance.ShowToastQueue("Your total time now: " 
            + System.TimeSpan.FromSeconds(GlobalManager.Instance.TotalTime).ToString("mm':'ss")
            + " + " + System.TimeSpan.FromSeconds(questTime).ToString("mm':'ss"), 1);
        GlobalManager.Instance.TotalTime += questTime;
        ToastManager.Instance.ShowToastQueue(System.TimeSpan.FromSeconds(GlobalManager.Instance.TotalTime).ToString("mm':'ss"), 1);

        this.saveDialog.Show();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (onQuest)
            {
                ToastManager.Instance.ShowToast("- Good Luck with your Quest -",1);
            }
            else
            {
                ToastManager.Instance.ShowToast("- Press G to Enter Quest " +
                    (idxCurrentQuest+1) + " - ",1);
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
            if (idxCurrentQuest == 1)
            {
                ToastManager.Instance.ShowToast("YOU WIN", 1);
                StartCoroutine(winHandler());
            }
        }
        else
        {
            ToastManager.Instance.ShowToast("Quest Enemies Left:\n" + questNumberEnemy.Stats(), 1);
        }
    }

    private IEnumerator winHandler()
    {
        yield return new WaitForSeconds(5);
        ScoreBoardScoreManager.Instance.AddScore(new Score(GlobalManager.Instance.PlayerName, (float)GlobalManager.Instance.TotalTime));
        SceneManager.LoadScene("CutsceneEnding");
    }
}
