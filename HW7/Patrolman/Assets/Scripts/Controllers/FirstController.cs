using UnityEngine;
using System.Collections.Generic;

public class FirstController : MonoBehaviour, ISceneController, IUserAction {
    private GameObject player;
    private Judger judger;
    private GameGUI gui;
    private GameObject[] patrols;

    private void Start() {
        Director director = Director.GetInstance();
        director.fps = 60;
        director.currentSceneController = this;
        judger = new Judger();
        gui = gameObject.AddComponent<GameGUI>() as GameGUI;
        patrols = new GameObject[8];
        LoadResources();
    }

    public void LoadResources() {
        // load player
        Vector3 playInitPosition = new Vector3(0f, 0f, 0f);
        player = Loader.LoadObj("Prefabs/Player", playInitPosition);
        player.AddComponent<PlayerAction>();
        // load patrols
        int index = 0;
        for (int i = -10; i <= 10; i += 10) {
            for (int j = -10; j <= 10; j += 10) {
                if (!(i == 0 && j == 0)) {
                    patrols[index] = PatrolFactory.GetInstance().GetPatrol(i, j);
                    RandomWalk(patrols[index]);
                    index += 1;
                }
            }
        }
    }

    void OnEnable() {
        // patrol events
        PatrolEventManager.OnHitPlayer += PatrolHitPlayer;
        PatrolEventManager.OnHitObstacle += PatrolHitObstacle;
        PatrolEventManager.OnSeePlayer += PatrolSeePlayer;
        PatrolEventManager.OnLosePlayer += PatrolLosePlayer;
        PatrolEventManager.OnStop += PatrolStop;
        // player event
        PlayerEventManager.OnMove += PlayerMove;
    }

    void OnDisable() {
        // patrol events
        PatrolEventManager.OnHitPlayer -= PatrolHitPlayer;
        PatrolEventManager.OnHitObstacle -= PatrolHitObstacle;
        PatrolEventManager.OnSeePlayer -= PatrolSeePlayer;
        PatrolEventManager.OnLosePlayer -= PatrolLosePlayer;
        PatrolEventManager.OnStop -= PatrolStop;
        // player event
        PlayerEventManager.OnMove -= PlayerMove;
    }

    void PatrolHitPlayer(GameObject patrol) {
        // game over
        judger.GameOver();
        gui.SetState(judger);
    }

    void PatrolHitObstacle(GameObject patrol) {
        RandomWalk(patrol);
    }

    void PatrolSeePlayer(GameObject patrol) {
        PatrolAction action = patrol.GetComponent<PatrolAction>() as PatrolAction;
        if (action != null) {
            // follow player
            action.Target = player.transform.position;
        }
    }

    void PatrolLosePlayer(GameObject patrol) {
        RandomWalk(patrol);
        // update player's score
        judger.AddScore();
        gui.SetState(judger);
    }

    void PatrolStop(GameObject patrol) {
        RandomWalk(patrol);
    }

    void RandomWalk(GameObject patrol) {
        PatrolAction action = patrol.GetComponent<PatrolAction>() as PatrolAction;
        if (action != null) {
            // find a new random position
            Vector3 newPosition = patrol.transform.position + new Vector3(Random.Range(-2f, 2f), 0f, Random.Range(-2f, 2f));
            action.Target = newPosition;
        }
    }

    void PlayerMove(float verticalAxis, float horizontalAxis) {
        PlayerAction action = player.GetComponent<PlayerAction>() as PlayerAction;
        action.Move(verticalAxis, horizontalAxis);
    }

    public void Restart() {
        // reset patrols
        int index = 0;
        for (int i = -10; i <= 10; i += 10) {
            for (int j = -10; j <= 10; j += 10) {
                if (!(i == 0 && j == 0)) {
                    GameObject patrol = patrols[index];
                    patrol.transform.position = new Vector3(i, 0f, j);
                    RandomWalk(patrol);
                    index += 1;
                }
            }
        }
        // reset score and result
        judger.Restart();
        gui.Restart();
    }
}
