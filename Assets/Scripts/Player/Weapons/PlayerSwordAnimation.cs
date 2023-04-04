using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwordAnimation : MonoBehaviour
{
    [SerializeField]
    private GameObject sword;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(SwordSwinging());
        }
    }

    IEnumerator SwordSwinging()
    {
        sword.GetComponent<Animator>().Play("SwordSwing");
        yield return new WaitForSeconds(1.0f);
        sword.GetComponent<Animator>().Play("Idle");
    }
}
