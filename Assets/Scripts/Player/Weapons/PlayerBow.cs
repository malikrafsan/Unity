using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBow : MonoBehaviour, WeaponHandler
{
    [SerializeField]
    private float reloadTime;

    [SerializeField]
    private PlayerArrow playerArrowPrefab;

    [SerializeField]
    private Transform arrowSpawnPoint;

    private PlayerArrow curArrow;
    private bool isReload = false;

    public Slider chargeSlider;
    [SerializeField]
    private float maxCharge = 3f;
    private float chargeTime = 0f;
    private float arrowEnergy = 0f;

    public Color colorMax = Color.green;
    public Color colorMin = Color.red;

    private int level = 1;
    public int Level
    {
        get
        {
            return level;
        }
        set
        {
            level = value;
        }
    }

    private Animator animator;

    void Awake()
    {
        ResetCharge();
        chargeSlider.gameObject.SetActive(true);
        curArrow = Instantiate(playerArrowPrefab, arrowSpawnPoint);
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        ResetCharge();
        chargeSlider.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        ResetCharge();
        chargeSlider.gameObject.SetActive(false);
    }

    public void Reload()
    {
        if (isReload || curArrow != null) return;
        isReload = true;
        StartCoroutine(ReloadCorutine());
    }

    void Update()
    {
        arrowEnergy = chargeTime > maxCharge ? maxCharge : chargeTime;

        if (Input.GetMouseButton(1) && !GameControl.control.cantShoot)
        {
            chargeSlider.value = arrowEnergy * 100;
            chargeTime += Time.deltaTime;
        }

        if (Input.GetMouseButtonUp(1) && !GameControl.control.cantShoot)
        {
            Shoot(arrowEnergy);
            ResetCharge();
        }
    }

    private void ResetCharge()
    {
        chargeTime = 0;
        chargeSlider.value = 0;
    }

    private IEnumerator ReloadCorutine()
    {
        yield return new WaitForSeconds(reloadTime);
        ReloadArrow();
    }

    private void ReloadArrow()
    {
        isReload = false;
    }

    public void Shoot(float energy)
    {
        var force = arrowSpawnPoint.TransformDirection(-200 * energy * level * Vector3.forward);
        curArrow.Shoot(force);
        StartCoroutine(ShootAnim());
        curArrow = Instantiate(playerArrowPrefab, arrowSpawnPoint);
    }

    IEnumerator ShootAnim()
    {
        this.animator.Play("Bow");
        yield return new WaitForSeconds(1f / 6);
        this.animator.Play("Idle");
    }

    public bool IsReady()
    {
        return (!isReload && curArrow != null);
    }

    public void IncrementLevel()
    {
        level++;
    }
}
