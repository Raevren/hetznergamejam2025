using System;
using UnityEngine;

public class Bear : MonoBehaviour
{
    #region Events

    Action OnDamage;

    Action OnHeal;

    Action OnDeath;

    #endregion

    #region Attributes

    [SerializeField] private int live = 3;

    #endregion

    #region Properties

    #endregion

    #region Methods

    private void Start()
    {
        OnDamage -= ReduceLive;
        OnDamage += ReduceLive;

        OnHeal -= IncreaseLive;
        OnHeal += IncreaseLive;
    }

    private void Update()
    {
        if (Math.Abs(transform.rotation.z) < 70) return;
        
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 0);
        ReduceLive();
    }

    private void ReduceLive()
    {
        live--;
        if (live <= 0)
        {
            OnDeath?.Invoke();
        }
    }

    private void IncreaseLive()
    {
        live++;
    }

    #endregion
}