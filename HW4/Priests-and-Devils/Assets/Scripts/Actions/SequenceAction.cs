using UnityEngine;
using System.Collections.Generic;

public class SequenceAction : Action, IActionCallback {
    // a sequence of actions to be done
    public List<Action> sequence;
    // times to repeat
    public int repeat = 1;
    // index of current action
    public int currentActionIndex = 0;

    public static SequenceAction GetSeqAction(IActionCallback callback, List<Action> sequence, int repeat = 1, int currentActionIndex = 0) {
        SequenceAction action = ScriptableObject.CreateInstance<SequenceAction>();
        action.callback = callback;
        action.sequence = sequence;
        action.repeat = repeat;
        action.currentActionIndex = currentActionIndex;
        return action;
    }

    public override void Start() {
        foreach (Action action in sequence) {
            action.callback = this;
            action.Start();
        }
    }

    public override void Update() {     
        if (sequence.Count == 0) {
            return;
        }
        if (currentActionIndex < sequence.Count) {
            sequence[currentActionIndex].Update();
        }
    }

    public void ActionDone(Action action) {
        action.destroy = false;
        currentActionIndex += 1;
        if (currentActionIndex >= sequence.Count) {
            currentActionIndex = 0;
            if (repeat > 0) {
                repeat -= 1;
            }
            if (repeat == 0) {
                destroy = true;
                callback.ActionDone(this);
            }
        }
    }

    public void OnDestroy() {
        foreach (Action action in sequence) {
            Destroy(action);
        }
    }
}
