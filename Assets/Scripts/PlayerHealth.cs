using System;
using System.Collections;
using UI;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    #region Attributes

    [SerializeField] private int health = 3;

    [SerializeField] private int maxHealth = 5;

    [SerializeField] private HealthBar healthBar;
    
    [SerializeField] private GameObject damageEffect;

    #endregion

    #region Properties

    public int Health
    {
        get => health;
    }

    private bool isRespawning = false;
    public bool IsAllowedToMove => Health > 0 && !isRespawning;
    public event Action OnRemoveHealth;

    #endregion

    #region Methods

    private void Update()
    {
        if(!IsAllowedToMove) return;
        var acceptableAngle = 70;
        var acceptableAngleOtherDIrefction = 360 - 70;

        var absAngle = Mathf.Abs(transform.eulerAngles.z);
        if (absAngle < acceptableAngle || absAngle > acceptableAngleOtherDIrefction)
        {
            return;
        }

        ReduceLive();
    }

    private void ReduceLive()
    {
        health--;
        OnRemoveHealth?.Invoke();
        healthBar.RenderHealth();

        if (health <= 0)
        {
            LoadGameOver();
        }
        else
        {
            StartCoroutine(Respawn());
        }
    }

    private IEnumerator Respawn()
    {
        isRespawning = true;
        damageEffect.SetActive(true);
        damageEffect.GetComponent<Animation>().Play();
        yield return new WaitForSeconds(0.5f);
        //TODO good animation
        isRespawning = false;
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 0);
    }

    private void LoadGameOver()
    {
        Instantiate(Resources.Load<GameObject>("Prefabs/BearGameOver"));
        
        var sceneLoader = Instantiate(Resources.Load<SceneTransitioner>("Prefabs/CanvasLoadScene"));
        sceneLoader.StartCoroutine(sceneLoader.LoadSceneIn(2f, 1));
        gameObject.SetActive(false);
    }

    private void IncreaseHealth()
    {
        if (health < maxHealth)
        {
            health++;
            healthBar.RenderHealth();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Wird beim Betreten des Triggers ausgeführt
        if (other.gameObject.CompareTag("Health"))
        {
            IncreaseHealth();
        }
    }

    #endregion
}