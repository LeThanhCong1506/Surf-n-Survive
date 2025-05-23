using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Manages applying and removing blur effects on SpriteRenderers in the scene.
/// Dynamically adjusts materials based on object names and tags to create a blur effect.
/// </summary>
public class BlurSpriteRenderer : MonoBehaviour
{
    public Material[] BlurMaterial;
    private Material[] m_originalMaterials;
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
                var spriteRenderer = obj.GetComponent<SpriteRenderer>();
                if (spriteRenderer != null)
                {
                    m_originalMaterials[index++] = spriteRenderer.material;
                }
            }
        }
    }

    private void ApplyBlurToObjects(GameObject[] objects)
    {
        if (objects == null || BlurMaterial == null) return;

        foreach (var obj in objects)
        {
            if (obj == null) continue;

            var spriteRenderer = obj.GetComponent<SpriteRenderer>();
            if (spriteRenderer == null) continue;

            switch (obj.name)
            {
                case string name when name.Contains("Trash") || name.Contains("Speed"):
                    spriteRenderer.material = BlurMaterial[2];
                    break;
                case string name when name.Contains("Seagull"):
                    spriteRenderer.material = BlurMaterial[4];
                    break;
                case string name when name.Contains("Mountain"):
                    spriteRenderer.material = BlurMaterial[4];
                    spriteRenderer.sortingOrder = -3;
                    break;
                case string name when name.Contains("Ground"):
                    spriteRenderer.sortingOrder = -2;
                    spriteRenderer.material = BlurMaterial[3];
                    break;
                case "Beach Opacity":
                    spriteRenderer.material = BlurMaterial[5];
                    break;
                case "Beach Inside":
                    spriteRenderer.sortingOrder = -1;
                    spriteRenderer.material = BlurMaterial[3];
                    break;
                case "Player":
                    spriteRenderer.material = BlurMaterial[1];
                    break;
                default:
                    spriteRenderer.material = BlurMaterial[0];
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
                var spriteRenderer = obj.GetComponent<SpriteRenderer>();
                if (spriteRenderer != null && index < m_originalMaterials.Length)
                {
                    obj.GetComponent<SpriteRenderer>().material = m_originalMaterials[index++];
                }
            }
        }
    }
}