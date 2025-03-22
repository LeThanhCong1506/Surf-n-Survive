using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BlurSpriteRenderer : MonoBehaviour
{
    public Material[] blurMaterial; // Material Blur được gán trong Inspector
    private Material[] m_originalMaterials; // Lưu lại material gốc của từng RawImage
    private GameObject[] m_findMoveLeft;
    private GameObject[] m_findStand;

    public void ApplyBlur()
    {
        m_findMoveLeft = FindObjectsByType<MoveLeft>(FindObjectsSortMode.None).Select(ml => ml.gameObject).ToArray();
        m_findStand = GameObject.FindGameObjectsWithTag("Stand");

        int length = m_findMoveLeft.Length + m_findStand.Length;
        m_originalMaterials = new Material[length];

        int index = 0;
        StoreOriginalMaterials(ref index, m_findMoveLeft);
        StoreOriginalMaterials(ref index, m_findStand);

        ApplyBlurToObjects(m_findMoveLeft);
        ApplyBlurToObjects(m_findStand);
    }

    private void StoreOriginalMaterials(ref int index, GameObject[] objects)
    {
        if (objects == null) return;

        foreach (var obj in objects)
        {
            if (obj != null)
            {
                m_originalMaterials[index++] = obj.GetComponent<SpriteRenderer>().material;
            }
        }
    }

    private void ApplyBlurToObjects(GameObject[] objects)
    {
        if (objects == null || blurMaterial == null) return;

        foreach (var obj in objects)
        {
            if (obj == null) continue;

            var spriteRenderer = obj.GetComponent<SpriteRenderer>();
            if (spriteRenderer == null) continue;

            switch (obj.name)
            {
                case string name when name.Contains("Trash") || name.Contains("Speed"):
                    spriteRenderer.material = blurMaterial[2];
                    break;
                case string name when name.Contains("Seagull"):
                    spriteRenderer.material = blurMaterial[4];
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
                case "Beach":
                    StartCoroutine(ApplyBlurWithDelay(obj, 0.5f, blurMaterial[3], 0));
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
        int index = 0;
        RestoreOriginalMaterials(ref index, m_findMoveLeft);
        RestoreOriginalMaterials(ref index, m_findStand);
    }

    private void RestoreOriginalMaterials(ref int index, GameObject[] objects)
    {
        if (objects == null || m_originalMaterials == null) return;

        foreach (var obj in objects)
        {
            if (obj != null)
            {
                obj.GetComponent<SpriteRenderer>().material = m_originalMaterials[index++];
            }
        }
    }

    private IEnumerator ApplyBlurWithDelay(GameObject obj, float delay, Material material, int sortingOrder)
    {
        yield return new WaitForSeconds(delay);
        var spriteRenderer = obj.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.sortingOrder = sortingOrder;
            spriteRenderer.material = material;
        }
    }
}
