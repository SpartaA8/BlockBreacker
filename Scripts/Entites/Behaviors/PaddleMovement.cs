using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.AnimatedValues;
using UnityEngine;

public class PaddleMovement : MonoBehaviour
{
    private PlayerController controller;
    private Rigidbody2D rigidbody;
    private BoxCollider2D collider;
    private FixedJoint2D joint;

    private float sizeRate = 0.25f;
    private float speed = 4f;    
    private Vector3 direction;
    private bool isHold;

    private void Awake()
    {
        controller = GetComponentInParent<PlayerController>();
        rigidbody = GetComponent<Rigidbody2D>();   
        collider = GetComponent<BoxCollider2D>();
        joint = GetComponent<FixedJoint2D>();
    }

    private void Start()
    {
        controller.OnMoveEvent += Move;
        controller.OnFireEvent += Fire;
        joint.connectedBody = rigidbody;
    }

    private void FixedUpdate()
    {
        rigidbody.velocity = direction * speed;        
    }

    public void ResetState(Vector3 position)
    {
        transform.position = position;
        speed = 4f;
        Vector3 scale = transform.localScale;
        transform.localScale = new Vector3(1.25f, 0.25f, 0);
    }

    public void Move(float input)
    {
        if (input == 0)
        {
            rigidbody.constraints |= RigidbodyConstraints2D.FreezePositionX;            
        }
            
        else
        {
            rigidbody.constraints &= ~RigidbodyConstraints2D.FreezePositionX;            
        }
            
        direction = new Vector2(input, 0);        
    }

    public void Fire()
    {
        if (isHold) ShootBall();
        else CheckHold();
    }

    private void CheckHold()
    {
        RaycastHit2D hit = Physics2D.BoxCast(collider.bounds.center, collider.size, 0f, Vector2.up, 0.1f,LayerMask.GetMask("Ball"));

        if(hit.collider != null)
        {
            HoldBall(hit.collider.gameObject, hit.point.x);
        }
    }

    private void ShootBall()
    {        
        isHold = false;
        float posX = joint.connectedAnchor.x > 0 ? 1f : -1f;        
        BallController ball = joint.connectedBody.gameObject.GetComponent<BallController>();
        joint.connectedBody = rigidbody;
        joint.enabled = false;
        ball.Shoot(posX);        
    }

    public void HoldBall(GameObject obj, float posX)
    {
        Rigidbody2D ball = obj.GetComponent<Rigidbody2D>();
        isHold = true;
        float posY = -3.97f;
        obj.transform.position = new Vector2(posX, posY);
        joint.enabled = true;
        joint.connectedBody = ball;
        obj.GetComponent<BallController>().Catched();
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 오브젝트 충돌 후 효과 적용
        if (collision.gameObject.layer == 9)
        {
            ItemController collisionController = collision.gameObject.GetComponent<ItemController>();
            if (collisionController != null)
            {
                // 아이템 타입 분별 및 효과 적용
                ApplyItem(collisionController.SoItem.itemType);
                // 아이템 먹은 후 파괴
                Destroy(collision.gameObject);
            }            
        }
        else if(collision.gameObject.layer == 12)
        {
            MainSceneManager.Instance.ReduceLife();
        }
    }

    public void ApplyItem(EItemType itemType)
    {
        if (itemType == EItemType.SIZE)
        {
            ChangeSize();
        }
        else if (itemType == EItemType.SPEED)
        {
            ChangeSpeed();
        }
        else if (itemType == EItemType.LIFE)
        {
            MainSceneManager.Instance.AddLife();
        }
        else if (itemType == EItemType.COPY)
        {
            MainSceneManager.Instance.Copyballs();
        }
        AudioManager.Instance.PlayClip("GetItem");
    }

    public void ChangeSize()
    {        
        int randomsize = Random.Range(0, 4);
    
        if(randomsize == 0)
        {
            if (transform.lossyScale.x == 1) return;
            Vector3 scale = transform.localScale;
            transform.localScale = scale + Vector3.left * sizeRate;
        }
        else
        {
            if (transform.lossyScale.x == 2) return;
            Vector3 scale = transform.localScale;
            transform.localScale = scale + Vector3.right * sizeRate;         
        }

    }

    public void ChangeSpeed()
    {
        int randomsize = Random.Range(0, 1);

        if (randomsize == 4)
        {
            if (speed == 3) return;
            speed -= 1f;
        }
        else
        {
            if (speed == 7) return;
            speed += 1f;
        }
    }
}

