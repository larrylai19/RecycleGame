using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TrashBar : MonoBehaviour
{
    public TextMeshProUGUI TrashText;
    public static int TrashCount;
    public static int TrashCountMax = 10;

    private Image trashBar;

    // Start is called before the first frame update
    void Start()
    {
        trashBar = GetComponent<Image>();
        TrashCount = TrashCountMax;
    }

    // Update is called once per frame
    void Update()
    {
        trashBar.fillAmount = (float)TrashCount / (float)TrashCountMax;
        TrashText.text = TrashCount.ToString();
    }
}
