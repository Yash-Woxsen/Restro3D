using UnityEngine;

public class Toggler : MonoBehaviour
{
    public void ToggleActiveInHierarchyOnCLick(GameObject gameobjectToToggle)
    {
        gameobjectToToggle.SetActive(!gameobjectToToggle.activeInHierarchy);
    }
}
