using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public static Player instance;
    public int coin()
    {
        return PlayerPrefs.GetInt("coin", 100);
    }

    public int level()
    {
        return PlayerPrefs.GetInt("level", 1);
    }

    public bool canMove = false;
    [SerializeField] int moveSpeed = 3;
    [SerializeField] Transform movePoint;
    [SerializeField] LayerMask whatStopsMovement;
    [SerializeField] float minx, maxx, miny, maxy;


    [Header("LEVEL5")]
    public GameObject tp1;
    public GameObject tp2;

    [Space]
    public TMPro.TMP_Text skortext;
    public TMPro.TMP_Text cleveltext;

    private void Awake()
    {
        instance = this;
    }
    void GameEnded()
    {
        Debug.LogError("game ended!");
        ReloadScene();
       
    }

    void move()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

    }
    bool levelEnded = false;

    int a;
    void Update()
    {
   
        cleveltext.text = "Current Level : " + (level()).ToString();
        skortext.text = "SCORE : " + coin().ToString(); 
        if (GameObject.FindWithTag("odul"))
        {
         
            if (transform.position == GameObject.FindWithTag("odul").transform.position&& !levelEnded)
            {
                Time.timeScale = 0;
                getReward(100);
                levelEnded = true;
                canMove = false;
                levelendedPanel.SetActive(true);

                leveltext.text = "YOU HAVE PASSED " + (level()).ToString() +" LEVEL!";
             
            }
        }
        if (transform.position == tp1.transform.position)
        {
            transform.position = tp2.transform.position;
            print("TELEPORTED!");
            tpdone = true;
            canMove = false;

        }
        if (!tpdone)
        {
            if (PlayerPrefs.GetInt("level", 1) == 5)
            {
                if (transform.position.y == 4f)
                {
                    if (transform.position.x < 16)
                    {
                        ReloadScene(); //
                    }
                }
            }

        }

        if (transform.position.x<minx ||transform.position.x > maxx || transform.position.y>maxy || transform.position.y < miny)
        {
            GameEnded();
            this.gameObject.SetActive(false);
            return;
        }
        if (canMove) move();
        if (Vector3.Distance(transform.position, movePoint.position) <= .05f)
        {
            if (Physics2D.OverlapCircle(movePoint.position + new Vector3(1, 0, 0), .2f, whatStopsMovement))
            {
                movePoint.position += new Vector3(-1, 0, 0);
            }
            if (Physics2D.OverlapCircle(movePoint.position + new Vector3(-1, 0, 0), .2f, whatStopsMovement))
            {
                movePoint.position += new Vector3(1, 0, 0);
            }
            if (Physics2D.OverlapCircle(movePoint.position + new Vector3(0, 1, 0), .2f, whatStopsMovement))
            {
                movePoint.position += new Vector3(0, -1, 0);
            }
            if (Physics2D.OverlapCircle(movePoint.position + new Vector3(0, -1, 0), .2f, whatStopsMovement))
            {
                movePoint.position += new Vector3(0, 1, 0);
            }


            //no walking
        }
        else
        {
          
            //walking
        }


    }
    protected bool tpdone=false;
    public GameObject levelendedPanel;

   
    public virtual void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
        tpdone = false;
        canMove = true;

    }

    public GameObject deadPanel;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("odul"))
        {
            deadPanel.SetActive(true);
            ReloadScene();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

    }
    void getReward(int rewardcoin)
    {
        int mycoin = coin();
        mycoin += rewardcoin;
        print("Coin : " + mycoin);
        PlayerPrefs.SetInt("coin", mycoin);
    }
    public List<GameObject> maps;
    public TMPro.TMP_Text leveltext;

    public void NextLevel()
    {
        int level = this.level();
        level++;
        if (level > maps.Count)
        {
            level = 1;
            Debug.LogError("LEVEL COUNT RESTARTED" + "\n" + "ALL MAPS HAS DONE.FIRST MAP LOADED");
        }
        PlayerPrefs.SetInt("level", level);
        ReloadScene();
    }
    private void Start()
    {
        LoadMap();
    }
    void LoadMap()
    {
        for (int i = 0; i < maps.Count; i++)
        {
            maps[i].SetActive(false);
        }
        maps[level() - 1].SetActive(true);
    }

  
}