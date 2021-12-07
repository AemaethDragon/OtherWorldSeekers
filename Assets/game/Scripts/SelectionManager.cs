using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour
{
    #region Variables
    //Public
    public GameObject gameOptionsPanel;
    public Button attack;
    public static GameObject SelectedGameObject { get => selectedGameObject; }
    public static TeamCharacter SelectedPlayer { get => selectedPlayer; }
    public static Resource SelectedResource { get => selectedResource; }
    public static Hexagon SelectedHexagon { get => selectedHexagon; }
    public static Enemy SelectedEnemy { get => selectedEnemy; }
    public static ITargetable SelectedTargetable;
    public static IAnimatable selectedAnimatable;

    //Private
    private static GameObject selectedGameObject;
    private static TeamCharacter selectedPlayer;
    private static Resource selectedResource;
    private static Hexagon selectedHexagon;
    private static Enemy selectedEnemy;
    private static bool enabledReturn;

    #endregion

    #region Methods
    private void Start()
    {
        enabledReturn = true;
        selectedGameObject = null;
        selectedPlayer = null;
        selectedHexagon = null;
        selectedEnemy = null;
        selectedResource = null;
        selectedAnimatable = null;
        gameOptionsPanel.SetActive(false);
    }

    private void Update()
    {
        SelectGameObject();
    }

    

    //Public

    public static Hexagon SelectHexagonMouseOver()
    {
        if (enabledReturn)
        {
            GameObject temp = MouseManager.ReturnMouseOver();
            if (temp != null && temp.transform.tag == "Hex")
            {
                return temp.GetComponent<Hexagon>();
            }
        }
        return null;
    }

    public static GameObject SelectGameObject()
    {
        if (TestSelectGameObject())
        {
            selectedGameObject = TestSelectGameObject();
            return selectedGameObject;
        }
        return null;
    }

    public static void SelectHexagon(Hexagon hexagon)
    {
        selectedHexagon = hexagon;
    }

    public static void SelectPlayer(TeamCharacter teamCharacter)
    {
        selectedPlayer = teamCharacter;
    }

    public static void SelectEnemy(Enemy enemy)
    {
        selectedEnemy = enemy;
    }

    public static void SelectTargetable(ITargetable targetable)
    {
        SelectedTargetable = targetable;
    }

    public static void SelectAnimatable(IAnimatable animatable)
    {
        selectedAnimatable = animatable;
    }
    
    public static Resource SelectResource()
    {
        if (TestSelectResource())
        {
            selectedResource = TestSelectResource();

            return selectedResource;
        }
        return null;
    }

    public static GameObject TestSelectGameObject()
    {
        if (enabledReturn)
        {
            GameObject temp = MouseManager.ReturnMouseOne();
            if (temp != null)
            {
                return temp;
            }
        }
        return null;
    }

    public static Hexagon TestSelectHexagon()
    {
        if (enabledReturn)
        {
            GameObject temp = MouseManager.ReturnMouseOne();
            if (temp != null && temp.transform.tag == "Hex")
            {
                return temp.GetComponent<Hexagon>();
            }
        }
        return null;
    }

    public static TeamCharacter TestSelectPlayer()
    {
        if (enabledReturn)
        {
            GameObject temp = MouseManager.ReturnMouseOne();
            if (temp != null && temp.transform.tag == "Player")
            {
                return temp.GetComponent<TeamCharacter>();
            }
        }
        return null;
    }

    public static Enemy TestSelectEnemy()
    {
        if (enabledReturn)
        {
            GameObject temp = MouseManager.ReturnMouseOne();
            if (temp != null && temp.transform.tag == "Enemy")
            {
                return temp.GetComponent<Enemy>();
            }
        }
        return null;
    }

    public static Resource TestSelectResource()
    {
        if (enabledReturn)
        {
            GameObject temp = MouseManager.ReturnMouseOne();

            if (temp != null && temp.transform.name == "Resource")
            {
                return temp.GetComponent<Resource>();
            }
        }
        return null;
    }

    public static void ClearSelectedGameObject()
    {
        selectedGameObject = null;
    }

    public static void ClearSelectedHexagon()
    {
        selectedHexagon = null;
    }

    public static void ClearSelectedPlayer()
    {
        selectedPlayer = null;
    }

    public static void ClearSelectedEnemy()
    {
        selectedEnemy = null;
    }

    public static void ClearSelectedTargetable()
    {
        SelectedTargetable = null;
    }
    
    public static void ClearSelectedAnimatable()
    {
        selectedAnimatable = null;
    }
    
    public static void ClearSelectedResource()
    {
        selectedResource = null;
    }

    public static void CleanAllSelections()
    {
        selectedGameObject = null;
        selectedHexagon = null;
        selectedPlayer = null;
        selectedEnemy = null;
        selectedResource = null;
    }

    public static void EnableReturn()
    {
        enabledReturn = true;
    }

    public static void DisableReturn()
    {
        enabledReturn = false;
    }

    //Private

    #endregion
}