using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HeroControll : MonoBehaviour
{
    public Rigidbody2D rb;
    // direction: 0: �V�U��, 1: �V�W��, -1: �V���k
    public Animator anim;
    public float speed;

    private Vector3 startPos;

    // �^�{�ʵe
    public RawImage rawImage;
    private float fadeSpeed = 10f;
    private bool isFading = false;
    
    // bag
    public GameObject myBag;
    public bool isOpen = false;

    // �H�����˰{�{
    private Renderer myRender;
    public int blinks;
    public float blinkTime;
    public AudioClip hurtSound;

    // Start is called before the first frame update
    void Start()
    {
        // �����H����l��m
        startPos = transform.localPosition;

        myRender = GetComponent<Renderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        HotKeys();
    }

    void FixedUpdate()
    {
        if (isFading)
        {
            FadeToClear();
        }
        Movement();
    }

    // �ֱ����T��
    void HotKeys()
    {
        // �^��H����l��m
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (isFading)
                return;
            transform.localPosition = startPos;
            anim.SetInteger("direction", 0);
            rawImage.enabled = true;
            isFading = true;
        }
        // ���}�I�]
        if (Input.GetKeyDown(KeyCode.I))
        {
            isOpen = !isOpen;
            myBag.SetActive(isOpen);
        }
    }

    void Movement()
    {
        float horizontalMove = Input.GetAxis("Horizontal");
        float verticalMove = Input.GetAxis("Vertical");
        float horizontalDirection = Input.GetAxisRaw("Horizontal");
        float verticalDirection = Input.GetAxisRaw("Vertical");

        // �]�w���ʦ�m
        rb.velocity = new Vector2(horizontalMove * speed * Time.deltaTime, verticalMove * speed * Time.deltaTime);

        // �]�w�����V�A�H������V���D
        if (horizontalDirection != 0)
        {
            transform.localScale = new Vector3(horizontalDirection * 3, 3, 3);
            anim.SetInteger("direction", -1);
            anim.SetBool("walking", true);
        }
        else if (verticalDirection != 0)
        {
            // verticalDirection �Y�� -1 ��ܴ¤U direction �]�� 0�A�Y�� 1 ��ܴ¤W direction �]�� 1
            anim.SetInteger("direction", Convert.ToInt32(Math.Max(0, verticalDirection)));
            anim.SetBool("walking", true);
        }
        
        
        if (horizontalMove == 0 && verticalMove == 0)
        {
            anim.SetBool("walking", false);
        }
    }

    void FadeToClear()
    {
        rawImage.color = Color.Lerp(rawImage.color, Color.clear, fadeSpeed * Time.deltaTime);
        if (rawImage.color.a < 0.05f)
        {
            rawImage.color = Color.black;
            rawImage.enabled = false;
            isFading = false;
        }
    }

    public void Damage(int damage)
    {
        AudioSource.PlayClipAtPoint(hurtSound, transform.position);
        HealthBar.health -= 1;
        BlinkPlayer(blinks, blinkTime);
    }

    void BlinkPlayer(int numBlinks, float seconds)
    {
        StartCoroutine(DoBlinks(numBlinks, seconds));
    }

    IEnumerator DoBlinks(int numBlinks, float seconds)
    {
        for (int i = 0; i < numBlinks * 2; ++i)
        {
            myRender.enabled = !myRender.enabled;
            yield return new WaitForSeconds(seconds);
        }
        myRender.enabled = true;
    }
}
