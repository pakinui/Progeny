using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisceateBarController : MonoBehaviour
{
    public GameObject toggle;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    
    // Start is called before the first frame update
    private Player player;
    private Gun gun;
    private int health = 0;
    private int maxHealth;
    private Image [] healthBlocks;
    private Image [] healthTogs;
    private bool holdGun = false;
    
    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        
        toggle.SetActive(false);
    }

    private void init()
    {
        maxHealth = gun.ammoCapacity;
        healthTogs = new Image[maxHealth];
        healthBlocks = new Image[maxHealth];
        for (var i = 0; i < maxHealth; i++)
        {
            
            var block = Instantiate(toggle, transform);
            block.transform.localPosition -= new Vector3(25 * i, 0, 0);
            block.SetActive(true);
            
            healthBlocks[i] = block.GetComponent<Image>();
            
            var tog = Instantiate(toggle, transform);
            tog.transform.localPosition -= new Vector3(25 * i, 0, 0);
            tog.SetActive(false);
            healthTogs[i] = tog.GetComponent<Image>();
            healthTogs[i].sprite = fullHeart;
            healthTogs[i].rectTransform.sizeDelta = fullHeart.rect.size;
                        
        }
    }
    
    private float getHealth()
    {
        return gun.ammoLeft * 1.0f / gun.ammoCapacity;
    }
    
    // Update is called once per frame
    private void Update()
    {
        if (player.gun != null && !holdGun)
        {
            holdGun = true;
            gun = player.gun;
            init();
        }
        else if (player.gun == null && holdGun)
        {
            holdGun = false;
        }
        
        // ceil rounds up
        var targetHealth = Mathf.CeilToInt(getHealth() * maxHealth);
        if (targetHealth < health)
        {
            healthTogs[health - 1].gameObject.SetActive(false);
            health--;
        }
        else if (targetHealth > health)
        {
            healthTogs[health].gameObject.SetActive(true);
            health++;
        }
    }
}
