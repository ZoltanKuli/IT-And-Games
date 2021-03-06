using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGrid {

    private CellType[,] worldGridMatrix;
    private int width;
    private int height;

    private List<Cell> roadList;
    private List<Cell> specialStructureList;

    public WorldGrid(int width, int height) {
        worldGridMatrix = new CellType[width, height];
        this.width = width;
        this.height = height;

        roadList = new List<Cell>();
        specialStructureList = new List<Cell>();
    }

    public CellType this[int xCoordinate, int yCoordinate] {
        get {
            return worldGridMatrix[xCoordinate, yCoordinate];
        }

        set {
            if (value == CellType.Road) {
                roadList.Add(new Cell(xCoordinate, yCoordinate));
            } else {
                roadList.Remove(new Cell(xCoordinate, yCoordinate));
            }

            if (value == CellType.SpecialStructure) {
                specialStructureList.Add(new Cell(xCoordinate, yCoordinate));
            } else {
                specialStructureList.Remove(new Cell(xCoordinate, yCoordinate));
            }

            worldGridMatrix[xCoordinate, yCoordinate] = value;
        }
    }

    public int getWidth() {
        return width;
    }

    public int getHeight() {
        return height;
    }

    public List<Cell> getWalkableAdjacentCells(Cell cell, bool isAIAgent) {
        return getWalkableAdjacentCells(cell.getXCoordinate(), cell.getYCoordinate(), isAIAgent);
    }

    public List<Cell> getWalkableAdjacentCells(int xCoordinate, int yCoordinate, bool isAIAgent) {
        List<Cell> adjacentCells = getAllAdjacentCells(xCoordinate, yCoordinate);

        foreach (Cell adjacentCell in adjacentCells) {
            if (!isCellWalkable(worldGridMatrix[adjacentCell.getXCoordinate(), adjacentCell.getYCoordinate()], isAIAgent)) {
                adjacentCells.Remove(adjacentCell);
            }
        }

        return adjacentCells;
    }

    public static bool isCellWalkable(CellType cellType, bool isAIAgent = false) {
        if (isAIAgent) {
            return cellType == CellType.Road;
        }

        return cellType == CellType.Empty || cellType == CellType.Road;
    }

    public List<Cell> getAdjacentCellsOfType(int xCoordinate, int yCoordinate, CellType cellType) {
        List<Cell> adjacentCells = getAllAdjacentCells(xCoordinate, yCoordinate);

        foreach (Cell adjacentCell in adjacentCells) {
            if (worldGridMatrix[adjacentCell.getXCoordinate(), adjacentCell.getYCoordinate()] != cellType) {
                adjacentCells.Remove(adjacentCell);
            }
        }

        return adjacentCells;
    }

    public List<Cell> getAllAdjacentCells(int xCoordinate, int yCoordinate) {
        List<Cell> adjacentCells = new List<Cell>();

        if (xCoordinate > 0) {
            adjacentCells.Add(new Cell(xCoordinate - 1, yCoordinate));
        }

        if (xCoordinate < width - 1) {
            adjacentCells.Add(new Cell(xCoordinate + 1, yCoordinate));
        }

        if (yCoordinate > 0) {
            adjacentCells.Add(new Cell(xCoordinate, yCoordinate - 1));
        }

        if (yCoordinate < height - 1) {
            adjacentCells.Add(new Cell(xCoordinate, yCoordinate + 1));
        }

        return adjacentCells;
    }

    public CellType[] getAllAdjacentCellTypes(int xCoordinate, int yCoordinate) {
        CellType[] adjacentCellTypes = { CellType.None, CellType.None, CellType.None, CellType.None };
        
        if (xCoordinate > 0) {
            adjacentCellTypes[0] = worldGridMatrix[xCoordinate - 1, yCoordinate];
        }

        if (xCoordinate < width - 1) {
            adjacentCellTypes[2] = worldGridMatrix[xCoordinate + 1, yCoordinate];
        }

        if (yCoordinate > 0) {
            adjacentCellTypes[3] = worldGridMatrix[xCoordinate, yCoordinate - 1];
        }

        if (yCoordinate < height - 1) {
            adjacentCellTypes[1] = worldGridMatrix[xCoordinate, yCoordinate + 1];
        }

        return adjacentCellTypes;
    }

    public Cell getRandomSpecialStructureCell() {
        System.Random rand = new System.Random();
        return specialStructureList[rand.Next(0, specialStructureList.Count - 1)];
    }

    public Cell getRandomRoadCell() {
        System.Random random = new System.Random();
        return roadList[random.Next(0, roadList.Count - 1)];
    }

    public float getCostOfEnteringCell(Cell cell) {
        return 1;
    }
}
