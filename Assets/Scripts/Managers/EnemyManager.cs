using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public float spawnTime = 3f;

    [SerializeField]
    MonoBehaviour factory;
    IFactory Factory
    {
        get
        {
            return factory as IFactory;
        }
    }

    void Start ()
    {

    }
    

    void Spawn ()
    {
        if (playerHealth.currentHealth <= 0f)
        {
            return;
        }

        int spawnEnemy = Random.Range(0, 3);

        Factory.FactoryMethod(spawnEnemy);
    }

    private void OnDisable()
    {
        Debug.Log("Turn off " + nameof(this.Spawn));
        CancelInvoke(nameof(this.Spawn));
    }

    private void OnEnable()
    {
        Debug.Log("Turn on " + nameof(this.Spawn));
        InvokeRepeating(nameof(this.Spawn), spawnTime, spawnTime);
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
