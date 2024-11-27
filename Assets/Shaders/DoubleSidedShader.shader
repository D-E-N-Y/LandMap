Shader "Custom/DoubleSidedTexture"
{
    Properties
    {
        _MainTex ("Base (RGB)", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        
        Pass
        {
            // Отключаем отбрасывание лиц для двустороннего рендеринга
            Cull Off
            // Настройка текстуры для рендеринга
            SetTexture[_MainTex] {
                combine primary
            }
        }
    }

    Fallback "Unlit/Texture"
}
