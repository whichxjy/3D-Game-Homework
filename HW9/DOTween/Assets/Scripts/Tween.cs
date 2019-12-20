namespace MyDOTween {
    // Tweener or Sequence
    public abstract class Tween : Sequentiable {
        // active or not?
        public bool active { get; internal set; }
        // whether or not this tween is in sequence
        internal bool isSequenced = false;

        internal abstract void Update(float deltaTime);
    }
}