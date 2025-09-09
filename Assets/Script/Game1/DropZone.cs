using UnityEngine;

public class DropZone : MonoBehaviour
{
    private bool occupied = false;

    public bool IsOccupied()
    {
        return occupied;
    }

    public void SetOccupied(bool value)
    {
        occupied = value;
        if (value)
            DropZoneManager.Instance.CheckAllZonesFilled();
    }
}
