using UnityEngine;

public enum RoleType {
    Priest,
    Devil
}

public class Role {
    // the type of role
    public RoleType type { get; set; }
    // index of this role (0, 1 or 2)
    public int index { get; set; }
    // left shore or right shore
    public Location location { get; set; }
    // the role is on boat or not
    public bool isOnBoat { get; set; }
    // the moving speed
    public float speed = 30f;
}
