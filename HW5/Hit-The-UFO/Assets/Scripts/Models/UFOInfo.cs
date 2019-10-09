using UnityEngine;

public enum UFOColor {
    Red,
    Green,
    Blue
}

public class UFOInfo {
    public float size { get; set; }
    public float speed { get; set; }
    public int score { get; set; }
    public UFOColor color { get; set; }
}
