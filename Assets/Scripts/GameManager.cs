using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager instance=null;
    public List<GameObject> slots;
    public int currentSlotIndex = 0;
    public Image up, down, right, left;
    public GameObject targetPoint;
    public GameObject player;
    Vector3 startPos;

    public int up_speed, right_speed;
    void Awake()
    {
        instance = this;

        startPos = targetPoint.transform.position;
        startPos = player.transform.position;
        player.transform.position = targetPoint.transform.position;
    }
    int currentSlotPos;
    float counter;

    public Button clearbtn;

    public void Clear()
    {
        int coin = Player.instance.coin();
        if (coin < 2000) return;
        coin -= 2000;
        PlayerPrefs.SetInt("coin", coin);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.End))
        {
            int coin = Player.instance.coin();
         
            coin += 2000;
            PlayerPrefs.SetInt("coin", coin);

        }
        clearbtn.interactable = Player.instance.coin() >= 2000 ? true : false;
        print("isPlaying : " + isPlaying + "\n" + "Time : " + Time.timeScale);
        if (isPlaying)
        {
            if (player.transform.position == targetPoint.transform.position)
            {
                if(currentSlotPos < slots.Count){
                    float targetx = slots[currentSlotPos].GetComponent<slotmanager>().x;
                    float targety = slots[currentSlotPos].GetComponent<slotmanager>().y;
                    if (targetx == 0 && targety > 0)
                    {
                        player.transform.rotation = Quaternion.Euler(0, 0, -90);
                    }else if(targetx == 0 && targety < 0)
                    {
                        player.transform.rotation = Quaternion.Euler(0, 0, 90);

                    }else if (targety == 0 && targetx > 0)
                    {
                        player.transform.rotation = Quaternion.Euler(0, 180, 0);

                    }else
                    {
                        player.transform.rotation = Quaternion.Euler(0, 0, 0);

                    }
                    targetPoint.transform.position += new Vector3(targetx, targety, 0);
                    currentSlotPos++;

                }
                else
                {
                    counter += Time.deltaTime;

                    if (counter > 1)
                    {
                        counter = 0;
                        Player.instance.ReloadScene();
                    }
                }

            }

        }
        else
        {
            targetPoint.transform.position = startPos;
            player.transform.position = startPos;

        }
    }
    public void BacktoLobby()
    {
        SceneManager.LoadScene(0);
    }
    private void LateUpdate()
    {
        SetSlotImages();
    }
    void SetSlotImages()
    {
        foreach (var item in slots)
        {
            if (item.GetComponent<slotmanager>().selectedDirection == 1)
            {
                item.GetComponent<Image>().sprite = up.sprite;
            }
            else if (item.GetComponent<slotmanager>().selectedDirection == 2)
            {
                item.GetComponent<Image>().sprite = down.sprite;
            }
            else if (item.GetComponent<slotmanager>().selectedDirection == 3)
            {
                item.GetComponent<Image>().sprite = right.sprite;
            }
            else if (item.GetComponent<slotmanager>().selectedDirection == 4)
            {
                item.GetComponent<Image>().sprite = left.sprite;
            }
        }
    }

    bool isPlaying=false;
 
    
    public void StartButton()
    {
        isPlaying = true;
        if (slots[0].GetComponent<Image>().sprite == null)
        {
            isPlaying = false;
            targetPoint.transform.position = startPos;
            player.transform.position = startPos;
            Player.instance.ReloadScene();
        }
    }
    public void CancelButton()
    {
       
        isPlaying = false;
        targetPoint.transform.position = startPos;
        player.transform.position = startPos;
    }
}
