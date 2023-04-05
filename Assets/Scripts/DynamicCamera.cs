using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicCamera : MonoBehaviour
{
    [SerializeField]
    float rangeInner = 20;
    [SerializeField]
    float rangeMiddle = 25;
    [SerializeField]
    PlayerController player;
    [SerializeField]
    LayerMask furnitureMask;

    float cacheY;
    // Start is called before the first frame update
    void Start()
    {
        cacheY = transform.parent.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        // Check for things in range
        Collider[] cols = Physics.OverlapSphere(player.transform.position, rangeInner, furnitureMask);
        Vector3 targetCamPos = Vector3.zero;
        if (cols.Length == 0)
        {
            if (player.MovementVector.magnitude < 0.05f)
            {
                targetCamPos = player.transform.position;
            }
            if (player.MovementVector.magnitude > 0.01f)
            {

            }

        }

        Debug.Log(cols.Length);
        targetCamPos = new Vector3(targetCamPos.x, cacheY, targetCamPos.z);
    }

	private void OnDrawGizmos()
	{
        Gizmos.color = new Color(0.7f, 0.7f, 0f, 0.2f);

		Gizmos.DrawSphere(player.transform.position, rangeInner);
		Gizmos.DrawSphere(player.transform.position, rangeMiddle);
	}
}
