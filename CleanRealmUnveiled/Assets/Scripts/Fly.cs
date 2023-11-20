using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : Enemy
{
    [SerializeField] private float chaseDistance;
    [SerializeField] private float stunDuration;
    float timer;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        ChangeState(EnemyStates.Fly_Idle);
    }

    protected override void UpdateEnemyState()
    {
        float _dist = Vector2.Distance(transform.position, Player.Instance.transform.position);
        switch(currenEnemyState)
        {
            case EnemyStates.Fly_Idle:
                if (_dist < chaseDistance)
                {
                    ChangeState(EnemyStates.Fly_Chase);
                }
                break;

            case EnemyStates.Fly_Chase:
                rb.MovePosition(Vector2.MoveTowards(transform.position, Player.Instance.transform.position, Time.deltaTime * speed));

                FlipFly();
                break;

            case EnemyStates.Fly_Stunned:
                timer += Time.deltaTime;

                if(timer > stunDuration)
                {
                    ChangeState(EnemyStates.Fly_Idle);
                    timer = 0;
                }

                break;


            case EnemyStates.Fly_Death:


                break;
        }
    }

    void FlipFly()
    {
        sr.flipX = Player.Instance.transform.position.x < transform.position.x;
    }
}
