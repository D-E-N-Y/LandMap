using UnityEngine;
using UnityEngine.UI;

public class UIMap : MonoBehaviour
{
    [SerializeField] private Image ui_map;

    private UIMapGenerator uiMapGenerator;
    private int mapWidth;
    private int mapHeight;

    private TopologyMode topologyMode = TopologyMode.Line;

    public void Initialize(float[,] noiseMap, int mapWidth, int mapHeight, int slices)
    {
        this.mapWidth = mapWidth;
        this.mapHeight = mapHeight;
        
        uiMapGenerator = new UIMapGenerator(noiseMap, mapWidth, mapHeight, slices);

        ui_map.GetComponent<RectTransform>().sizeDelta = new Vector2(mapWidth, mapHeight);

        DrawMap();
    }

    private void DrawMap()
    {
        switch(topologyMode)
        {
            case TopologyMode.Fill:
                ui_map.material.mainTexture = TextureGenerator.TextureFromColourMap(uiMapGenerator.FillTopologyMap(), mapWidth, mapHeight);
                ui_map.SetMaterialDirty();
                break;
            
            case TopologyMode.Line:
                ui_map.material.mainTexture = TextureGenerator.TextureFromColourMap(uiMapGenerator.LineTopologyMap(), mapWidth, mapHeight);
                ui_map.SetMaterialDirty();
                break;
        }
    }

    public void SetFillTopologyMode()
    {
        topologyMode = TopologyMode.Fill;

        DrawMap();
    }

    public void SetLineTopologyMode()
    {
        topologyMode = TopologyMode.Line;

        DrawMap();
    }
        
}