using System;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        var acceptableAngle = 70;
        var acceptableAngleOtherDIrefction = 360 - 70;
        
        var absAngle = Mathf.Abs(transform.eulerAngles.z);
        if (absAngle < acceptableAngle || absAngle > acceptableAngleOtherDIrefction)
        {
            return;
        }
        
        Debug.Log("Death");
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 0);
        ReduceLive();
    }

    private void ReduceLive()
    {
        live--;
        if (live <= 0)
        {
            OnDeath?.Invoke();
            SceneManager.LoadScene(1);
        }
    }

    private void IncreaseLive()
    {
        live++;
    }

    #endregion
}