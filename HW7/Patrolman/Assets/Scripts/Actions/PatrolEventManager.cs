using UnityEngine;

public class PatrolEventManager {
    // singleton instance
    private static PatrolEventManager instance;

    // hit player event
    public delegate void HitPlayerAction(GameObject patrol);
    public static event HitPlayerAction OnHitPlayer;

    // hit obstacle event
    public delegate void HitObstacleAction(GameObject patrol);
    public static event HitObstacleAction OnHitObstacle;

    // see player event
    public delegate void SeePlayerAction(GameObject patrol);
    public static event SeePlayerAction OnSeePlayer;

    // lose player event
    public delegate void LosePlayerAction(GameObject patrol);
    public static event LosePlayerAction OnLosePlayer;

    // stop event
    public delegate void StopAction(GameObject patrol);
    public static event StopAction OnStop;

    public static PatrolEventManager GetInstance() {
        if (instance == null) {
            instance = new PatrolEventManager();
        }
        return instance;
    }

    public void HitPlayer(GameObject patrol) {
        OnHitPlayer?.Invoke(patrol);
    }

    public void HitObstacle(GameObject patrol) {
        OnHitObstacle?.Invoke(patrol);
    }

    public void SeePlayer(GameObject patrol) {
        OnSeePlayer?.Invoke(patrol);
    }

    public void LosePlayer(GameObject patrol) {
        OnLosePlayer?.Invoke(patrol);
    }

    public void Stop(GameObject patrol) {
        OnStop?.Invoke(patrol);
    }
}
