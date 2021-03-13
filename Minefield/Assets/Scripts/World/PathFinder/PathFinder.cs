using System;
using System.Collections.Generic;

public class PathFinder {

    public static List<Field> FindPath(WorldManager world, Field startField, Field destinationField, bool isAIAgent = false) {
        List<Field> path = new List<Field>();

        List<Field> FieldsTocheck = new List<Field>();
        Dictionary<Field, float> costDictionary = new Dictionary<Field, float>();
        Dictionary<Field, float> priorityDictionary = new Dictionary<Field, float>();
        Dictionary<Field, Field> parentsDictionary = new Dictionary<Field, Field>();

        FieldsTocheck.Add(startField);
        priorityDictionary.Add(startField, 0);
        costDictionary.Add(startField, 0);
        parentsDictionary.Add(startField, null);

        while (FieldsTocheck.Count > 0) {
            Field currentField = GetClosestVertex(FieldsTocheck, priorityDictionary);
            FieldsTocheck.Remove(currentField);
            if (currentField.Equals(destinationField)) {
                path = GeneratePath(parentsDictionary, currentField);
                return path;
            }

            foreach (Field adjacentField in world.GetWalkableAdjacentFields(currentField, isAIAgent)) {
                float newCost = costDictionary[currentField] + world.GetCostOfEnteringField(adjacentField);

                if (!costDictionary.ContainsKey(adjacentField) || newCost < costDictionary[adjacentField]) {
                    costDictionary[adjacentField] = newCost;

                    float priority = newCost + ManhattanDistance(destinationField, adjacentField);
                    FieldsTocheck.Add(adjacentField);
                    priorityDictionary[adjacentField] = priority;

                    parentsDictionary[adjacentField] = currentField;
                }
            }
        }

        return path;
    }

    private static Field GetClosestVertex(List<Field> list, Dictionary<Field, float> distanceMap) {
        Field candidate = list[0];
        foreach (Field vertex in list) {
            if (distanceMap[vertex] < distanceMap[candidate]) {
                candidate = vertex;
            }
        }

        return candidate;
    }

    private static float ManhattanDistance(Field endPos, Field field) {
        return Math.Abs(endPos.getMainPosition().x - field.getMainPosition().x) + Math.Abs(endPos.getMainPosition().z - field.getMainPosition().z);
    }

    public static List<Field> GeneratePath(Dictionary<Field, Field> parentMap, Field endState) {
        List<Field> path = new List<Field>();

        Field parent = endState;
        while (parent != null && parentMap.ContainsKey(parent)) {
            path.Add(parent);
            parent = parentMap[parent];
        }

        return path;
    }
}
