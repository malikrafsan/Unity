using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    // Static Reference
    private static string prefabPath = "Assets/Prefabs/GameControl.prefab";

    public static GameControl control;

    public static GameControl Control
    {
        set
        {
            control = value;
        }
        get
        {
            if (control == null)
            {
                // initialise reference game object
                Object gameControlRef = Resources.Load(prefabPath);
                GameObject controlObject = Instantiate(gameControlRef) as GameObject;

                if (control != null)
                {
                    control = controlObject?.GetComponent<GameControl>();

                    DontDestroyOnLoad(controlObject);
                }
            }
            return control;
        }
    }

    private void Awake()
    {
        if (control != null)
        {
            Destroy(gameObject);
            return;
        }
        control = this;
        DontDestroyOnLoad(gameObject);
    }

    // Data to persist
    public float petIdx = -1;
    public int currency = 0;

    // BOSS ABILITY
    public bool cantShoot = false;

    // CHEATS
    public bool cheatOneHitKill = false;
    public bool fullHPPet = false;
    public bool killPet = false;
    public bool motherLoadOn = false;

    // Currencies
    public void addCurrency(int amt)
    {
        currency += amt;
    }

    public bool isEnough(int amt)
    {
        return (currency >= amt);
    }

    public void minusCurrency(int amt)
    {
        currency -= amt;
    }

    // From: https://johnleonardfrench.com/how-to-fade-audio-in-unity-i-tested-every-method-this-ones-the-best
    public IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume, GameObject theGameObject)
    {
        float currentTime = 0;
        float start = audioSource.volume;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        Destroy(theGameObject, 2f);
        yield break;

    }
}
