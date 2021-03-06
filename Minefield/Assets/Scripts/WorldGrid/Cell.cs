using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell {

    public int xCoordinate;
    public int yCoordinate;

    public Cell(int xCoordinate, int yCoordinate) {
        this.xCoordinate = xCoordinate;
        this.yCoordinate = yCoordinate;
    }

    public int GetXCoordinate() {
        return xCoordinate;
    }

    public int GetYCoordinate() {
        return yCoordinate;
    }

    public override bool Equals(object obj) {
        if (obj == null) {
            return false;
        }

        if (obj is Cell) {
            Cell cell = obj as Cell;

            return this.xCoordinate == cell.xCoordinate 
                && this.yCoordinate == cell.yCoordinate;
        }

        return false;
    }

    public override int GetHashCode() {
        unchecked {
            int hash = 6949;
            hash = hash * 7907 + xCoordinate.GetHashCode();
            hash = hash * 7907 + yCoordinate.GetHashCode();
            return hash;
        }
    }

    public override string ToString() {
        return "Cell(" + this.xCoordinate + ", " + this.yCoordinate + ")";
    }
}
