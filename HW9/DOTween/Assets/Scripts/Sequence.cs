using System.Collections.Generic;

namespace MyDOTween {
    public class Sequence : Tween {
        // sequenced tweens
        internal readonly Queue<Tween> sequencedTweens = new Queue<Tween>();

        // constructor
        internal Sequence() {
            tweenType = TweenType.Sequence;
        }

        // insert tween to sequence
        internal static Sequence DoInsert(Sequence sequence, Tween tween) {
            TweenManager.AddActiveTweenToSequence(tween);
            tween.isSequenced = true;
            sequence.sequencedTweens.Enqueue(tween);
            return sequence;
        }

        internal override void Update(float deltaTime) {
            if (!active) {
                return;
            }
            if (sequencedTweens.Count > 0) {
                // get the first element in the sequenced tweens
                Tween currentTween = sequencedTweens.Peek();
                // update current tween
                currentTween.Update(deltaTime);
                // check if current tween finished
                if (!currentTween.active) {
                    sequencedTweens.Dequeue();
                }
            }
            // whether the sequence is active
            active = (sequencedTweens.Count > 0);
        }
    }
}