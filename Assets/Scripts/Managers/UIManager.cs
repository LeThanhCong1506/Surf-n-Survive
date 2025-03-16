using UnityEngine;
using UnityEngine.UIElements;

public class UIManager
{
    private Label m_distanceLabel;

    public UIManager(UIDocument uiDoc)
    {
        m_distanceLabel = uiDoc.rootVisualElement.Q<Label>("Distance");
    }

    public void UpdateDistanceLabel(int distance)
    {
        m_distanceLabel.text = distance + " m";
    }
}
