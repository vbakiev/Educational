using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{


    public int maxHealth;
    public int currentHealth;

    public PlayerController thePlayer;

    //Immunity length
    public float invincibilityLength;
    private float invinvibilityCounter;

    public Renderer playerRenderer;
    private float flashCounter;
    public float flashLength = 0.2f;

    //Respawning and point of respawn
    private bool IsRespawning;

    private Vector3 respawnPoint;
    //public float respawnLength;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
  
        //thePlayer = FindObjectOfType<PlayerController>();

        respawnPoint = thePlayer.transform.position;
    }

    // Update is called once per frame
    // if statments for immunity and its length
    void Update()
    {
        if(invinvibilityCounter > 0)
        {
            invinvibilityCounter -= Time.deltaTime;

            flashCounter -= Time.deltaTime;
            if(flashCounter <= 0)
            {
                playerRenderer.enabled = !playerRenderer.enabled;
                flashCounter = flashLength;
            }

            if(invinvibilityCounter <= 0)
            {
                playerRenderer.enabled = true;
            }
        }
    }

    //Damage done to player
    public void HurtPlayer(int damage, Vector3 direction)
    {
        if (invinvibilityCounter <= 0)
        {
            currentHealth -= damage;

            if (currentHealth <= 0)
            {
                Respawn();
            }
            else
            {
                //Knockback if health isn't 0
                thePlayer.KnockBack(direction);

                invinvibilityCounter = invincibilityLength;

                playerRenderer.enabled = false;

                flashCounter = flashLength;
            }
            
        }
    }

    //Respawning function
    public void Respawn()
    {
        if (!IsRespawning)
        {
            StartCoroutine("RespawnCo");
        }
    }
  

    public IEnumerator RespawnCo()
    {
        IsRespawning = true;
        thePlayer.gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);
        IsRespawning = false;
        thePlayer.gameObject.SetActive(true);

        GameObject player = GameObject.Find("Player");
        CharacterController charController = player.GetComponent<CharacterController>();

        charController.enabled = false;
        thePlayer.transform.position = respawnPoint;
        charController.enabled = true;

        currentHealth = maxHealth;

        invinvibilityCounter = invincibilityLength;
        playerRenderer.enabled = false;
        flashCounter = flashLength;

    }
 

    // Healing the player
    public void HealPlayer(int healAmount)
    {
        currentHealth += healAmount;

        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    //Saving progress made
    public void SetSpawnPoint(Vector3 newPosition)
    {
        respawnPoint = newPosition;
    }

}