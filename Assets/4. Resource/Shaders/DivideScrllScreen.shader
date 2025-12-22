Shader "Custom/DivideScrllScreen"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex("InputTex", 2D) = "white" {}
        _ScrollSpeed("Scroll Speed", Float) = 0.5
        _Divisions("Divisions", Int) = 4
     }

     SubShader
     {
        Tags { "RenderType"="Opaque" }
        Blend One Zero

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

            float4      _Color;
            sampler2D   _MainTex;
            float4      _MainTex_ST;
            float       _ScrollSpeed;
            int         _Divisions;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            float4 frag(v2f i) : SV_Target
            {
                float2 uv = i.uv;

                // 화면을 4개로 가로 분할 (위에서 아래로)
                float sectionHeight = 1.0 / _Divisions;
                int sectionIndex = floor(uv.y / sectionHeight);

                // 각 섹션 내의 로컬 UV (0~1)
                float localY = frac(uv.y / sectionHeight);

                // 왼쪽에서 오른쪽으로 순차적으로 스크롤
                // 섹션마다 시간 오프셋 적용 (위에서 아래로 순차)
                float timeOffset = sectionIndex * 0.25; // 각 섹션마다 0.25초 지연
                float scrollOffset = frac(_Time.y * _ScrollSpeed + timeOffset);

                // X 방향으로 스크롤 (왼쪽에서 오른쪽)
                float scrolledX = frac(uv.y + scrollOffset);

                // 최종 UV 계산
                float2 finalUV = float2(uv.x, scrolledX);

                // 텍스처 샘플링
                float4 color = tex2D(_MainTex, finalUV);

                return color;
            }
            ENDCG
        }
    }
}
