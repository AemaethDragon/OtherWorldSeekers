using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerManager : MonoBehaviour
{/*
    #region variables

    //public
    public WorkerList workerListComponent;

    //Private
    private GameManager _gameManager;
    #endregion

    #region Methods
    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        workerListComponent = GetComponent<WorkerList>();
    }


    //public
    public IEnumerator ExecuteWorkerBehaviour()
    {
        foreach (Worker currentWorker in workerListComponent.workerList)
        {
            WorkerBehaviour(currentWorker);
            yield return new WaitForSeconds(0.2f);
        }
    }

    #endregion
    
    #region ToRemoverBehaviourTree

    private bool WorkerBehaviour(Worker behaveWorker)
    {
        WorkerSeek(behaveWorker, _gameManager.fieldManager.graph);

        return true;
    }


    private void WorkerSeek(Worker worker, Graph graph)
    {
        List<Vector2> path;

        if (worker.resQuantity == 40)
        {
            /*if (Utils.CreateRangeList(_gameManager.fieldManager.graph, worker.hexID, worker.Range, ListType.ATTACK).Contains(worker.targetS.hexID) && worker.resQuantity != 0)
            {
                worker.GiveResource();
            }
            else
            {
                path = Utils.GetBestRoute(worker.targetS.hexID, worker.hexID, ref graph, worker.Speed);
                worker.hexID = path[path.Count - 1];
                StartCoroutine(MovementManager.MoveCoroutine(path, worker.gameObject));
            }
        }
        else if (worker.resQuantity == 0)
        {
            if (Utils.CreateRangeList(_gameManager.fieldManager.graph, worker.hexID, worker.Range, ListType.ATTACK).Contains(worker.targetResource) && worker.resQuantity != 5)
            {
                worker.GatherResource();
            }
            else
            {
                path = Utils.GetBestRoute(worker.targetResource, worker.hexID, ref graph, worker.Speed);
                worker.hexID = path[path.Count - 1];
                StartCoroutine(MovementManager.MoveCoroutine(path, worker.gameObject));
            }#1#
        }
    }

    #endregion*/
}
