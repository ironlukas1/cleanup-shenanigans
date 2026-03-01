using UnityEngine;
using UnityEngine.UI;

public class MopCleaner : MonoBehaviour
{
    //Mop
    public float reachDistance = 5f;
    public float cleanTime = 2f;
    public bool debugRay = true;

    //HUD
    public Image progressCircle;

    private float cleanProgress = 0f;
    private GameObject currentDirt = null;

    void Start()
    {
        if (progressCircle == null)
            progressCircle = GameObject.Find("CleaningProgressCircle").GetComponent<Image>();
    }

    void Update()
    {
        if (!Input.GetMouseButton(0))
        {
            ResetCleaning();
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));
        RaycastHit hit;

        if (debugRay)
            Debug.DrawRay(ray.origin, ray.direction * reachDistance, Color.red);

        int layerMask = LayerMask.GetMask("Dirt");

        if (Physics.Raycast(ray, out hit, reachDistance, layerMask))
        {
            if (currentDirt != hit.collider.gameObject)
            {
                currentDirt = hit.collider.gameObject;
                cleanProgress = 0f;
            }

            cleanProgress += Time.deltaTime;

            if (progressCircle)
                progressCircle.fillAmount = cleanProgress / cleanTime;

            if (cleanProgress >= cleanTime)
            {
                Destroy(currentDirt.transform.root.gameObject);
                ResetCleaning();
            }
        }
        else
        {
            ResetCleaning();
        }
    }

    void ResetCleaning()
    {
        cleanProgress = 0f;
        currentDirt = null;

        if (progressCircle)
            progressCircle.fillAmount = 0f;
    }
}