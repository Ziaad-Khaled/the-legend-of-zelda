using UnityEngine;

public class ShieldController : MonoBehaviour
{
    // Scroll main texture based on time

    public float scrollSpeed = 0.5f;
    Renderer rend;
    BossHealthController bossHealth;
    public string text = "I am shield";

    public GameObject shield;

    void Start()
    {
        shield = this.gameObject;
        rend = GetComponent<Renderer>();
        bossHealth = transform.parent.gameObject.GetComponent<BossHealthController>();
    }

    void Update()
    {


        
        // Animates main texture scale in a funky way!
       
            
            this.enabled = true;
            float scaleX = Mathf.Cos(Time.time / 10f) * scrollSpeed;
            float scaleY = Mathf.Sin(Time.time / 10f) * scrollSpeed;

            //rend.material.SetTextureScale("_MainTex", new Vector2(scaleX, scaleY));
            rend.material.mainTextureScale = new Vector2(scaleX, scaleY);
   
        
    }
    
}