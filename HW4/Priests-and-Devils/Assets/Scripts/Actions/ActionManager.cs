using UnityEngine;
using System.Collections.Generic;

public class ActionManager : MonoBehaviour, IActionCallback {

    private Dictionary<int, Action> actions = new Dictionary<int, Action>();
    private List<Action> waitingToAdd = new List<Action>();
    private List<int> watingToDelete = new List<int>();

    private void Update() {
        // add actions
        foreach (Action action in waitingToAdd) {
            actions[action.GetInstanceID()] = action;
        }
        waitingToAdd.Clear();

        // execute all active actions
        foreach (KeyValuePair<int, Action> kv in actions) {
            Action action = kv.Value;
            if (action.destroy) {
                watingToDelete.Add(action.GetInstanceID());
            }
            else if (action.enable) {
                action.Update();
            }
        }

        // delete actions
        foreach (int key in watingToDelete) {
            Action action = actions[key];
            actions.Remove(key);
            Destroy(action);
        }
        watingToDelete.Clear();
    }

    public void AddAction(Action action) {
        waitingToAdd.Add(action);
        action.Start();
    }

    public void ActionDone(Action action) {
        // empty
    }
}
