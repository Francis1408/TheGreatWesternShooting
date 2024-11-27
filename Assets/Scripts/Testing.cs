using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;


public class Testing : MonoBehaviour
{
    // FOR THE HEATMAP
    /*
    // Start is called before the first frame update
    [SerializeField] private HeatMapVisual heatMapVisual;

    private Grid grid;
    private void Start()
    {
        
        grid = new Grid(150, 100, 2f, Vector3.zero);
        heatMapVisual.SetGrid(grid);
        
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 position = UtilsClass.GetMouseWorldPosition();
            grid.AddValue(position, 100, 2, 25);
        }
        
    }
    */
    private Pathfinding pathfinding;
    private void Start()
    {
        pathfinding = new Pathfinding(10, 10);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition();
            pathfinding.GetGrid().GetXY(mouseWorldPosition, out int x, out int y);
            List<PathNode> path = pathfinding.FindPath(0, 0, x, y);
            if (path != null)
            {
                for (int i = 0; i < path.Count - 1; i++)
                {
                    Debug.DrawLine(new Vector3(path[i].x, path[i].y) * 10f + Vector3.one * 5f, new Vector3(path[i+1].x, path[i+1].y) * 10f + Vector3.one *5f);
                }
            }
        }
    }
}
