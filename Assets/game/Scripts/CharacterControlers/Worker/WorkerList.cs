using System.Collections.Generic;
using UnityEngine;

public class WorkerList : MonoBehaviour
{
    public List<Worker> workerList;
    
    
    
    #region Methods

    private void Awake()
    {
        workerList = new List<Worker>();
    }
    
    public void AddWorkerToList(Worker worker)
    {
        workerList.Add(worker);
    }

    public void RemoveWorkerFromList(Worker worker)
    {
        workerList.Remove(worker);
    }
    #endregion
}
