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
    private bool isReload;

    public void Reload()
    {
        if (isReload || curArrow != null) return;
        isReload = true;
        StartCoroutine(ReloadCorutine());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            Debug.Log("Click J");
            Shoot(50);
        }
    }


    private IEnumerator ReloadCorutine()
    {
        yield return new WaitForSeconds(reloadTime);
        curArrow = Instantiate(playerArrowPrefab, arrowSpawnPoint);
        curArrow.transform.localPosition = Vector3.zero;
        isReload = false;
    }

    public void Shoot(float power)
    {
        if (isReload || curArrow == null) return;

        var force = arrowSpawnPoint.TransformDirection(Vector3.forward * power);
        curArrow.Shoot(force);
        curArrow = null;
        Reload();
    }

    public bool IsReady()
    {
        return (!isReload && curArrow != null);
    }
}
