using TMPro;
using UnityEngine;

/// <summary>
/// Manages the UI elements of the game, including updating distance labels,
/// handling UI visibility during pause and game over states, and managing best scores.
/// </summary>
public class UIManager : MonoBehaviour
{
    private TextMeshProUGUI m_distanceLabel;
    private TextMeshProUGUI m_currentDistanceLabel;
    private TextMeshProUGUI m_bestDistanceLabel;

    public UIManager(TextMeshProUGUI dUi, TextMeshProUGUI cdUi, TextMeshProUGUI bdUi)
    {
        m_distanceLabel = dUi;
        m_currentDistanceLabel = cdUi;
        m_bestDistanceLabel = bdUi;
    }

    public void UpdateDistanceLabel(int distance)
    {
        m_distanceLabel.text = distance + " m";
    }

    public void UpdateDistanceWhenPauseAndGameOver()
    {
        m_distanceLabel.text = "";
    }

    public void UpdateCurrentDistanceLabel(string text)
    {
        m_currentDistanceLabel.text = text;
    }

    public void UpdateBestDistanceLabel(string text)
    {
        m_bestDistanceLabel.text = text;
    }
}
