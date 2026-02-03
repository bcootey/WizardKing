using UnityEngine;

public class DashTrail3D : MonoBehaviour
{
    [Header("Source Skinned Meshes")]
    public SkinnedMeshRenderer[] skinnedMeshes;

    [Header("Ghost Settings")]
    public Material ghostMaterial;
    public float ghostSpawnInterval = 0.03f;
    public float ghostLifetime = 0.3f;
    public float ghostStartAlpha = 0.7f;

    [Header("Manual Fix")]
    public float ghostScaleMultiplier = 1f;   // adjust this until ghost matches

    private bool isDashing;
    private float ghostTimer;

    void Update()
    {
        if (!isDashing) return;

        ghostTimer += Time.deltaTime;

        if (ghostTimer >= ghostSpawnInterval)
        {
            ghostTimer = 0f;
            SpawnGhost();
        }
    }

    private void SpawnGhost()
    {
        GameObject root = new GameObject("DashGhostRoot");

        foreach (var smr in skinnedMeshes)
        {
            if (smr == null) continue;

            //bake mesh in local space
            Mesh baked = new Mesh();
            smr.BakeMesh(baked);
            
            GameObject child = new GameObject(smr.name + "_Ghost");
            child.transform.SetParent(root.transform);
            
            child.transform.position = smr.transform.position;
            child.transform.rotation = smr.transform.rotation;
            
            child.transform.localScale = smr.transform.lossyScale * ghostScaleMultiplier;
            
            var mf = child.AddComponent<MeshFilter>();
            mf.mesh = baked;

            var mr = child.AddComponent<MeshRenderer>();
            mr.material = new Material(ghostMaterial);
        }

        var ghostComponent = root.AddComponent<DashGhost3D>();
        ghostComponent.lifetime = ghostLifetime;
        ghostComponent.startAlpha = ghostStartAlpha;
    }

    public void StartDashTrail()
    {
        isDashing = true;
        ghostTimer = 0f;
    }

    public void StopDashTrail()
    {
        isDashing = false;
    }
}
