using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void Awake()
    {
        //ReloadArrow();
    }

    public void Reload()
    {
        if (isReload || curArrow != null) return;
        isReload = true;
        StartCoroutine(ReloadCorutine());
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }


    private IEnumerator ReloadCorutine()
    {
        yield return new WaitForSeconds(reloadTime);
        ReloadArrow();
    }

    private void ReloadArrow()
    {
        /*curArrow.transform.SetParent(arrowSpawnPoint.transform, false);*/
        isReload = false;
    }

    public void Shoot()
    {
        curArrow = Instantiate(playerArrowPrefab, transform.position, arrowSpawnPoint.transform.rotation);
        var force = curArrow.transform.forward;
        curArrow.GetComponent<Rigidbody>().AddRelativeForce(force * -40);
        // Reload();
    }

    public bool IsReady()
    {
        return (!isReload && curArrow != null);
    }
}
