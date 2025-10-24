Shader "Custom/HealthWaveShader"
{
    Properties
    {
        _Color ("Wave Color", Color) = (0, 0.8, 1, 1)
        _Health ("Health Level", Range(0,1)) = 1
        _WaveAmplitude ("Wave Amplitude", Range(0,0.2)) = 0.05
        _WaveFrequency ("Wave Frequency", Range(1,10)) = 5
        _WaveSpeed ("Wave Speed", Range(0,10)) = 2
        _MainTex ("Wave Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 100
        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
        ZWrite Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_ST;
            fixed4 _Color;
            float _Health;
            float _WaveAmplitude;
            float _WaveFrequency;
            float _WaveSpeed;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Use Unity built-in _Time
                float wave = sin(i.uv.x * _WaveFrequency + _Time.y * _WaveSpeed) * _WaveAmplitude;
                float fill = _Health + wave;

                // Clip pixels above the liquid level
                if (i.uv.y > fill)
                    discard;

                fixed4 texColor = tex2D(_MainTex, i.uv) * _Color;
                return texColor;
            }
            ENDCG
        }
    }
}
