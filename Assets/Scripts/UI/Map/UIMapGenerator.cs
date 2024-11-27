using UnityEngine;

public class UIMapGenerator : MonoBehaviour
{
    private float[,] noiseMap;
    private int mapWidth;
    private int mapHeight;
    private TerrainType[] regions;

    public UIMapGenerator(float[,] noiseMap, int mapWidth, int mapHeight, int slices)
    {
        this.noiseMap = noiseMap;

        this.mapWidth = mapWidth;
        this.mapHeight = mapHeight;

        regions = new TerrainType[slices];
        for(int i = 0; i < slices; i++)
        {
            regions[i] = new TerrainType();

            regions[i].name = $"{i}";
            regions[i].height = (float)(i + 1) / slices;
            
            float t = (float)i / (slices - 1); // Нормализованное значение от 0 до 1
            regions[i].colour = Color.Lerp(new Color(0.91f, 0.96f, 0.91f), // Светло-зеленый (RGB: #E8F5E9)
                                           new Color(0.11f, 0.37f, 0.13f), // Темно-зеленый (RGB: #1B5E20)
                                           t);
        }
    }

    public Color[] FillTopologyMap()
    {
        Color[] colourMap = new Color[mapWidth * mapHeight];
        for(int y = 0; y < mapHeight; y++)
        {
            for(int x = 0; x < mapWidth; x++)
            {
                float currentHeight = noiseMap[x, y];

                for(int i = 0; i < regions.Length; i++)
                {
                    if(currentHeight <= regions[i].height)
                    {
                        colourMap[y * mapWidth + x] = regions[i].colour;
                        break;
                    }
                }
            }
        }

        return colourMap;
    }

    int GetRegionIndex(float height)
    {
        for (int i = 0; i < regions.Length; i++)
        {
            if (height <= regions[i].height)
            {
                return i;
            }
        }
        return -1;
    }

    public Color[] LineTopologyMap()
    {
        Color[] colourMap = new Color[mapWidth * mapHeight];

        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                float currentHeight = noiseMap[x, y];
                int currentRegionIndex = GetRegionIndex(currentHeight);

                bool isBoundary = false;

                for (int offsetY = -1; offsetY <= 1; offsetY++)
                {
                    for (int offsetX = -1; offsetX <= 1; offsetX++)
                    {
                        if (offsetX == 0 && offsetY == 0)
                            continue;

                        int neighborX = x + offsetX;
                        int neighborY = y + offsetY;

                        if (neighborX >= 0 && neighborX < mapWidth && neighborY >= 0 && neighborY < mapHeight)
                        {
                            float neighborHeight = noiseMap[neighborX, neighborY];
                            int neighborRegionIndex = GetRegionIndex(neighborHeight);

                            if (neighborRegionIndex != currentRegionIndex && neighborRegionIndex != -1)
                            {
                                isBoundary = true;
                                break;
                            }
                        }
                    }

                    if (isBoundary) break;
                }

                if (isBoundary)
                {
                    colourMap[y * mapWidth + x] = regions[currentRegionIndex].colour;
                }
                else
                {
                    colourMap[y * mapWidth + x] = Color.clear;
                }
            }
        }

        return colourMap;
    }
}