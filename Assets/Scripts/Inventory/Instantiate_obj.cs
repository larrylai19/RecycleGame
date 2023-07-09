using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiate_obj : MonoBehaviour
{
    public Transform obj_class;
    static Instantiate_obj instance;

    public List<Tuple<float, float>> position = new List<Tuple<float, float>>
    {
        new Tuple<float, float>(-7f, 16.5f),
        new Tuple<float, float>(-13.75f, 2f),
        new Tuple<float, float>(-18.63f, -0.87f),
        new Tuple<float, float>(-31.23f, -6.02f),
        new Tuple<float, float>(-29.96f, -0.53f),
        new Tuple<float, float>(-0.33f, 0.11f),
        new Tuple<float, float>(3.94f, -7.6f),
        new Tuple<float, float>(8.11f, -11.87f),
        new Tuple<float, float>(15.31f, -4.82f),
        new Tuple<float, float>(14.79f, 1.52f),
        new Tuple<float, float>(16.66f, 4.9f),
        new Tuple<float, float>(24.71f, 8.6f),
        new Tuple<float, float>(20.93f, 16.29f),
        new Tuple<float, float>(10.12f, 11.7f),
        new Tuple<float, float>(3.7f, 3.75f)
    };

    public List<int> idx = new List<int>();

    public List<string> trash = new List<string>
    {
        "CD","Cooking","Cosmetic","Dumpling","Egg","Halmet","Smoke","Sport","YaKerLi","ZLerBau"
    };

    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        instance = this;
    }

    private void Start()
    {
        for(int i=0;i<10;++i)
        {
            SpawnObj(trash[i]);
        }
    }


    
    public void SpawnObj(string itemName)
    {
        //Debug.Log("Generate");
        //Debug.Log(item.itemName);
        var (x, y) = position[GetRandomIdx()];
        GameObject obj = (GameObject)Instantiate(Resources.Load(itemName), new Vector3(x, y, 0.0f), Quaternion.identity, obj_class);
    }

    public static void Handler(Item item)
    {
        instance.SpawnObj(item.itemName);
    }
    
    public int GetRandomIdx()
    {
        int tmp = 0;
        while(true)
        {
            tmp = UnityEngine.Random.Range(0, 14);
            if (!idx.Contains(tmp))
            {
                idx.Add(tmp);
                break;
            }
            else if (idx.Count >= 15)
                idx.Clear();
        }
        return tmp;
    }
}
