using UnityEngine;

// DİKKAT: Dosya adını ModelController.cs yaptığın için, class adının da aynı olması gerekir.
public class ModelController : MonoBehaviour
{
    public float lastUpdateTime;
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    /// <summary>
    /// Updates the marker’s transform, and records the update time.
    /// Metin ve ölçek ile ilgili kısımlar çıkarıldı.
    /// </summary>
    // ÖNEMLİ NOT: Diğer script'ler bu fonksiyonu bu şekilde çağırdığı için
    // fonksiyonun imzasını (parametrelerini) şimdilik değiştirmemek en kolayı.
    public void UpdateMarker(Vector3 position, Quaternion rotation, Vector3 scale, string text)
    {
        transform.SetPositionAndRotation(position, rotation);
        transform.localScale = scale; // Scale (boyut) güncellemesi kalsın, işe yarayabilir.

        lastUpdateTime = Time.time;
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        // Metinle ilgili olan ve kameraya döndürme kodu çıkarıldı.
        // Otonom olarak kendini gizleme mekanizması ise hala çalışıyor.
        if (gameObject.activeSelf && Time.time - lastUpdateTime > 2f)
        {
            gameObject.SetActive(false);
        }
    }
}