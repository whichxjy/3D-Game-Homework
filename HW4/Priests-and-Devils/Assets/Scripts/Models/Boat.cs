using UnityEngine;

public class Boat {
    // behind left shore or right shore
    public Location location { get; set; }
    // the roles on the boat
    public Role[] roles = new Role[2];
    // the moving speed
    public float speed = 1200f;

    // boat position
    public static readonly Vector3 leftBoatPosition = new Vector3(6f, 0.5f, 0f);
    public static readonly Vector3 rightBoatPosition = new Vector3(-6f, 0.5f, 0f);
    // seat position
    public static readonly Vector3[] leftSideSeatPosition = { new Vector3(5.5f, 1.5f, 0), new Vector3(6.5f, 1.5f, 0) };
    public static readonly Vector3[] rightSideSeatPosition = { new Vector3(-5.5f, 1.5f, 0), new Vector3(-6.5f, 1.5f, 0) };

    public int GetEmptyPositionIndex() {
        for (int i = 0; i < roles.Length; i++) {
            if (roles[i] == null) {
                return i;
            }
        }
        return -1;
    }

    public Vector3 GetPosition(int index) {
        if (location == Location.Left) {
            return leftSideSeatPosition[index];
        }
        else {
            return rightSideSeatPosition[index];
        }
    }

    public Vector3 GetEmptyPosition() {
        int index = GetEmptyPositionIndex();
        return GetPosition(index);
    }

    public Vector3 GetPosition() {
        if (location == Location.Left) {
            return leftBoatPosition;
        }
        else {
            return rightBoatPosition;
        }
    }

    public bool IsFull() {
        foreach (Role role in roles) {
            if (role == null) {
                return false;
            }
        }
        return true;
    }

    public bool IsEmpty() {
        foreach (Role role in roles) {
            if (role != null) {
                return false;
            }
        }
        return true;
    }
}
