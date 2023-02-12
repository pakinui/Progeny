using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    public GameObject toggle;
    public int maxHealth;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    
    // Start is called before the first frame update
    private Player player;
    private int health = 0;
    
    private float toggleWidth = 18;
    private Image[] healthTogs;

    private void Start()
    {
        var rectTransform = GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(toggleWidth * maxHealth + rectTransform.sizeDelta.x, rectTransform.sizeDelta.y);
        player = GameObject.Find("Player").GetComponent<Player>();
        maxHealth = 10;
        healthTogs = new Image[maxHealth];
        for (var i = 0; i < maxHealth; i++)
        {
            var tog = Instantiate(toggle, transform);
            tog.transform.localPosition += new Vector3(toggleWidth * i, 0, 0);
            tog.SetActive(true);
            healthTogs[i] = tog.GetComponent<Image>();
        }
    }

    float getHealth()
    {
        return player.GetCurrentHealth() / player.maxHealth;
    }
    
    // Update is called once per frame
    private void Update()
    {
        // ceil rounds up
        var targetHealth = Mathf.CeilToInt(getHealth() * maxHealth);
        if (targetHealth < health)
        {
            healthTogs[health - 1].sprite = emptyHeart;
            health--;
        }
        else if (targetHealth > health)
        {
            healthTogs[health].sprite = fullHeart;
            health++;
        }
    }
}