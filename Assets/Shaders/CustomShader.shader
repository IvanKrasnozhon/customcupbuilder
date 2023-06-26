Shader "Custom/RotationShader"
{
Properties
{
_MainTex ("Main Texture", 2D) = "white" {}
_RotationAngle ("Rotation Angle", Range(0, 360)) = 0
}
    SubShader
{
    Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }
    Lighting Off
    ZWrite Off
    Blend SrcAlpha OneMinusSrcAlpha

    Pass
    {
        CGPROGRAM
        #pragma vertex vert
        #pragma fragment frag

        #include "UnityCG.cginc"

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

        float _RotationAngle;
        float2x2 _RotationMatrix;

        v2f vert(appdata v)
        {
            v2f o;
            o.vertex = UnityObjectToClipPos(v.vertex);
            o.uv = mul(_RotationMatrix, v.uv);
            return o;
        }

        sampler2D _MainTex;

        fixed4 frag(v2f i) : SV_Target
        {
            fixed4 col = tex2D(_MainTex, i.uv);
            return col;
        }
        ENDCG
    }
}
}