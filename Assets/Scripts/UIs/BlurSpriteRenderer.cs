using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BlurSpriteRenderer : MonoBehaviour
{
    public Material[] blurMaterial; // Material Blur được gán trong Inspector
    private Dictionary<GameObject, Material> m_originalMaterials; // Lưu lại material gốc theo GameObject
    private GameObject[] m_findMoveLeft;
    private GameObject[] m_findStand;
    private GameObject m_tsunami; // Thêm tham chiếu trực tiếp đến Tsunami
    private GameObject m_beachOpacity; // Thêm tham chiếu trực tiếp đến Beach Opacity

    private void Awake()
    {
        // Khởi tạo Dictionary để lưu trữ materials theo GameObject
        m_originalMaterials = new Dictionary<GameObject, Material>();
    }

    public void ApplyBlur()
    {
        // Tìm tất cả các đối tượng cần áp dụng blur
        m_findMoveLeft = FindObjectsByType<MoveLeft>(FindObjectsSortMode.None).Select(ml => ml.gameObject).ToArray();
        m_findStand = GameObject.FindGameObjectsWithTag("Stand");

        // Tìm trực tiếp Tsunami và Beach Opacity bằng tag hoặc name
        m_tsunami = GameObject.Find("Tsunami");
        m_beachOpacity = GameObject.Find("Beach Opacity");

        // Lưu trữ materials gốc
        StoreOriginalMaterials(m_findMoveLeft);
        StoreOriginalMaterials(m_findStand);

        // Lưu trữ materials cho Tsunami và Beach Opacity riêng biệt
        if (m_tsunami != null)
        {
            var tsunamiRenderer = m_tsunami.GetComponent<SpriteRenderer>();
            if (tsunamiRenderer != null)
            {
                m_originalMaterials[m_tsunami] = tsunamiRenderer.material;
            }
        }

        if (m_beachOpacity != null)
        {
            var beachOpacityRenderer = m_beachOpacity.GetComponent<SpriteRenderer>();
            if (beachOpacityRenderer != null)
            {
                m_originalMaterials[m_beachOpacity] = beachOpacityRenderer.material;
            }
        }

        // Áp dụng blur
        ApplyBlurToObjects(m_findMoveLeft);
        ApplyBlurToObjects(m_findStand);

        // Áp dụng blur cho Tsunami và Beach Opacity riêng biệt
        ApplyBlurToSpecificObject(m_tsunami, 7, 0);
        ApplyBlurToSpecificObject(m_beachOpacity, 10, 0);
    }

    private void StoreOriginalMaterials(GameObject[] objects)
    {
        if (objects == null) return;

        foreach (var obj in objects)
        {
            if (obj != null)
            {
                var spriteRenderer = obj.GetComponent<SpriteRenderer>();
                if (spriteRenderer != null && !m_originalMaterials.ContainsKey(obj))
                {
                    m_originalMaterials[obj] = spriteRenderer.material;
                }
            }
        }
    }

    private void ApplyBlurToSpecificObject(GameObject obj, int sortingOrder, int materialIndex)
    {
        if (obj == null || blurMaterial == null || materialIndex >= blurMaterial.Length) return;

        var spriteRenderer = obj.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.sortingOrder = sortingOrder;
            spriteRenderer.material = blurMaterial[materialIndex];
        }
    }

    private void ApplyBlurToObjects(GameObject[] objects)
    {
        if (objects == null || blurMaterial == null) return;

        foreach (var obj in objects)
        {
            if (obj == null) continue;

            // Bỏ qua Tsunami và Beach Opacity vì chúng được xử lý riêng
            if (obj.name == "Tsunami" || obj.name == "Beach Opacity") continue;

            var spriteRenderer = obj.GetComponent<SpriteRenderer>();
            if (spriteRenderer == null) continue;

            switch (obj.name)
            {
                case string name when name.Contains("Trash") || name.Contains("Speed"):
                    spriteRenderer.material = blurMaterial[2];
                    break;
                case string name when name.Contains("Seagull"):
                    spriteRenderer.material = blurMaterial[5];
                    break;
                case string name when name.Contains("Mountain"):
                    spriteRenderer.material = blurMaterial[4];
                    spriteRenderer.sortingOrder = -3;
                    break;
                case string name when name.Contains("Ground"):
                    spriteRenderer.sortingOrder = -2;
                    spriteRenderer.material = blurMaterial[3];
                    break;
                case "Beach Inside":
                    spriteRenderer.sortingOrder = -1;
                    spriteRenderer.material = blurMaterial[3];
                    break;
                case "Player":
                    spriteRenderer.material = blurMaterial[1];
                    break;
                default:
                    spriteRenderer.material = blurMaterial[0];
                    break;
            }
        }
    }

    public void RemoveBlur()
    {
        // Khôi phục materials gốc cho tất cả đối tượng đã lưu
        foreach (var kvp in m_originalMaterials)
        {
            GameObject obj = kvp.Key;
            Material originalMaterial = kvp.Value;

            if (obj != null)
            {
                var spriteRenderer = obj.GetComponent<SpriteRenderer>();
                if (spriteRenderer != null)
                {
                    spriteRenderer.material = originalMaterial;
                }
            }
        }

        // Xóa dictionary sau khi khôi phục
        m_originalMaterials.Clear();
    }
}
