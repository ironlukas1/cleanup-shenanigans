using UnityEngine;

public class PuddleSpawner : MonoBehaviour
{
    [Header("Spawn Area (world-space bounds)")]
    [SerializeField] private Vector3 areaMin = new Vector3(-10f, 0f, -10f);
    [SerializeField] private Vector3 areaMax = new Vector3(10f, 0f, 10f);
    [SerializeField] private float spawnY = 0.08f;

    [Header("Wave Settings")]
    [SerializeField] private int minPerWave = 3;
    [SerializeField] private int maxPerWave = 6; // exclusive upper bound, so 3-5
    [SerializeField] private float waveCooldown = 45f;

    [Header("Puddle Layer")]
    [SerializeField] private LayerMask puddleLayer;

    private float timer;

    private void Start()
    {
        SpawnWave();
        timer = waveCooldown;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            SpawnWave();
            timer = waveCooldown;
        }
    }

    private void SpawnWave()
    {
        int count = Random.Range(minPerWave, maxPerWave);
        for (int i = 0; i < count; i++)
        {
            SpawnPuddle(PickType());
        }
    }

    private PuddleType PickType()
    {
        // Weighted: dirt 50%, water 30%, dust 20%
        float roll = Random.value;
        if (roll < 0.5f) return PuddleType.Dirt;
        if (roll < 0.8f) return PuddleType.Water;
        return PuddleType.Dust;
    }

    private void SpawnPuddle(PuddleType type)
    {
        float x = Random.Range(areaMin.x, areaMax.x);
        float z = Random.Range(areaMin.z, areaMax.z);
        Vector3 pos = new Vector3(x, spawnY, z);

        GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        obj.name = "Puddle_" + type;
        obj.transform.position = pos;

        // Assign to puddle layer (use first set bit in the layermask)
        int layer = LayerMaskToLayer(puddleLayer);
        if (layer >= 0)
            obj.layer = layer;

        Puddle puddle = obj.AddComponent<Puddle>();
        puddle.type = type;
    }

    private static int LayerMaskToLayer(LayerMask mask)
    {
        int val = mask.value;
        for (int i = 0; i < 32; i++)
        {
            if ((val & (1 << i)) != 0)
                return i;
        }
        return -1;
    }
}
