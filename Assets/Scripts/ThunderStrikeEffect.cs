using UnityEngine;

public class ThunderStrikeEffect : MonoBehaviour
{
    public float fallSpeed = 5f;
    private bool isStriking = false;

    private void Update()
    {
        if (isStriking)
        {
            transform.position += Vector3.down * (fallSpeed * Time.deltaTime);
        }
    }

    public void StartStrike()
    {
        isStriking = true;
    }
}
