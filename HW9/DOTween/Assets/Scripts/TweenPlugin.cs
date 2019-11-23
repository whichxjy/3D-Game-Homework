namespace MyDOTween {
    public abstract class TweenPlugin<TweenVT, StoreVT> : ITweenPlugin {
        // set tweener's startValue and changeValue
        public abstract void SetValues(Tweener<TweenVT, StoreVT> tweener);

        // evaluate and apply to tweener
        public abstract void EvaluateAndApply(
            Tweener<TweenVT, StoreVT> tweener, DOSetter<TweenVT> setter,
            float elapsed, float duration,
            StoreVT startValue, StoreVT changeValue
        );
    }
}