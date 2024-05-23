using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Collections.AllocatorManager;

public class BallController : MonoBehaviour
{    
    private Rigidbody2D rigidbody;
    private TrailRenderer trailRenderer;    

    // �� Copy���� ����� ����
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
        // ���� ���� �ӵ�
        Vector2 currentVelocity = rigidbody.velocity;

        Vector2 direction = Quaternion.Euler(0, 0, rotationAngle) * currentVelocity.normalized;

        // ������ ���� ��ġ ����
        ball.transform.position = transform.position;

        // ������ ���� Rigidbody2D Component ��������
        Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();

        // ������ ���� ����� �ӵ� ����
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
        //Vector2 direction = Vector2.right; // Test�� �ڵ�
        rigidbody.velocity = direction.normalized * defaultSpeed;
        isCatched = false;
    } 

    private void OnCollisionEnter2D(Collision2D collision) //TODO ::  switch������ ��ġ��
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

    //�е鿡 �������
    private void ProcessPaddleCollision(Collision2D collision)
    {
        Vector2 collisionpoint = collision.contacts[0].point; // �е鿡 �����ġ�� �浹�� �Ͼ���� Ȯ�� �ϱ����� vector
        Vector2 paddlecenter = collision.transform.position; // �е� �߽� ������������ vector

        // �浹 ������ �е� �߽ɺ��� ���ʿ� �ִ��� Ȯ��
        bool isleftcollision = collisionpoint.x < paddlecenter.x;

        // x�� ������ �ӵ��� ���� �Ǵ� ���������� ����
        float direction = isleftcollision ? -1f : 1f;
        rigidbody.velocity = new Vector2(direction * Mathf.Abs(rigidbody.velocity.x), rigidbody.velocity.y);        
    }

    //��Ͽ� �������
    private void ProcessBlockCollision(Collision2D collision)
    {
        BlockHandler blockHandler = collision.gameObject.GetComponent<BlockHandler>();
        if (blockHandler != null) blockHandler.TakeDamage(1);
        BossHandler bossHandler = collision.gameObject.GetComponent<BossHandler>();
        if (bossHandler != null) bossHandler.TakeDamage(1);        
    }

    //�е��� ������ ������Ʈ�� �������
    private void ObjectCollision(Collision2D collision)
    {
        //Vector2 newDirection = rigidbody.velocity.normalized;
        //float angleChangeRadians = Mathf.Deg2Rad * Random.Range(-5f, 5f);
        //newDirection = Quaternion.Euler(0, 0, Mathf.Rad2Deg * angleChangeRadians) * newDirection;
        //rigidbody.velocity = newDirection * defaultSpeed;

        Vector2 newDirection = rigidbody.velocity.normalized;
        Vector2 localXAxis = new Vector2(1f, 0f); // ������Ʈ�� ���� x��
        Vector2 localYAxis = new Vector2(0f, 1f); // ������Ʈ�� ���� y��
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

    // �ٴڿ� �������
    public void Destroyed()
    {        
        if (!gameObject.activeSelf) return;
        MainSceneManager.Instance.ObjectPool.ReturnObject(this.gameObject);
        MainSceneManager.Instance.DestroyBalls();
    }
}
