using UnityEngine;
using UnityEngine.Events;

public class Bear : MonoBehaviour
{
    #region Events

    UnityEvent OnDamage;
    
    UnityEvent OnHeal;

    UnityEvent OnDeath;

    #endregion

    #region Attributes

    [SerializeField] private int live = 3;

    #endregion

    #region Properties

    #endregion

    #region Methods

    private void Start()
    {
        OnDamage.RemoveListener(ReduceLive);
        OnDamage.AddListener(ReduceLive);
        
        OnHeal.RemoveListener(IncreaseLive);
        OnHeal.AddListener(IncreaseLive);
    }

    private void ReduceLive()
    {
        live--;
        if (live <= 0)
        {
            OnDeath.Invoke();
        }
    }

    private void IncreaseLive()
    {
        live++;
    }

    #endregion
}