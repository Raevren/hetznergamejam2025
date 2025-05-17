using System;
using UnityEngine;

namespace UI
{
    public class HealthBar : MonoBehaviour
    {
        #region Attributes

        [SerializeField] private PlayerHealth playerHealth;

        [SerializeField] private GameObject healthPrefab;

        #endregion


        #region Methods

        private void Start()
        {
            RenderHealth();
        }

        public void RenderHealth()
        {
            int health = playerHealth.Health;
            int currentHealth = transform.childCount;
            int healthChange = health - currentHealth;

            if (healthChange > 0)
            {
                //add
                for (int i = 0; i < healthChange; i++)
                {
                    Instantiate(healthPrefab, transform);
                }
            }
            else
            {
                //remove
                for (int i = 0; i < Math.Abs(healthChange); i++)
                {
                    Destroy(transform.GetChild(i).gameObject);
                }
            }
        }

        #endregion
    }
}