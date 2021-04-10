using UnityEngine;

public class CrewStation : Structure {

    protected Crew crew;

    public CrewStation(GameObject prefab, Vector3Int origoPosition, 
        Vector3 prefabOffset, float yAngle, WorldManager worldManager, Crew crew)
        : base(prefab, origoPosition, prefabOffset, 
            yAngle, 1, 1, 0, 0, 0, 0, worldManager) {
        this.crew = crew;
    }

    /// <summary>
    /// Does crew have field to do action on.
    /// </summary>
    public bool DoesCrewHaveFieldToDoActionOn() {
        return crew.HasFieldToDoActionOn();
    }

    /// <summary>
    /// Set field for crew to do action on.
    /// </summary>
    public void SetFieldForCrewToDoActionOn(Field field) {
        crew.SetFieldToDoActionOn(field);
    }

    /// <summary>
    /// Update crew.
    /// </summary>
    public void UpdateCrew() {
        crew.Update();
    }

    /// <summary>
    /// Destroy game object.
    /// </summary>
    public override void DestroyGameObject() {
        GameObject.Destroy(gameObject);
        crew.DestroyGameObject();
        gameObject = null;
        isDestroyed = true;
    }
}
