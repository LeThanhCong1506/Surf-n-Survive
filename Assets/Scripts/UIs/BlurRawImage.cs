using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages applying and removing blur effects on UI RawImages.
/// Allows dynamic switching between original and blurred materials for specified images.
/// </summary>
public class BlurRawImages : MonoBehaviour
{
    public GameObject[] Images;
    public Material[] BlurMaterial;
    private Material[] m_originalMaterials;

    void Start()
    {
        if (Images != null && Images.Length > 0)
        {
            m_originalMaterials = new Material[Images.Length];
            for (int i = 0; i < Images.Length; i++)
            {
                if (Images[i] != null)
                {
                    m_originalMaterials[i] = Images[i].GetComponent<Image>().material;
                }
            }
        }
    }

    public void ApplyBlur()
    {
        if (Images != null && BlurMaterial != null)
        {
            for (int i = 0; i < Images.Length; i++)
            {
                if (Images[i] != null)
                {
                    if (i == 0)
                        Images[i].GetComponent<Image>().material = BlurMaterial[0];

                    else if (i == 1)
                        Images[i].GetComponent<Image>().material = BlurMaterial[3];

                    else if (i == 2 || i == 3 || i == 4)
                        Images[i].GetComponent<Image>().material = BlurMaterial[1];

                    else
                        Images[i].GetComponent<Image>().material = BlurMaterial[2];
                }
            }
        }
    }

    public void RemoveBlur()
    {
        if (Images != null && m_originalMaterials != null)
        {
            for (int i = 0; i < Images.Length; i++)
            {
                if (Images[i] != null)
                {
                    Images[i].GetComponent<Image>().material = m_originalMaterials[i];
                }
            }
        }
    }


}
