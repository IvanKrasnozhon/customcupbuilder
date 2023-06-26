Shader "Custom/RotatingScalingShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _RotationAngle ("Rotation Angle", Range(0.0, 360.0)) = 0.0
        _Scale ("Scale", Range(0.0, 2.0)) = 1.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Lambert alpha

        sampler2D _MainTex;
        float _RotationAngle;
        float _Scale;

        struct Input
        {
            float2 uv_MainTex;
        };

        void surf(Input IN, inout SurfaceOutput o)
        {
            // Apply rotation around the center
            float2 pivot = float2(0.5, 0.5);
            float2 offset = IN.uv_MainTex - pivot;
            float angle = radians(_RotationAngle);
            float2 rotatedOffset = float2(
                offset.x * cos(angle) - offset.y * sin(angle),
                offset.x * sin(angle) + offset.y * cos(angle)
            );

            // Apply scaling
            float2 scaledOffset = rotatedOffset / _Scale;
            float2 scaledUV = scaledOffset + pivot;

            // Sample the texture using the rotated and scaled UV coordinates
            fixed4 texColor = tex2D(_MainTex, scaledUV);

            // Assign the color and alpha values to the output surface
            o.Albedo = texColor.rgb;
            o.Alpha = texColor.a;
        }
        ENDCG
    }
}
