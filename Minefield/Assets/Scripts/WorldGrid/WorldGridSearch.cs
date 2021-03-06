using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WorldGridSearch {

    public static List<Cell> aStarSearch(WorldGrid worldGrid, Cell startCell, Cell destinationCell, bool isAIAgent = false) {
        List<Cell> path = new List<Cell>();

        List<Cell> cellsTocheck = new List<Cell>();
        Dictionary<Cell, float> costDictionary = new Dictionary<Cell, float>();
        Dictionary<Cell, float> priorityDictionary = new Dictionary<Cell, float>();
        Dictionary<Cell, Cell> parentsDictionary = new Dictionary<Cell, Cell>();

        cellsTocheck.Add(startCell);
        priorityDictionary.Add(startCell, 0);
        costDictionary.Add(startCell, 0);
        parentsDictionary.Add(startCell, null);

        while (cellsTocheck.Count > 0) {
            Cell currentCell = getClosestVertex(cellsTocheck, priorityDictionary);
            cellsTocheck.Remove(currentCell);
            if (currentCell.Equals(destinationCell)) {
                path = generatePath(parentsDictionary, currentCell);
                return path;
            }

            foreach (Cell adjacentCell in worldGrid.getWalkableAdjacentCells(currentCell, isAIAgent)) {
                float newCost = costDictionary[currentCell] + worldGrid.getCostOfEnteringCell(adjacentCell);

                if (!costDictionary.ContainsKey(adjacentCell) || newCost < costDictionary[adjacentCell]) {
                    costDictionary[adjacentCell] = newCost;

                    float priority = newCost + manhattanDiscance(destinationCell, adjacentCell);
                    cellsTocheck.Add(adjacentCell);
                    priorityDictionary[adjacentCell] = priority;

                    parentsDictionary[adjacentCell] = currentCell;
                }
            }
        }

        return path;
    }

    private static Cell getClosestVertex(List<Cell> list, Dictionary<Cell, float> distanceMap) {
        Cell candidate = list[0];
        foreach (Cell vertex in list) {
            if (distanceMap[vertex] < distanceMap[candidate]) {
                candidate = vertex;
            }
        }

        return candidate;
    }

    private static float manhattanDiscance(Cell endPos, Cell cell) {
        return Math.Abs(endPos.getXCoordinate() - cell.getXCoordinate()) + Math.Abs(endPos.getYCoordinate() - cell.getYCoordinate());
    }

    public static List<Cell> generatePath(Dictionary<Cell, Cell> parentMap, Cell endState) {
        List<Cell> path = new List<Cell>();

        Cell parent = endState;
        while (parent != null && parentMap.ContainsKey(parent)) {
            path.Add(parent);
            parent = parentMap[parent];
        }

        return path;
    }
}
