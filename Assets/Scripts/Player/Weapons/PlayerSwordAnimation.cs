using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwordAnimation : MonoBehaviour
{
    [SerializeField]
    private GameObject sword;

    private Animator animator;

    private void Awake()
    {
        this.animator = sword.GetComponent<Animator>();
    }

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
        this.animator.Play("SwordSwing");
        yield return new WaitForSeconds(1.0f);
        this.animator.Play("Idle");
    }
}
