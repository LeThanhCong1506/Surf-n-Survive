using UnityEngine;
using UnityEngine.UI;

public class BlurRawImages : MonoBehaviour
{
    public GameObject[] images;      // Gán trong Inspector cho danh sách RawImage cần áp dụng hiệu ứng blur
    public Material[] blurMaterial;     // Material Blur được gán trong Inspector
    private Material[] originalMaterials; // Lưu lại material gốc của từng RawImage

    void Start()
    {
        if (images != null && images.Length > 0)
        {
            originalMaterials = new Material[images.Length];
            for (int i = 0; i < images.Length; i++)
            {
                if (images[i] != null)
                {
                    // Lưu lại material gốc của từng RawImage
                    originalMaterials[i] = images[i].GetComponent<Image>().material;
                }
            }
        }
    }

    public void ApplyBlur()
    {
        if (images != null && blurMaterial != null)
        {
            for (int i = 0; i < images.Length; i++)
            {
                if (images[i] != null)
                {
                    if (i == 0)
                        images[i].GetComponent<Image>().material = blurMaterial[0];

                    else if (i == 1)
                        images[i].GetComponent<Image>().material = blurMaterial[3];

                    else if (i == 2 || i == 3 || i == 4)
                        images[i].GetComponent<Image>().material = blurMaterial[1];

                    else
                        images[i].GetComponent<Image>().material = blurMaterial[2];
                }
            }
        }
    }

    public void RemoveBlur()
    {
        if (images != null && originalMaterials != null)
        {
            for (int i = 0; i < images.Length; i++)
            {
                if (images[i] != null)
                {
                    // Khôi phục lại material gốc của từng RawImage
                    images[i].GetComponent<Image>().material = originalMaterials[i];
                }
            }
        }
    }


}
