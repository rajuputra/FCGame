using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected float health;
    [SerializeField] protected float recoilLength;
    [SerializeField] protected float recoilFactor;
    [SerializeField] protected bool isRecoiling = false;

    [SerializeField] protected float speed;
    [SerializeField] protected float damage;

    protected float recoilTimer;
    protected Rigidbody2D rb;
    protected SpriteRenderer sr;

    protected enum EnemyStates
    {
        //Crawler Enemy
        Crawler_Idle,
        Crawler_Flip,

        //Fly Enemy
        Fly_Idle,
        Fly_Chase,
        Fly_Stunned,
        Fly_Death,
    }

    protected EnemyStates currenEnemyState;
    // Start is called before the first frame update
    
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    protected virtual void Update()
    {
        

        if (health <= 0)
        {
            Destroy(gameObject);
        }
        if (isRecoiling)
        {
            if (recoilTimer < recoilLength)
            {
                recoilTimer += Time.deltaTime;
            }
            else
            {
                isRecoiling = false;
                recoilTimer = 0;
            }
        }
        else
        {
            UpdateEnemyState();
        }
    }

    public virtual void EnemyHit(float _damageDone, Vector2 _hitDirection, float _hitForce)
    {
        health -= _damageDone;
        if (!isRecoiling)
        {
            rb.velocity = -_hitForce * recoilFactor * _hitDirection;
        }
    }

    protected void OnCollisionStay2D(Collision2D _other)
    {
        if (_other.gameObject.CompareTag("Player") && !Player.Instance.aState.invincible)
        {
            Attack();
            Player.Instance.HitStopTime(0, 5, 0.5f);
        }
    }

    protected virtual void UpdateEnemyState()
    {

    }
    protected void ChangeState(EnemyStates _newState)
    {
        currenEnemyState = _newState;
    }
    protected virtual void Attack()
    {
        Player.Instance.TakeDamage(damage);
    }
}
