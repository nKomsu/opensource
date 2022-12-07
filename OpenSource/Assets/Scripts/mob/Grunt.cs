using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Grunt : MonoBehaviour
{
    public GameObject coin;
    public GameObject getHitEffect;
    public BoxCollider attackRange;
    public float maxHp;
    public float nowHp;

    Transform target;
    Vector3 des;
    Rigidbody rigid;
    NavMeshAgent nav;
    Animator ani;
    SphereCollider sphereCollider;
    Player pStat;


    bool isrun;
    bool isAttack;
    bool isDie;

    // Start is called before the first frame update
    void Awake()
    {
        pStat = GameObject.Find("PLAYER").GetComponent<Player>();
        target = GameObject.Find("PLAYER").GetComponent<Transform>();
        rigid = GetComponent<Rigidbody>();
        sphereCollider = GetComponent<SphereCollider>();
        ani = GetComponentInChildren<Animator>();
        nav = GetComponent<NavMeshAgent>();
        des = nav.destination;
        NavStart();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDie)
        {
            StopAllCoroutines();
            rigid.velocity = Vector3.zero;
            return;
        }
        Navigation();
    }

    private void FixedUpdate()
    {
        FreezeVelocity();
        Targetting();
    }

    void NavStart()
    {
        isrun = true;
        ani.SetBool("isRun", true);
    }

    void Navigation()
    {
        if (nav.enabled)
        {
            nav.SetDestination(target.position);
            nav.isStopped = !isrun;
        }

    }

    void FreezeVelocity()
    {
        if (isrun)
        {
            rigid.velocity = Vector3.zero;
            rigid.angularVelocity = Vector3.zero;
        }
    }

    void Targetting()
    {
        float targetRadius = 0.3f;
        float targetRange = 7f;

        RaycastHit[] rayHits = Physics.SphereCastAll(transform.position, targetRadius, transform.forward, targetRange, LayerMask.GetMask("Player"));   //�ڽ��� ��ġ, ������, ���ư��� ����, ����(�Ÿ�), ���̾ƿ�

        if (rayHits.Length > 0 && !isAttack)
        {
            StartCoroutine(Attack());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet" && !isDie)
        {
            nowHp -= (int)pStat.CurWeapon.Damage;    //damage�� �ٲٱ�
            StartCoroutine(GetHitEffect());
            StartCoroutine(OnDamage());
        }
    }

    IEnumerator GetHitEffect()
    {
        Vector3 dir = transform.position + new Vector3(0.0f, 1.0f, 0.0f);

        GameObject blood = Instantiate(getHitEffect, dir, transform.rotation);

        yield return new WaitForSeconds(1.0f);

        Destroy(blood);

        yield return null;
    }

    IEnumerator Attack()
    {
        isrun = false;
        ani.SetBool("isRun", false);

        isAttack = true;

        ani.SetBool("isAttack", true);

        yield return new WaitForSeconds(0.1f);
        
        rigid.AddForce(transform.forward * 10, ForceMode.Impulse); //����
        attackRange.enabled = true;

        yield return new WaitForSeconds(0.5f);

        rigid.velocity = Vector3.zero;  //������ �ӵ� ����
        attackRange.enabled = false;

        yield return new WaitForSeconds(0.8f);

        isrun = true;
        ani.SetBool("isRun", true);

        isAttack = false;
        ani.SetBool("isAttack", false);

    }

    IEnumerator OnDamage()
    {
        yield return new WaitForSeconds(0.1f);

        if (nowHp <= 0)
        {
            ani.SetTrigger("doDie");
            nav.speed = 0;
            isDie = true;

            pStat.kill++;
            Instantiate(coin, transform.position, transform.rotation);

            Destroy(gameObject, 3);
        }
    }
}
