using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    public GameObject playerDeathPrefab;
    public AudioClip deathClip;
    public Sprite hitSprite;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D coll)
    {   // 1  looks for the player tag  
        if (coll.transform.tag == "Player")
        {     // 2    plays ddeath clip if the gameonject has a audio source
            var audioSource = GetComponent<AudioSource>();

            if (audioSource != null && deathClip != null)
            {
                audioSource.PlayOneShot(deathClip);
            }
            // 3 swap the sprite of the saw blade with the hitSprite version. 
            Instantiate(playerDeathPrefab, coll.contacts[0].point, Quaternion.identity);
            spriteRenderer.sprite = hitSprite;
            // 4  kills the player and destroys it
            Destroy(coll.gameObject);
            GameManager.instance.RestartLevel(1.25f);
        }
    } 
}
