using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    // count times activated
    private int count = 0;
    private bool isBoss = false;
    public PlayerHealth playerHealth;
    public float spawnTime = 2f;
    public float spawnStart = 0f;

    [SerializeField]
    MonoBehaviour factory;
    IFactory Factory
    {
        get
        {
            return factory as IFactory;
        }
    }

    void Start()
    {
        isBoss = GlobalStateManager.Instance.IdxQuest == 3;
    }


    void Spawn()
    {
        if (playerHealth.currentHealth <= 0f)
        {
            return;
        }
        if (!isBoss)
        {
            int spawnEnemy = Random.Range(0, 3);

            Factory.FactoryMethod(spawnEnemy);
            return;
        }
        Factory.FactoryMethod(3);
        isBoss = false;
    }

    private void OnDisable()
    {
        Debug.Log("Turn off " + nameof(this.Spawn));
        CancelInvoke(nameof(this.Spawn));
    }

    private void OnEnable()
    {
        count++;
        // fill count with last quest
        if (count == 4) isBoss = true;
        Debug.Log("Turn on " + nameof(this.Spawn));
        InvokeRepeating(nameof(this.Spawn), spawnStart, spawnTime);
    }

    public static string EnemyName(EnemyType type)
    {
        switch (type)
        {
            case EnemyType.Zombunny: return "ZomBunny";
            case EnemyType.ZomBear: return "ZomBear";
            case EnemyType.Hellephant: return "Hellephant";
            case EnemyType.FinalBoss: return "Final Boss";
            default: return "";
        }
    }
}
