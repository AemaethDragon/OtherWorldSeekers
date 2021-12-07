using UnityEngine;

public class WorkerHealth : MonoBehaviour
{
    #region Variables
    //Public
    [SerializeField] public HealthBar healthBar;
    [SerializeField] public int workerCurrentHealth;
    [SerializeField] public int workerMaxHealth;

    #endregion


    #region Methods

    private void Start()
    {
        workerCurrentHealth = workerMaxHealth;
        healthBar.SetMaxHealth(workerMaxHealth);
    }

    #endregion
}
