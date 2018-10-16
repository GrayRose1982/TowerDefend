using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandCreation : MonoBehaviour {
    public static LandCreation Instance;

    public GameObject[] Land;
    public float Distance = .2f;

    public List<Vector2> PathMove;

    public int[,] Map = new int[,]
    {
        {1, 1, 1, 1, 1, 1, 1, 1, 1,1},
        {1, 2, 0, 0, 0, 1, 1, 1, 1,1},
        {1, 1, 1, 1, 0, 1, 1, 1, 1,1},
        {1, 1, 1, 1, 0, 1, 1, 1, 1,1},
        {1, 1, 1, 1, 0, 0, 0, 1, 1,1},
        {1, 1, 1, 1, 1, 1, 0, 1, 1,1},
        {1, 1, 1, 1, 1, 1, 0, 1, 1,1},
        {1, 1, 1, 1, 1, 1, 0, 0, 3,1},
        {1, 1, 1, 1, 1, 1, 1, 1, 1,1},
    };

    void Awake() {
        Instance = this;
    }

    [ContextMenu("Create Land")]
    public void CreateLandFollowMatrix() {
        if (PathMove == null)
            PathMove = new List<Vector2>();
        else PathMove.Clear();

        while (0 < transform.childCount)
            DestroyImmediate(transform.GetChild(0).gameObject);

        var size = 1f;

        var startPosition = Vector2.zero;
        var morePosition = Vector2.zero;

        morePosition.x = Map.GetLength(1) % 2 != 0 ? 0 : (size + Distance) / 2;
        morePosition.y = Map.GetLength(0) % 2 != 0 ? 0 : (size + Distance) / 2;

        startPosition.x = -Mathf.FloorToInt(Map.GetLength(1) / 2) * (size + Distance);
        startPosition.y = -Mathf.FloorToInt(Map.GetLength(0) / 2) * (size + Distance);

        startPosition += morePosition;



        for (int i = 0; i < Map.GetLength(1); i++)
            for (int j = 0; j < Map.GetLength(0); j++) {
                var newPos = Vector2.zero;
                newPos.x = i * Distance + i * size;
                newPos.y = j * Distance + j * size;
                newPos += startPosition;
                newPos.y = -newPos.y;

                if (Map[j, i] != 1)
                    PathMove.Add(newPos);

                if (Map[j, i] != 0) {
                    var newLand = Instantiate(Land[Map[j, i]], transform);
                    newLand.transform.localPosition = newPos;

                    if (Map[j, i] == 1)
                    {
                        newLand.GetComponent<LandSelection>();
                    }
                }

            }

        Debug.Log("Create lane done!!");
    }


#if UNITY_EDITOR
    void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        for (int i = 0; i < PathMove.Count - 1; i++) {
            Gizmos.DrawLine(PathMove[i], PathMove[i + 1]);
        }
    }
#endif
}
