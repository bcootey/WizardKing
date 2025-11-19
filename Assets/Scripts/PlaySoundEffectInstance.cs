using UnityEngine;

public class PlaySoundEffectInstance : MonoBehaviour
{
    public GameObject audioPrefab; 

    public void Start()
    {

        GameObject obj = Instantiate(audioPrefab, transform.position, Quaternion.identity);

        AudioSource src = obj.GetComponent<AudioSource>();
        if (src != null && src.clip != null)
        {
            src.Play();
            Destroy(obj, src.clip.length / Mathf.Abs(src.pitch));
        }
        else
        {

            Destroy(obj);
        }
    }
}
