using UnityEngine;
using System;

namespace MyDOTween {
    // TweenVT: type of value to tween
    // StoreVT: format in which value is stored while tweening
    public class Tweener<TweenVT, StoreVT> : Tween {
        // start or not
        internal bool start;

        // getter for tweener
        public DOGetter<TweenVT> getter = null;
        // setter for tweener
        public DOSetter<TweenVT> setter = null;
        // tween plugin (for current TweenVT and StoreVT)
        internal TweenPlugin<TweenVT, StoreVT> tweenPlugin;

        // start value
        public StoreVT startValue;
        // end value
        public StoreVT endValue;
        // the distance from start value to end value
        public StoreVT changeValue;

        // elapsed time
        public float elapsed;
        // duration time
        public float duration;

        // ease type
        internal Ease easeType;

        // constructor
        internal Tweener() {
            tweenType = TweenType.Tweener;
        }

        internal bool Setup(
            DOGetter<TweenVT> getter, DOSetter<TweenVT> setter,
            StoreVT endValue, float duration
        ) {
            // not start yet
            this.start = false;
            // set getter & setter
            this.getter = getter;
            this.setter = setter;
            // set tween plugin
            if (tweenPlugin == null) {
                tweenPlugin = PluginsManager.GetDefaultPlugin<TweenVT, StoreVT>();
            }
            // set end value
            this.endValue = endValue;
            // set time
            this.elapsed = 0;
            this.duration = duration;
            // set ease type
            this.easeType = DOTween.defaultEaseType;
            return true;
        }

        internal override void Update(float deltaTime) {
            if (!active) {
                return;
            }
            if (!start) {
                // set start value and change value dynamically
                tweenPlugin.SetValues(this);
                start = true;
            }
            // update elapsed time
            elapsed = Math.Min(elapsed + deltaTime, duration);
            if (elapsed == duration) {
                active = false;
            }
            // set new value
            tweenPlugin.EvaluateAndApply(this, setter, elapsed, duration, startValue, changeValue);
        }
    }
}