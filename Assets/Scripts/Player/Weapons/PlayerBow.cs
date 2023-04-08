using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBow : MonoBehaviour
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

    void Awake()
    {
        ResetCharge();
        chargeSlider.gameObject.SetActive(true);
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

        if (Input.GetMouseButton(0))
        {
            chargeSlider.value = arrowEnergy * 100;
            chargeTime += Time.deltaTime;
        }

        if (Input.GetMouseButtonUp(0))
        {
            Shoot(arrowEnergy);
            ResetCharge();
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            Shoot(0);
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
        curArrow = Instantiate(playerArrowPrefab, transform.position, arrowSpawnPoint.transform.rotation);
        var force = curArrow.transform.forward;
        curArrow.GetComponent<Rigidbody>().AddRelativeForce(-400 * energy * force);
    }

    public bool IsReady()
    {
        return (!isReload && curArrow != null);
    }
}
