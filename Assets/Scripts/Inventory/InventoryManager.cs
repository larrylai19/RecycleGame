using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class InventoryManager : MonoBehaviour
{
    static InventoryManager instance;

    public GameObject BagPanel;
    public Inventory myBag;
    public GameObject slotGrid; // �b�]�w�n����l�ͦ�
    public Slot slotPrefab;
    public Text itemInfo;

    public GameObject recyclePanel;
    public TextMeshProUGUI itemName;
    private Item currentItem;
  
    //結束畫面
    public GameObject FinishPanel;
    public TextMeshProUGUI State;
    public TextMeshProUGUI TimerTxt;

    //音效
    public AudioClip correctSound;
    public AudioClip wrongSound;
    public AudioClip VictorySound;
    public AudioClip LoseSound;
    AudioSource audioSource;

    public GameObject SettingsPanel;

    public Transform CameraPos; 

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        myBag.itemList.Clear(); //�I�]�M��

        RefreshInventory();
    }

    void Update()
    {
        CheckFinishOrNot();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SettingsPanel.activeSelf) 
            {
                Time.timeScale = 1;
                SettingsPanel.SetActive(false);
            }
            else
            {
                Time.timeScale = 0;
                SettingsPanel.SetActive(true);
            } 
        }
    }

    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        instance = this;
    }

    private void OnEnable()
    {
        RefreshItem();
        instance.itemInfo.text = "";
    }

    public void ClickUse() //backbag using
    {
        if (currentItem)
        {
            BagPanel.SetActive(false);
            recyclePanel.SetActive(true);
            itemName.text = currentItem.itemInfo;
        }
    }

    public void ClickRecycle() //recycle using
    {
        string RecylceObjectName = EventSystem.current.currentSelectedGameObject.name;
        Debug.Log(RecylceObjectName);

        if (currentItem.RecycleID == RecylceObjectName)
        {
            AudioSource.PlayClipAtPoint(correctSound, CameraPos.position,0.1F);
            DestroyItem(currentItem);
            TrashBar.TrashCount -= 1;
        }
        else //在地圖隨機生成
        {
            AudioSource.PlayClipAtPoint(wrongSound, CameraPos.position, 0.1F);
            HealthBar.health -= 1;
            DestroyItem(currentItem);
            //Debug.Log("1");
            Instantiate_obj.Handler(currentItem);
        }
        recyclePanel.SetActive(false);
        BagPanel.SetActive(true);
        instance.itemInfo.text = "";
        currentItem = null;
    }

    public void DestroyItem(Item item)
    {
        myBag.itemList.Remove(item);
        RefreshInventory();
    }

    public void CheckFinishOrNot() //判斷是否結束遊戲
    {
        if (TrashBar.TrashCount == 0)
        {
            audioSource.PlayOneShot(VictorySound, 0.01F);
            Timer.stop = true;
            Time.timeScale = 0;
            FinishPanel.SetActive(true);
            State.text = "Win";
            TimerTxt.text = Timer.GetTime();
        }
        else if(HealthBar.health == 0)
        {
            audioSource.PlayOneShot(LoseSound, 0.01F);
            Timer.stop = true;
            Time.timeScale = 0;
            FinishPanel.SetActive(true);
            State.text = "Lose";
            TimerTxt.text = Timer.GetTime();
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    public void RefreshInventory()
    {
        for (int i = 0; i < instance.slotGrid.transform.childCount; ++i)
        {
            if (instance.slotGrid.transform.childCount == 0)
                break;
            Destroy(instance.slotGrid.transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < instance.myBag.itemList.Count; ++i)
        {
            CreateNewItem(instance.myBag.itemList[i]);
        }
    }

    public static void CreateNewItem(Item item)
    {
        Slot newItem = Instantiate(instance.slotPrefab, instance.slotGrid.transform.position, Quaternion.identity);
        newItem.gameObject.transform.SetParent(instance.slotGrid.transform);
        newItem.slotItem = item;
        newItem.slotImage.sprite = item.itemImage;
        newItem.slotNum.text = item.itemHeld.ToString();
        newItem.transform.localScale = new Vector3(1.1f, 1.1f, 1.0f);
    }

    public static void RefreshItem()
    {
        for (int i = 0; i < instance.slotGrid.transform.childCount; ++i)
        {
            if (instance.slotGrid.transform.childCount == 0)
                break;
            Destroy(instance.slotGrid.transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < instance.myBag.itemList.Count; ++i)
        {
            CreateNewItem(instance.myBag.itemList[i]);
        }
    }

    public static void UpdateItemInfo(String itemDescription)
    {
        instance.itemInfo.text = itemDescription;
    }

    public static void UpdateCurrentItem(Item item)
    {
        instance.currentItem = item;
    }

    public void EndGame()
    {
        Application.Quit();
    }
}
