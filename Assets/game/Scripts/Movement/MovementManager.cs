using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager: MonoBehaviour
{
    static float xOffset = 1.503f;
    static float yOffset = 1.735f;
    private GameManager gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public static IEnumerator MoveCoroutine(List<Vector2> nodes, GameObject character)
    {
        SelectionManager.selectedAnimatable.WalkBool(true);
        foreach (Vector2 node in nodes)
        {
            if (node.x % 2 != 0)
            {
                Vector3 tempVector = new Vector3(node.x * xOffset, 1, node.y * yOffset + yOffset / 2);
                Vector3 tempvector2 = tempVector - character.transform.position;
                character.transform.rotation = Quaternion.LookRotation(tempvector2);
                
                yield return new WaitUntil(() => FinalMove(character, tempVector) == tempVector);
            }
            else
            {
                Vector3 tempVector = new Vector3(node.x * xOffset, 1, node.y * yOffset);
                Vector3 tempvector2 = tempVector - character.transform.position;
                character.transform.rotation = Quaternion.LookRotation(tempvector2);
                
                yield return new WaitUntil(() => FinalMove(character, tempVector) == tempVector);
            }
        }
        
        SelectionManager.selectedAnimatable.WalkBool(false);
        
        if (character.GetComponent<Enemy>() != null)
        {
            character.GetComponent<Enemy>().iEnemy.Rotation();
            yield return new WaitForSeconds(5f);
        }
        
        SelectionManager.ClearSelectedEnemy();
    }   

    private static Vector3 FinalMove(GameObject character, Vector3 targetPos)
    {
        return character.transform.position = Vector3.MoveTowards(character.transform.position, targetPos, Time.deltaTime * 5f);
    }

    public static IEnumerator AbilityMovementCoroutine(GameObject character, float animationTime, params Vector3[] positionsToMove)
    {
        while (true)
        {
            character.transform.position = Vector3.MoveTowards(positionsToMove[0], positionsToMove[1], Time.deltaTime * animationTime);
            yield return new WaitUntil(() => character.transform.position == positionsToMove[1]);
        }
    }

    public static Vector3 AbilityMovement(GameObject character, float animationTime, params Vector3[] positionsToMove)
    {
        return character.transform.position = Vector3.MoveTowards(positionsToMove[0], positionsToMove[1], Time.deltaTime * animationTime);        
    }
}
