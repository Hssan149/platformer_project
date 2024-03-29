using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    //player stats 
    public float moveSpeed = 4f;
    public float jumpForce = 9.75f;
    private bool canAttack = true;
    public bool isGrounded = false;

    //components references
    private Animator anim;
    public static SpriteRenderer sp;
    private Rigidbody2D rb;

    //spawn points
    
    public GameObject startPoint;
    private GameObject spawnPoint;

    //abilities
    private bool haveAbility = false;
    [SerializeField]
    GameObject shootingPoint;
    //fire ball
    [SerializeField]
    private GameObject fireBall;
    private bool haveFireBall=false;
    private bool canFireBall=false;
    //ice blizzard
    [SerializeField]
    private GameObject blizzard;
    private bool haveBlizzard = false;
    private bool canBlizzard = false;
    //electric shock
    [SerializeField]
    private GameObject shock;
    private bool haveShock = false;
    private bool canShock = false;
    //spark
    [SerializeField]
    private GameObject spark;
    private bool haveSpark = false;
    private bool canSpark= false;
    //ability gems
    [SerializeField]
    private Sprite[] sprites;

    //menus references
    [SerializeField]
    private GameObject pause_menu;
    [SerializeField]
    private GameObject audio_settings;

    //game stats
    public static bool paused = false;
    public static bool dead = false;
    public static bool won = false;
    private bool finalBoss = false;

    //UI
    [SerializeField]
    private TextMeshProUGUI coins;
    [SerializeField]
    private TextMeshProUGUI currentAbility;
    public GameObject[] hearts;
    public GameObject winText;
    [SerializeField]
    private GameObject Lore_dialogue;
    [SerializeField]
    private GameObject Lore_box;
    [SerializeField]
    private GameObject Lore_box_opened;
    [SerializeField]
    private GameObject Lore_sign;
    [SerializeField]
    private GameObject Lore_hint;
    [SerializeField]
    private GameObject[] hints;//to show or hide hint signs


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sp = GetComponent<SpriteRenderer>();
        gameObject.transform.GetChild(0).gameObject.GetComponent<BoxCollider2D>().enabled = false;
        spawnPoint = startPoint;
        GameManager.getInstance().currentLevel = SceneManager.GetActiveScene().name[GameManager.getInstance().currentLevel = SceneManager.GetActiveScene().name.Length - 1];
        if(PlayerPrefs.GetInt("hint")==1)
        {
            foreach(GameObject n in hints)
            {
                n.SetActive(true);
            }
        }
        else if (PlayerPrefs.GetInt("hint") == 0)
        {
            foreach (GameObject n in hints)
            {
                n.SetActive(false);
            }
        }
    }

    void Update()
    {
        if (!paused) 
        {
            //Exit lore box
            quit_Chest_Box();
            quit_hint();
            //player control section
            move();
            jump();
            attack();
            death();
            if(Input.GetKeyDown(KeyCode.V))
            discardAbility();
            if (haveAbility)
            { //abilites control
                if (haveFireBall)
                    shootFireBall();
                else if (haveBlizzard)
                    shootBlizzard();
                else if (haveShock)
                    shootShock();
                else if (haveSpark)
                    shootSpark();
            }
            if (finalBoss)
            {
                GameObject.FindGameObjectWithTag("finalColl").SetActive(false);
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape)&&!dead&&!won)
        {//pausing the game
            if (!paused)//pause
            {
                Time.timeScale = 0;
                if(!pause_menu.activeSelf)
                pause_menu.SetActive(true);
                paused = !paused;
            }
            else
            {//unpause
                Time.timeScale = 1;
                if(pause_menu.activeSelf)
                pause_menu.SetActive(false);
                paused = !paused;
                if (audio_settings.activeSelf)
                    audio_settings.SetActive(false);
            }
        }//game pause
    }


    //collision handling start
    private void OnCollisionEnter2D(Collision2D collision)//collision handling
    {
        if (collision.gameObject.tag == "Ground")//only jump when on ground
        {
            isGrounded = true;
            anim.SetBool("jumping", false);
        }
        else if (collision.gameObject.tag == "spikes")//lose lives when hitting hazards
        {
            if (GameManager.getInstance().lives > 0)
            {
                AudioManager.Instance.playSfx("hit");
                GameManager.getInstance().lives--;
                hearts[GameManager.getInstance().lives].SetActive(false);
                if(GameManager.getInstance().lives>1)
                transform.position = spawnPoint.transform.position;
            }
        }
    }

    //********************************************************************
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "spawnPoint")//change spawn point on collision
        {
            spawnPoint = collision.gameObject;
        }
        else if (collision.gameObject.tag == "coin")//pick up collectables
        {
            AudioManager.Instance.playSfx("coin");
            Destroy(collision.gameObject);
            GameManager.getInstance().coins_level++;
            coins.text = "X " + GameManager.getInstance().coins_level;
        }
        else if (collision.gameObject.tag == "Finish")
            win();
        else if (collision.gameObject.tag == "ChestBox")//chest box
        {
            AudioManager.Instance.playSfx("Chest open");
            Lore_dialogue.SetActive(true);
            Time.timeScale = 0;
            Vector3 temp = Lore_box.transform.position;
            //Destroy(Lore_box); --> it worked without destroying lore box (higher priority)
            Instantiate(Lore_box_opened, temp, Quaternion.identity);
        }
        else if (collision.gameObject.tag == "loreHint")
        {
            Lore_hint.SetActive(true);
            Time.timeScale = 0;
        }
        /////melee enemies ai start
        else if (collision.gameObject.tag == "enemyLeft")
        {
            if (collision.gameObject != null)
            {
                //seperate the colliders and refernce the enemy in their script
                collision.gameObject.GetComponent<Colliders>().meleeEnemy.GetComponent<SpriteRenderer>().flipX = true;
                collision.gameObject.GetComponent<Colliders>().meleeEnemy.GetComponent<Patrol>().enabled = false;
                collision.gameObject.GetComponent<Colliders>().meleeEnemy.GetComponent<Attack>().enabled = true;
            }
        }
        else if (collision.gameObject.tag == "enemyRight")
        {
            if (collision.gameObject != null)
            {
                collision.gameObject.GetComponent<Colliders>().meleeEnemy.GetComponent<SpriteRenderer>().flipX = false;
                collision.gameObject.GetComponent<Colliders>().meleeEnemy.GetComponent<Patrol>().enabled = false;
                collision.gameObject.GetComponent<Colliders>().meleeEnemy.GetComponent<Attack>().enabled = true;
            }
        }
        /////melee enemies ai end
        /////ranged enemies ai start
        else if (collision.gameObject.tag == "rangedLeft")
        {
            if (collision.gameObject != null)
            {
                collision.gameObject.GetComponent<Colliders>().meleeEnemy.GetComponent<SpriteRenderer>().flipX = true;
                collision.gameObject.GetComponent<Colliders>().meleeEnemy.GetComponent<RangedPatrol>().enabled = false;
                collision.gameObject.GetComponent<Colliders>().meleeEnemy.GetComponent<Shoot>().enabled = true;
            }
        }
        else if (collision.gameObject.tag == "rangedRight")
        {
            if (collision.gameObject != null)
            {
                collision.gameObject.GetComponent<Colliders>().meleeEnemy.GetComponent<SpriteRenderer>().flipX = false;
                collision.gameObject.GetComponent<Colliders>().meleeEnemy.GetComponent<RangedPatrol>().enabled = false;
                collision.gameObject.GetComponent<Colliders>().meleeEnemy.GetComponent<Shoot>().enabled = true;
            }
        }
        /////ranged enemies ai end
        else if (collision.gameObject.tag == "bossLeft") ;
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        //melee enemy
        if (collision.gameObject.tag == "enemyRight" || collision.gameObject.tag == "enemyLeft")
        {
            if (collision.gameObject != null) 
            {
            collision.gameObject.GetComponent<Colliders>().meleeEnemy.GetComponent<Patrol>().enabled = true;
            collision.gameObject.GetComponent<Colliders>().meleeEnemy.GetComponent<Attack>().enabled = false;
            collision.gameObject.GetComponent<Colliders>().meleeEnemy.GetComponent<Patrol>().anim.SetBool("moving", true); 
            }
        }
        else if (collision.gameObject.tag == "rangedRight" || collision.gameObject.tag == "rangedLeft")//ranged enemy
        {
            if (collision.gameObject != null)
            {
            collision.gameObject.GetComponent<Colliders>().meleeEnemy.GetComponent<Shoot>().CancelInvoke();     
            collision.gameObject.GetComponent<Colliders>().meleeEnemy.GetComponent<RangedPatrol>().enabled = true;
            collision.gameObject.GetComponent<Colliders>().meleeEnemy.GetComponent<Shoot>().enabled = false;
            collision.gameObject.GetComponent<Colliders>().meleeEnemy.GetComponent<RangedPatrol>().anim.SetBool("moving", true);
            }
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "abilityGem" && !haveAbility && Input.GetKey(KeyCode.C))
        {
            AudioManager.Instance.playSfx("gem");
            if (collision.gameObject.GetComponent<SpriteRenderer>().sprite == sprites[0])
            {
                GameObject effect = GameObject.Find("fire PartSystem");
                effect.transform.position = collision.gameObject.transform.position;
                effect.GetComponent<ParticleSystem>().Play();

                haveAbility = true;
                canFireBall = true;
                haveFireBall = true;
                currentAbility.text = "Ability: Fire Ball";
                Destroy(collision.gameObject);
            }
            else if (collision.gameObject.GetComponent<SpriteRenderer>().sprite == sprites[1])
            {
                GameObject effect = GameObject.Find("ice PartSystem");
                effect.transform.position = collision.gameObject.transform.position;
                effect.GetComponent<ParticleSystem>().Play();

                haveAbility = true;
                canBlizzard = true;
                haveBlizzard = true;
                Destroy(collision.gameObject);
                currentAbility.text = "Ability: Ice Blizzard";
            }
            else if (collision.gameObject.GetComponent<SpriteRenderer>().sprite == sprites[2])
            {
                GameObject effect = GameObject.Find("shock PartSystem");
                effect.transform.position = collision.gameObject.transform.position;
                effect.GetComponent<ParticleSystem>().Play();

                haveAbility = true;
                canShock = true;
                haveShock = true;
                Destroy(collision.gameObject);
                currentAbility.text = "Ability: Dark Bolt";
            }
            else if (collision.gameObject.GetComponent<SpriteRenderer>().sprite == sprites[3])
            {
                GameObject effect = GameObject.Find("spark PartSystem");
                effect.transform.position = collision.gameObject.transform.position;
                effect.GetComponent<ParticleSystem>().Play();

                haveAbility = true;
                canSpark = true;
                haveSpark = true;
                Destroy(collision.gameObject);
                currentAbility.text = "Ability: Spark";
            }
            
        }
    }

    //collision handling end

    //Chest box Handling
    void quit_Chest_Box()
    {
        if (Input.GetKeyDown(KeyCode.Q) && Lore_dialogue.activeSelf == true)
        {
            Lore_dialogue.SetActive(false);
            Time.timeScale = 1; 
        }
    }

    void quit_hint()
    {
        if (Input.GetKeyDown(KeyCode.Q) && Lore_hint.activeSelf == true)
        {
            Lore_hint.SetActive(false);
            Time.timeScale = 1;
        }
}

    //player movement and abilities start

    void move()//move horizontally
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        anim.SetFloat("speed", Mathf.Abs(horizontalInput));
        if (horizontalInput != 0)
            if (horizontalInput >= 0)
                sp.flipX = false;
            else
                sp.flipX = true;   
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);        
    }

    void jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            isGrounded = false;
            anim.SetBool("jumping", true);
        }
    }

    void attack()
    {
        if(Input.GetKeyDown(KeyCode.X)&&canAttack)
        {
            // AudioManager.Instance.playSfx("sword");
            canAttack = false;
            GameObject Attack = gameObject.transform.GetChild(0).gameObject;
            Attack.GetComponent<BoxCollider2D>().enabled = true;
            if (sp.flipX == true)
                Attack.transform.position = transform.position + new Vector3(-1.5f, 0f, 0f);
            else
                Attack.transform.position = transform.position;
            anim.SetBool("attacking", true);
            StartCoroutine("waitAttack",Attack);
           
            
        }
    }
    IEnumerator waitAttack(GameObject att)
    {
        yield return new WaitForSeconds(.5f);
        att.GetComponent<BoxCollider2D>().enabled = false;
        anim.SetBool("attacking", false);
        canAttack = true;
    }

    //Abilities section start

    void discardAbility()
    {
        if(haveAbility)
        {
            haveAbility = false;
            currentAbility.text = "Ability: none";
            if (haveFireBall)
            {
                haveFireBall = false;
                canFireBall = false;
            }
            else if (haveBlizzard)
            {
                haveBlizzard = false;
                canBlizzard = false;
            }
            else if (haveShock)
            {
                haveShock = false;
                canShock = false;
            }
            else if (haveSpark)
            {
                haveSpark = false;
                canSpark = false;
            }
        }
    }

    //fire ball
    void shootFireBall()
    {
        if (canFireBall && Input.GetKeyDown(KeyCode.Z))
        {
            AudioManager.Instance.playSfx("Fire Ball out");
            canFireBall = false;
            if (sp.flipX == true)
                shootingPoint.transform.position = transform.position + new Vector3(-1.5f, 0f, 0f);
            else
                shootingPoint.transform.position = transform.position+ new Vector3(1.5f, 0f, 0f);
            Instantiate(fireBall, transform.position, Quaternion.identity);
            StartCoroutine("fireBallCoolDown");
        }
    }

    IEnumerator fireBallCoolDown()
    {
        yield return new WaitForSeconds(1.5f);
        canFireBall = true;
    }

    //blizzard
    void shootBlizzard()
    {
        if (canBlizzard && Input.GetKeyDown(KeyCode.Z))
        {
            AudioManager.Instance.playSfx("Ice spell");
            canBlizzard = false;
            if (sp.flipX == true)
                shootingPoint.transform.position = transform.position + new Vector3(-1.5f, 0f, 0f);
            else
                shootingPoint.transform.position = transform.position + new Vector3(1.5f, 0f, 0f);
            Instantiate(blizzard, transform.position, Quaternion.identity);
            StartCoroutine("blizzardCoolDown");
        }
    }

    IEnumerator blizzardCoolDown ()
    {
        yield return new WaitForSeconds(1.5f);
        canBlizzard = true;
    }

    //electric shock
    void shootShock()
    {
        if (canShock && Input.GetKeyDown(KeyCode.Z))
        {
            AudioManager.Instance.playSfx("Electric shock");
            canShock = false;
            if (sp.flipX == true)
                shootingPoint.transform.position = transform.position + new Vector3(-1.5f, 0f, 0f);
            else
                shootingPoint.transform.position = transform.position + new Vector3(1.5f, 0f, 0f);
            Instantiate(shock, transform.position, Quaternion.identity);
            AudioManager.Instance.playSfx("Electric shock");
            StartCoroutine("shockCoolDown");
        }
    }

    IEnumerator shockCoolDown()
    {
        yield return new WaitForSeconds(1.5f);
        canShock = true;
    }

    //spark
    void shootSpark()
    {
        if (canSpark && Input.GetKeyDown(KeyCode.Z))
        {
            AudioManager.Instance.playSfx("spark");
            canSpark = false;
            if (sp.flipX == true)
                shootingPoint.transform.position = transform.position + new Vector3(-1.5f, 0f, 0f);
            else
                shootingPoint.transform.position = transform.position + new Vector3(1.5f, 0f, 0f);
            Instantiate(spark, transform.position, Quaternion.identity);
            StartCoroutine("sparkCoolDown");
        }
    }

    IEnumerator sparkCoolDown()
    {
        yield return new WaitForSeconds(2.5f);
        canSpark = true;
    }

    //Abilities section end

    //player movement and abilities end


    //player state start
    void death()
    {
        if (GameManager.getInstance().lives == 0)
        {
            Time.timeScale = 0;
            dead = true;
            pause_menu.transform.GetChild(0).gameObject.SetActive(false);
            pause_menu.SetActive(true);
            if(SceneManager.GetActiveScene().name=="level1")
            AudioManager.Instance.stopMusic("bgm_level1");
            else if (SceneManager.GetActiveScene().name == "level2")
                AudioManager.Instance.stopMusic("bgm_level2");
            else if (SceneManager.GetActiveScene().name == "level4")
                AudioManager.Instance.stopMusic("bgm_level3");
        }
    }

    public void takeHit()
    {
        AudioManager.Instance.playSfx("hit");
        GameManager.getInstance().lives--;
        hearts[GameManager.getInstance().lives].SetActive(false);
    }
    
    void win()
    {
        AudioManager.Instance.playSfx("win");
        Time.timeScale = 0;
        won = true;
        pause_menu.transform.GetChild(0).gameObject.SetActive(false);
        if(SceneManager.GetActiveScene().name=="level1")
            pause_menu.transform.GetChild(4).gameObject.SetActive(true);
        else
            pause_menu.transform.GetChild(4).gameObject.SetActive(false);
        pause_menu.SetActive(true);
        paused = true;
        winText.SetActive(true);

        
        if (SceneManager.GetActiveScene().name == "level1")//unlocks next level on win
            PlayerPrefs.SetInt("level2",1);
        else if (SceneManager.GetActiveScene().name == "level2")
            PlayerPrefs.SetInt("level4", 1);//update after changing scene name

    }

    //player state end

    
}
