using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UI_RandomEdgeParticles : MonoBehaviour
{
    [System.Serializable]
    public class Particle
    {
        public RectTransform rect;
        public Vector2 velocity;
        public float lifetime;
        public float timeAlive;
        public Image image;
        public float startAlpha;
    }

    public Sprite particleSprite;
    public int maxParticles = 50;
    public Vector2 sizeRange = new Vector2(10, 40);
    public float lifetimeMin = 3f;
    public float lifetimeMax = 7f;
    public float speedMin = 50f;
    public float speedMax = 150f;
    public Color particleColor = new Color(1, 1, 1, 0.8f);

    private List<Particle> particles = new List<Particle>();
    private RectTransform canvasRect;

    void Start()
    {
        canvasRect = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
        for (int i = 0; i < maxParticles; i++) SpawnParticle();
    }

    void SpawnParticle()
    {
        GameObject go = new GameObject("Particle", typeof(Image));
        go.transform.SetParent(transform, false);
        Image img = go.GetComponent<Image>();
        img.sprite = particleSprite;
        img.color = particleColor;

        RectTransform rect = go.GetComponent<RectTransform>();

        int side = Random.Range(0, 4);
        Vector2 startPos = Vector2.zero;
        Vector2 targetDir = Vector2.zero;

        float halfWidth = canvasRect.rect.width / 2;
        float halfHeight = canvasRect.rect.height / 2;

        switch (side)
        {
            case 0: startPos = new Vector2(-halfWidth - 20, Random.Range(-halfHeight, halfHeight)); targetDir = Vector2.right; break;
            case 1: startPos = new Vector2(halfWidth + 20, Random.Range(-halfHeight, halfHeight)); targetDir = Vector2.left; break;
            case 2: startPos = new Vector2(Random.Range(-halfWidth, halfWidth), halfHeight + 20); targetDir = Vector2.down; break;
            case 3: startPos = new Vector2(Random.Range(-halfWidth, halfWidth), -halfHeight - 20); targetDir = Vector2.up; break;
        }

        Vector2 centerDir = (Vector2.zero - startPos).normalized;
        targetDir = Vector2.Lerp(targetDir, centerDir, 0.8f);

        rect.anchoredPosition = startPos;
        float size = Random.Range(sizeRange.x, sizeRange.y);
        rect.sizeDelta = new Vector2(size, size);

        Particle p = new Particle();
        p.rect = rect;
        p.image = img;
        p.velocity = targetDir * Random.Range(speedMin, speedMax);
        p.lifetime = Random.Range(lifetimeMin, lifetimeMax);
        p.timeAlive = 0;
        p.startAlpha = particleColor.a;

        particles.Add(p);
    }

    void Update()
    {
        for (int i = particles.Count - 1; i >= 0; i--)
        {
            var p = particles[i];
            p.timeAlive += Time.deltaTime;

            if (p.timeAlive >= p.lifetime)
            {
                Destroy(p.rect.gameObject);
                particles.RemoveAt(i);
                SpawnParticle();
            }
            else
            {
                p.rect.anchoredPosition += p.velocity * Time.deltaTime;
                float alpha = Mathf.Lerp(p.startAlpha, 0, p.timeAlive / p.lifetime);
                Color c = p.image.color;
                c.a = alpha;
                p.image.color = c;
            }
        }
    }
}
