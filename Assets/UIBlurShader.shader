/// <summary>
/// A custom shader designed to support background blurring when integrated into UI elements.
/// Provides a Gaussian blur effect with adjustable intensity.
/// </summary>
Shader "Unlit/UIBlurShader"
{
    Properties
    {
        _MainTex("Base (RGB)", 2D) = "white" {}
        _BlurSize("Blur Size", Float) = 1.0
    }

    SubShader
    {
        Tags { "Queue" = "Overlay" }
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        Cull Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            sampler2D _MainTex;
            float _BlurSize;

            v2f vert(appdata_t v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            // Gaussian kernel 5x5:
            // [0.003  0.013  0.022  0.013  0.003]
            // [0.013  0.059  0.097  0.059  0.013]
            // [0.022  0.097  0.159  0.097  0.022]
            // [0.013  0.059  0.097  0.059  0.013]
            // [0.003  0.013  0.022  0.013  0.003]
            half4 frag(v2f i) : SV_Target
            {
                float2 uv = i.uv;
                half4 color = half4(0, 0, 0, 0);

                float blur = _BlurSize * 0.005;
                float2 texelSize = float2(blur, blur);

                color += tex2D(_MainTex, uv + texelSize * float2(-2, -2)) * 0.003;
                color += tex2D(_MainTex, uv + texelSize * float2(-1, -2)) * 0.013;
                color += tex2D(_MainTex, uv + texelSize * float2( 0, -2)) * 0.022;
                color += tex2D(_MainTex, uv + texelSize * float2( 1, -2)) * 0.013;
                color += tex2D(_MainTex, uv + texelSize * float2( 2, -2)) * 0.003;

                color += tex2D(_MainTex, uv + texelSize * float2(-2, -1)) * 0.013;
                color += tex2D(_MainTex, uv + texelSize * float2(-1, -1)) * 0.059;
                color += tex2D(_MainTex, uv + texelSize * float2( 0, -1)) * 0.097;
                color += tex2D(_MainTex, uv + texelSize * float2( 1, -1)) * 0.059;
                color += tex2D(_MainTex, uv + texelSize * float2( 2, -1)) * 0.013;

                color += tex2D(_MainTex, uv + texelSize * float2(-2,  0)) * 0.022;
                color += tex2D(_MainTex, uv + texelSize * float2(-1,  0)) * 0.097;
                color += tex2D(_MainTex, uv + texelSize * float2( 0,  0)) * 0.159;
                color += tex2D(_MainTex, uv + texelSize * float2( 1,  0)) * 0.097;
                color += tex2D(_MainTex, uv + texelSize * float2( 2,  0)) * 0.022;

                color += tex2D(_MainTex, uv + texelSize * float2(-2,  1)) * 0.013;
                color += tex2D(_MainTex, uv + texelSize * float2(-1,  1)) * 0.059;
                color += tex2D(_MainTex, uv + texelSize * float2( 0,  1)) * 0.097;
                color += tex2D(_MainTex, uv + texelSize * float2( 1,  1)) * 0.059;
                color += tex2D(_MainTex, uv + texelSize * float2( 2,  1)) * 0.013;

                color += tex2D(_MainTex, uv + texelSize * float2(-2,  2)) * 0.003;
                color += tex2D(_MainTex, uv + texelSize * float2(-1,  2)) * 0.013;
                color += tex2D(_MainTex, uv + texelSize * float2( 0,  2)) * 0.022;
                color += tex2D(_MainTex, uv + texelSize * float2( 1,  2)) * 0.013;
                color += tex2D(_MainTex, uv + texelSize * float2( 2,  2)) * 0.003;

                return color;
            }
            ENDCG
        }
    }
    Fallback "Diffuse"
}
