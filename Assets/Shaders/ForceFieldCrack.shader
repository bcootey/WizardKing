Shader "Custom/ForceFieldCrack"
{
    Properties
    {
        _MainColor   ("Main Color", Color) = (0.2, 0.7, 1, 0.4)
        _EdgeColor   ("Edge Color", Color) = (0.8, 1, 1, 1)

        // Black background, white crack lines
        _CrackTex    ("Crack Mask (white = cracks)", 2D) = "white" {}

        // Slider to control how broken the shield is
        _CrackAmount ("Crack Amount", Range(0,1)) = 0

        _RimPower    ("Rim Power", Range(0.5, 8)) = 3
        _RimIntensity("Rim Intensity", Range(0, 3)) = 1
    }

    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 200

        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        Cull Back

        CGPROGRAM
        #pragma surface surf Standard alpha:fade

        sampler2D _CrackTex;
        fixed4 _MainColor;
        fixed4 _EdgeColor;
        half _CrackAmount;
        half _RimPower;
        half _RimIntensity;

        struct Input
        {
            float2 uv_CrackTex;
            float3 viewDir;
        };

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 col = _MainColor;

            // Rim glow (strong on edges, weak in center)
            float3 n = normalize(o.Normal);
            float3 v = normalize(IN.viewDir);
            half rim = 1.0h - saturate(dot(n, v));
            rim = pow(rim, _RimPower);
            col.rgb = lerp(col.rgb, _EdgeColor.rgb, rim * _RimIntensity);

            // Crack alpha mask
            fixed crackMask = tex2D(_CrackTex, IN.uv_CrackTex).r;
            // When CrackAmount = 0 → full alpha
            // When CrackAmount = 1 → areas where crackMask is white become fully transparent
            half alpha = _MainColor.a * saturate(1.0h - _CrackAmount * crackMask);

            if (alpha < 0.01h)
                discard;

            o.Albedo    = 0;          // use emission only so it glows nicely
            o.Emission  = col.rgb;
            o.Metallic  = 0;
            o.Smoothness= 0.8;
            o.Alpha     = alpha;
        }
        ENDCG
    }

    FallBack "Transparent/Diffuse"
}
