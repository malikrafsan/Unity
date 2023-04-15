using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField]
    private NPC npc;

    bool isTalking = false;
    bool onDistance = false;

    float distance;
    float curResponseTracker = 0f;

    readonly float thresholdDist = 2.5f;

    public GameObject player;
    public GameObject dialogueUI;

    public Text npcName;
    public Text npcDialogueBox;
    public Text playerResponse;

    private bool[] hasTalked = new bool[4];
    public bool[] HasTalked
    {
        get => hasTalked;
        set
        {
            hasTalked = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        dialogueUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (!onDistance)
        {
            if (isTalking)
            {
                EndConvo();
            }

            return;
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            if (isTalking)
            {
                EndConvo();
            }
            else
            {
                StartConvo();
            }
        }

        if (isTalking)
        {
            if (Input.GetKeyDown(KeyCode.Tab)) {
                curResponseTracker++;
                if (curResponseTracker >= npc.playerDialogues.Length) 
                {
                    curResponseTracker = 0;
                }
            }

            for (int i=0;i<npc.playerDialogues.Length;i++)
            {
                CheckConvo(i);
            }
        }
    }

    private void CheckConvo(int idx)
    {
        if (curResponseTracker == idx && npc.playerDialogues.Length >= idx)
        {
            playerResponse.text = npc.playerDialogues[idx];
            if (Input.GetKeyDown(KeyCode.Return))
            {
                npcDialogueBox.text = npc.dialogues[idx+1];
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        onDistance = true;
        ToastManager.Instance.ShowToast("Press T to start or stop talking", 1);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        onDistance = false;
    }


    private void StartConvo()
    {
        hasTalked[GlobalStateManager.Instance.IdxQuest] = true;
        isTalking = true;
        curResponseTracker = 0f;
        dialogueUI.SetActive(true);
        npcName.text = npc.name;
        npcDialogueBox.text = npc.dialogues[0];
    }

    private void EndConvo()
    {
        isTalking = false;
        dialogueUI.SetActive(false);
    }
}
