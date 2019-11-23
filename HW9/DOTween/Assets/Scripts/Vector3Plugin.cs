using UnityEngine;

namespace MyDOTween {
    public class Vector3Plugin : TweenPlugin<Vector3, Vector3> {
        // set tweener's startValue, endValue, and changeValue
        public override void SetValues(Tweener<Vector3, Vector3> tweener) {
            tweener.startValue = tweener.getter();
            tweener.changeValue = tweener.endValue - tweener.startValue;
        }

        // evaluate and apply to tweener
        public override void EvaluateAndApply(
            Tweener<Vector3, Vector3> tweener, DOSetter<Vector3> setter,
            float elapsed, float duration,
            Vector3 startValue, Vector3 changeValue
        ) {
            // get ease value
            float easeVal = EaseManager.Evaluate(tweener.easeType, elapsed, duration);
            // set new value
            setter(startValue + changeValue * easeVal);
        }
    }
}