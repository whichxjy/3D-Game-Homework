using UnityEngine;

public class Shore {
    // left shore or right shore
    public Location location { get; set; }
    // the roles in the shore
    public Role[] roles = new Role[6];

    public static readonly Vector3 leftShorePosition = new Vector3(10f, 1f, 0f);
    public static readonly Vector3 rightShorePosition = new Vector3(-10f, 1f, 0f);
    public static readonly Vector3[] seatPosition = {
        new Vector3(8f, 2.5f, 0),
        new Vector3(9f, 2.5f, 0),
        new Vector3(10f, 2.5f, 0),
        new Vector3(11f, 2.5f, 0),
        new Vector3(12f, 2.5f, 0),
        new Vector3(13f, 2.5f, 0)
    };

    public int GetEmptyPositionIndex() {
        for (int i = 0; i < roles.Length; i++) {
            if (roles[i] == null) {
                return i;
            }
        }
        return -1;
    }

    public Vector3 GetPosition(int index) {
        Vector3 position = seatPosition[index];
        if (location == Location.Right) {
            position.x *= -1;
        }
        return position;
    }
    
    public Vector3 GetEmptyPosition() {
        int index = GetEmptyPositionIndex();
        return GetPosition(index);
    }
}