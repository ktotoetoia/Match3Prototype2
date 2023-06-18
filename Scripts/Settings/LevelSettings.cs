using UnityEngine;

[CreateAssetMenu(fileName = "LevelSettings", menuName = "Settings/LevelSettings")]
public class LevelSettings : ScriptableObject
{
    [SerializeField] private int columns = 6;
    [SerializeField] private int rows = 8;

    private GraphInfo graphInfo;

    public IGraphInfo GraphInfo
    {
        get
        {
            if(graphInfo == null)
            {
                graphInfo = new GraphInfo(columns,rows);
            }

            return graphInfo;
        }
    }
}