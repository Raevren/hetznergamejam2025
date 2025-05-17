using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    #region Attributes

    [SerializeField] private int health = 3;

    [SerializeField] private int maxHealth = 5;

    [SerializeField] private HealthBar healthBar;

    #endregion

    #region Properties

    public int Health
    {
        get => health;
    }

    #endregion

    #region Methods

    private void Update()
    {
        var acceptableAngle = 70;
        var acceptableAngleOtherDIrefction = 360 - 70;

        var absAngle = Mathf.Abs(transform.eulerAngles.z);
        if (absAngle < acceptableAngle || absAngle > acceptableAngleOtherDIrefction)
        {
            return;
        }

        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 0);
        ReduceLive();
    }

    private void ReduceLive()
    {
        health--;
        healthBar.RenderHealth();

        if (health <= 0)
        {
            SceneManager.LoadScene(1);
        }
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