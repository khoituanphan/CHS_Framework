using UnityEngine;

public class ObjectToggle : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    QuizManager.Instance.ReceiveAnswer(gameObject.name);
                }
            }
        }
    }
}

