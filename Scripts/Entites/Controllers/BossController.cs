using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    private Animator animator;
    private BossHandler bossHandler;
    private Coroutine CurrentPattern;
    public GameObject miniSpear;
    private float spearSpeed = 8f;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        bossHandler = GetComponent<BossHandler>();
    }

    public void Start()
    {
        CurrentPattern = StartCoroutine(BossAttackPhase1());
        bossHandler.BossPhase2 += BossChangePattern;
    }

    private IEnumerator BossAttackPhase1()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);

            int randomValue = Random.Range(0, 4);
            if (randomValue == 0)
            {
                animator.SetTrigger("Attack1");
            }
            else if (randomValue == 1)
            {
                animator.SetTrigger("Attack2");
            }
            else if (randomValue == 2)
            {
                animator.SetTrigger("Attack3");
            }
        }
    }

    private IEnumerator BossAttackPhase2()
    {
        while (true)
        {

            yield return new WaitForSeconds(7f);

            int randomValue = Random.Range(1, 101);
            if (randomValue <= 20)
            {
                HyukAttack();
            }
            else if (randomValue > 20 && randomValue <= 40)
            {
                animator.SetTrigger("hyukAttack1");
            }
            else if (randomValue > 40 && randomValue <= 60)
            {
                animator.SetTrigger("hyukAttack2");
            }
            else if (randomValue > 60 && randomValue <= 70)
            {
                animator.SetTrigger("Attack1");
            }
            else if (randomValue > 70 && randomValue <= 80)
            {
                animator.SetTrigger("Attack2");
            }
            else if (randomValue > 80 && randomValue <= 90)
            {
                animator.SetTrigger("Attack3");
            }
        }
    }

    private void HyukAttack()
    {
        StartCoroutine(SpawnAndShootSpears());
    }

    private IEnumerator SpawnAndShootSpears()
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            float x = UnityEngine.Random.Range(-3.18f, 3.18f);
            float y = UnityEngine.Random.Range(-0.74f, -1f);
            Vector3 spawnPosition = new Vector3(x, y, 0);

            GameObject spear = Instantiate(miniSpear, spawnPosition, Quaternion.identity);

            if (player != null)
            {
                Vector3 direction = (player.transform.position - spear.transform.position).normalized;

                float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                spear.transform.rotation = Quaternion.Euler(0f, 0f, rotZ - 90f);

                yield return new WaitForSeconds(0.5f);

                Rigidbody2D rb = spear.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.velocity = direction * spearSpeed;
                }
            }
        }
    }

    private void BossChangePattern()
    {
        StopCoroutine(CurrentPattern);
        CurrentPattern = StartCoroutine(BossAttackPhase2());
    }
}
