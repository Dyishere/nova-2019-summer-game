using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleMovement : MonoBehaviour
{
    public float recoveringTime = 2f; //受伤后恢复的时间
    public float hp = 100f;
    public float regionDamage; // 区域伤害
    public bool isMovingToTree = false;
    public bool isGrapplingEnemy = false;
    public int grappledEnemyID = 0;
    Vector3 Pos = new Vector3();


    private bool isHurt = false;

    private IslandPlayerController m_IslandPlayerController;
    private void Awake()
    {
        m_IslandPlayerController = GetComponent<IslandPlayerController>();
    }
    public void Move(float iMoveX, float iMoveY)
    {
        // 受伤期间无法操作
        if (isHurt)
        {
            return;
        }

        Pos.x = iMoveX;
        Pos.y = iMoveY;

        gameObject.transform.position = gameObject.transform.position + (Pos.normalized) * Time.deltaTime * 3;


        if (isGrapplingEnemy == true)                     //拉敌人
        {
            GameObject temp = null;

            switch (grappledEnemyID)
            {
                case 1:
                    temp = GameObject.Find("Player1");
                    break;
                case 2:
                    temp = GameObject.Find("Player2");
                    break;
                case 3:
                    temp = GameObject.Find("Player3");
                    break;
                case 4:
                    temp = GameObject.Find("Player4");
                    break;
            }
            if (temp != null)
            {
                float dis = (transform.position - temp.transform.position).sqrMagnitude;
                if (dis <= 6f)
                {
                    temp.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                    temp.transform.position = gameObject.transform.position + (temp.transform.position - gameObject.transform.position).normalized * 3;
                    isGrapplingEnemy = false;
                    grappledEnemyID = 0;
                    temp = null;
                }
                else
                {
                    temp.GetComponent<Rigidbody2D>().velocity = (gameObject.transform.position - temp.transform.position).normalized * 20f;
                }
            }
        }

        if (isMovingToTree == true)                      //把自己往树拉
        {
            GameObject temp = GameObject.FindGameObjectWithTag("Tree");
            gameObject.GetComponent<Rigidbody2D>().velocity = (temp.transform.position - gameObject.transform.position).normalized * 10f;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 如果触碰了陷阱
        // 请在这里添加陷阱条件
        if (false)
        {
            GetHurt(20);
        }
    }

    private float getRegionDamageRate = 0.2f; // 每过x秒，触发一次区域伤害 
    private float getRegionDamageTimer = 0f;
    private void OnTriggerStay2D(Collider2D other)
    {
        IslandRegion m_Region = other.GetComponent<IslandRegion>();
        if (m_Region == null) { return; }

        if (m_Region.playerID != GetMyPlayerID())
        {
            getRegionDamageTimer += Time.deltaTime;
            if (getRegionDamageTimer > getRegionDamageRate)
            {
                GetHurt(regionDamage);
                getRegionDamageTimer = 0f;
            }
        }
    }

    private int GetMyPlayerID() // 低效操作，应该引用相关组件来获取ID值
    {
        switch (gameObject.name)
        {
            case "Player1": return 1;
            case "Player2": return 2;
            case "Player3": return 3;
            case "Player4": return 4;
            default: return -1;
        }
    }
    private void GetHurt(float damage)
    {
        isHurt = true;
        // m_Animator.SetBool("hurt", true);
        hp -= damage;
        if (hp < 0)
        {
            hp = 0f;
            //死亡
        }
        Invoke("Recover", recoveringTime);
    }
    private void Recover()
    {
        isHurt = false;
        // m_Animator.SetBool("hurt", false);
    }
}
