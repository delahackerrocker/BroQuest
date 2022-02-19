using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerController : MonoBehaviourPun
{
    [HideInInspector]
    public int ID;

    [Header("Info")]
    public float moveSpeed;
    public float gold;
    public float currentHP;
    public float maxHP;
    public bool dead;

    [Header("Attack")]
    public int damage;
    public float attackRange;
    public float attackDelay;
    public float lastAttackTime;

    [Header("Components")]
    public Rigidbody2D rig;
    public Player photonPlayer;
    public SpriteRenderer spriteRenderer;
    public GameObject centerOfBody;
    public Animator weaponAnim;
    public PlayerInfo playerInfo;

    // Local Player
    public static PlayerController me;

    private Camera playerCamera;

    [PunRPC]
    public void Initialize(Player player)
    {
        ID = player.ActorNumber;
        photonPlayer = player;

        GameManager.instance.players[ID-1] = this;

        playerInfo.Initialize(player.NickName, maxHP);

        GameObject[] findingCamera = GameObject.FindGameObjectsWithTag("Game_Camera");
        playerCamera = findingCamera[0].GetComponent<Camera>();

        // initialize the health bar
        if (player.IsLocal)
        {
            me = this;
        }
        else
        {
            rig.isKinematic = false;
        }
    }

    private void Update()
    {
        if (!photonView.IsMine)
            return;

        Move();

        if (Input.GetMouseButtonDown(0) && Time.time - lastAttackTime > attackDelay)
        {
            Attack();
        }

        float mouseX = (Screen.width/2) - Input.mousePosition.x;
        if (mouseX < 0)
        {
            centerOfBody.transform.localScale = new Vector3(1, 1, 1);
        } else {
            centerOfBody.transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void Move()
    {
        // Get the horizontal and vertical input
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        //float x = Input.GetAxis("Mouse X");
        //float y = Input.GetAxis("Mouse Y");

        rig.velocity = new Vector2(x,y) * moveSpeed;
    }
    private void Attack()
    {
        //lastAttackTime = Time.time;
        //Vector3 dir = (Input.mousePosition - playerCamera.ScreenToWorldPoint(transform.position).normalized);

        //RaycastHit2D hit = Physics2D.Raycast(transform.position + dir, dir, attackRange);

        ///if (hit.collider != null && hit.collider.gameObject.CompareTag("Enemy"))
        //{

        //}

        weaponAnim.SetTrigger("Attack");
    }

    private void Die()
    {
        this.dead = true;
        rig.isKinematic = true;

        // Hide player from camera view
        transform.position = new Vector3(0,99,0);

        Vector3 spawnPos = GameManager.instance.spawnPoints[Random.Range(0, GameManager.instance.spawnPoints.Length)].position;
    }

    [PunRPC]
    public void TakeDamage(float damage)
    {
        currentHP = damage;

        playerInfo.photonView.RPC("UpdateHealthBar", RpcTarget.All, currentHP);

        if (currentHP < 0)
        {
            Die();
        } else
        {
            StartCoroutine(DamageFlash());

            IEnumerator DamageFlash()
            {
                spriteRenderer.color = Color.red;
                yield return new WaitForSeconds(0.04f);
                spriteRenderer.color = Color.white;
            }
        }
    }

    [PunRPC]
    private void Heal(float amountToHeal)
    {
        currentHP = Mathf.Clamp(currentHP + amountToHeal, 0, maxHP);

        playerInfo.photonView.RPC("UpdateHealthBar", RpcTarget.All, currentHP);
    }

    [PunRPC]
    private void GetGold(float goldToGive)
    {
        gold += goldToGive;

        // Update the UI
    }

    IEnumerator Spawn(Vector3 spawnPos, float timeToSpawn)
    {
        yield return new WaitForSeconds(timeToSpawn);

        dead = false;
        transform.transform.position = spawnPos;
        currentHP = maxHP;
        rig.isKinematic = false;
        // Update health bar
    }
}