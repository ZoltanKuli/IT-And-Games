using System;
using System.Collections.Generic;

public class PathFinder {

    /// <summary>
    /// Find path.
    /// </summary>
    public static List<Field> FindPath(WorldManager worldManager, Field startField, Field destinationField, bool isAIAgent) {
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

            foreach (Field walkabledjacentField in worldManager.GetWalkableAdjacentFields(currentField, isAIAgent)) {
                float newCost = costDictionary[currentField] + worldManager.GetCostOfEnteringField(walkabledjacentField);
                
                if (!costDictionary.ContainsKey(walkabledjacentField) || newCost < costDictionary[walkabledjacentField]) {
                    costDictionary[walkabledjacentField] = newCost;

                    float priority = newCost + GetManhattanDistance(destinationField, walkabledjacentField);
                    FieldsTocheck.Add(walkabledjacentField);
                    priorityDictionary[walkabledjacentField] = priority;

                    parentsDictionary[walkabledjacentField] = currentField;
                }
            }
        }

        return path;
    }

    /// <summary>
    /// Get closest vertex.
    /// </summary>
    private static Field GetClosestVertex(List<Field> list, Dictionary<Field, float> distanceMap) {
        Field candidate = list[0];
        foreach (Field vertex in list) {
            if (distanceMap[vertex] < distanceMap[candidate]) {
                candidate = vertex;
            }
        }

        return candidate;
    }

    /// <summary>
    /// Get manhattan distance.
    /// </summary>
    private static float GetManhattanDistance(Field endPos, Field field) {
        return Math.Abs(endPos.GetOrigoPosition().x - field.GetOrigoPosition().x) + Math.Abs(endPos.GetOrigoPosition().z - field.GetOrigoPosition().z);
    }

    /// <summary>
    /// Generate path.
    /// </summary>
    private static List<Field> GeneratePath(Dictionary<Field, Field> parentMap, Field endState) {
        List<Field> path = new List<Field>();

        Field parent = endState;
        while (parent != null && parentMap.ContainsKey(parent)) {
            path.Add(parent);
            parent = parentMap[parent];
        }

        return path;
    }
}
