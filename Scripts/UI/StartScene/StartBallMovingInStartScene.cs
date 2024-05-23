using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;

public class StartBallMovingInStartScene : MonoBehaviour
{
    public Rigidbody2D rigidbody;


    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        StartCoroutine(BallMoving());
    }

    IEnumerator BallMoving()
    {
        rigidbody.gravityScale = 0f;
        rigidbody.velocity = (Vector3.down + Vector3.right) * 7f;
        yield break;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            SoundManager.instance.OnFxSound();
        }
    }
}
