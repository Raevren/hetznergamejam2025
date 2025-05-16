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