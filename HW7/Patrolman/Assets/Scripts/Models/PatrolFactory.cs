using UnityEngine;

public class PatrolFactory {
    // singleton instance
    private static PatrolFactory instance;

    public static PatrolFactory GetInstance() {
        if (instance == null) {
            instance = new PatrolFactory();
        }
        return instance;
    }

    public GameObject GetPatrol(float x, float z) {
        GameObject patrol = Loader.LoadObj("Prefabs/Patrol", new Vector3(x, 0f, z));
        patrol.AddComponent<PatrolCollide>();
        patrol.AddComponent<PatrolTrigger>();
        patrol.AddComponent<PatrolAction>();
        return patrol;
    }
}
