using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Collections.AllocatorManager;

public class BallController : MonoBehaviour
{    
    private Rigidbody2D rigidbody;
    private TrailRenderer trailRenderer;    

    // 공 Copy에서 사용할 각도
    private float minRotationAngle = 30f;
    private float maxRotationAngle = 50f;

    private float defaultSpeed = 5f;
    private bool isCatched = false;    

    private void OnEnable()
    {
        MainSceneManager.Instance.OnCopyBallEvent += Copy;
        MainSceneManager.Instance.OnFinishStageEvent += Destroyed;
    }
    private void OnDisable()
    {
        MainSceneManager.Instance.OnCopyBallEvent -= Copy;
        MainSceneManager.Instance.OnFinishStageEvent -= Destroyed;
    }

    private void Awake()
    {  
        rigidbody = GetComponent<Rigidbody2D>();
        trailRenderer = GetComponent<TrailRenderer>();
    }

    public void Copy()
    {
        float rotationAngle = Random.Range(minRotationAngle, maxRotationAngle);
        RotateAngle(rotationAngle);
        RotateAngle(-rotationAngle);
    }

    public void RotateAngle(float rotationAngle)
    {
        GameObject ball = MainSceneManager.Instance.CreateBalls();
        if (ball == null)
        {
            return;
        }
        // 현재 공의 속도
        Vector2 currentVelocity = rigidbody.velocity;

        Vector2 direction = Quaternion.Euler(0, 0, rotationAngle) * currentVelocity.normalized;

        // 복사할 공의 위치 설정
        ball.transform.position = transform.position;

        // 복사할 공의 Rigidbody2D Component 가져오기
        Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();

        // 복사할 공의 방향과 속도 설정
        rb.velocity = direction * currentVelocity.magnitude;
    }

    public void Catched()
    {
        isCatched = true;        
    }

    public void Shoot(float posX)
    {
        float rotationAngle = Random.Range(minRotationAngle, maxRotationAngle);
        Vector2 direction = Quaternion.Euler(0, 0, rotationAngle * posX) * Vector2.up;
        //Vector2 direction = Vector2.right; // Test용 코드
        rigidbody.velocity = direction.normalized * defaultSpeed;
        isCatched = false;
    } 

    private void OnCollisionEnter2D(Collision2D collision) //TODO ::  switch문으로 고치기
    {
        string collisionLayerName = LayerMask.LayerToName(collision.gameObject.layer);
        
        switch (collisionLayerName)
        {
            case "Player":
                ProcessPaddleCollision(collision);
                AudioManager.Instance.PlayClip("PaddleHit");
                break;
            case "Block":
                ProcessBlockCollision(collision);
                ObjectCollision(collision);
                AudioManager.Instance.PlayClip("BlockHit");
                break;
            case "Boss":
                ProcessBlockCollision(collision);
                ObjectCollision(collision);
                break;
            case "Bottom":
                Destroyed();
                AudioManager.Instance.PlayClip("BallDestroy");
                break;
            case "Wall":
                ObjectCollision(collision);
                AudioManager.Instance.PlayClip("PaddleHit");
                break;
            default:
                break;
        }
    }

    //패들에 닿았을때
    private void ProcessPaddleCollision(Collision2D collision)
    {
        Vector2 collisionpoint = collision.contacts[0].point; // 패들에 어느위치에 충돌이 일어났는지 확인 하기위한 vector
        Vector2 paddlecenter = collision.transform.position; // 패들 중심 가져오기위한 vector

        // 충돌 지점이 패들 중심보다 왼쪽에 있는지 확인
        bool isleftcollision = collisionpoint.x < paddlecenter.x;

        // x축 방향의 속도를 왼쪽 또는 오른쪽으로 반전
        float direction = isleftcollision ? -1f : 1f;
        rigidbody.velocity = new Vector2(direction * Mathf.Abs(rigidbody.velocity.x), rigidbody.velocity.y);        
    }

    //블록에 닿았을때
    private void ProcessBlockCollision(Collision2D collision)
    {
        BlockHandler blockHandler = collision.gameObject.GetComponent<BlockHandler>();
        if (blockHandler != null) blockHandler.TakeDamage(1);
        BossHandler bossHandler = collision.gameObject.GetComponent<BossHandler>();
        if (bossHandler != null) bossHandler.TakeDamage(1);        
    }

    //패들을 제외한 오브젝트에 닿았을때
    private void ObjectCollision(Collision2D collision)
    {
        //Vector2 newDirection = rigidbody.velocity.normalized;
        //float angleChangeRadians = Mathf.Deg2Rad * Random.Range(-5f, 5f);
        //newDirection = Quaternion.Euler(0, 0, Mathf.Rad2Deg * angleChangeRadians) * newDirection;
        //rigidbody.velocity = newDirection * defaultSpeed;

        Vector2 newDirection = rigidbody.velocity.normalized;
        Vector2 localXAxis = new Vector2(1f, 0f); // 오브젝트의 로컬 x축
        Vector2 localYAxis = new Vector2(0f, 1f); // 오브젝트의 로컬 y축
        float angleWithXAxis = Vector2.Angle(newDirection, localXAxis);
        float angleWithYAxis = Vector2.Angle(newDirection, localYAxis);
        if (Mathf.Approximately(angleWithXAxis, 90f) || Mathf.Approximately(angleWithYAxis, 90f) ||
            Mathf.Approximately(angleWithXAxis, 180f) || Mathf.Approximately(angleWithYAxis, 180f))
        {
            float angleChangeRadians = Mathf.Deg2Rad * Random.Range(-30f, 30f);
            newDirection = Quaternion.Euler(0, 0, Mathf.Rad2Deg * angleChangeRadians) * newDirection;            
        }
        rigidbody.velocity = newDirection * defaultSpeed;
    }

    // 바닥에 닿았을때
    public void Destroyed()
    {        
        if (!gameObject.activeSelf) return;
        MainSceneManager.Instance.ObjectPool.ReturnObject(this.gameObject);
        MainSceneManager.Instance.DestroyBalls();
    }
}
