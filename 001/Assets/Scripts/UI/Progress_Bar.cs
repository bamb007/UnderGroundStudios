using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Progress_Bar : MonoBehaviour {

    [System.Serializable]
    public class ColorInfo
    {
        public float percent;
        public Color color;
    }

    [SerializeField]
    private int max;

    [SerializeField]
    private int value;

    [SerializeField]
    private RectTransform rectTransform;

    [SerializeField]
    private RectTransform animTransform;

    [SerializeField]
    private Color color;

    [SerializeField]
    private ColorInfo[] colors;

    private float animSmoothness = 750;
    private float animValue;
    
    public int Max
    {
        get
        {
            return max;
        }
        set
        {
            max = value;
            Resize();
        }
    }

    public int Value
    {
        get
        {
            return value;
        }
        set
        {
            this.value = value;
            Resize();
        }
    }    

    private float width;

	void Start () {
        width = rectTransform.sizeDelta.x;
        Resize();
        animValue = value;
	}

    private void Update()
    {
        if (animValue == value)
        {
            animTransform.sizeDelta = new Vector2(0, rectTransform.sizeDelta.y);
        }
        else if (Mathf.Abs(animValue - value) < 1)
        {
            animValue = value;
        }
        else
        {
            float dt = 1 - Mathf.Exp(-(Time.deltaTime * 1000) / animSmoothness);
            animValue = (value - animValue) * dt + animValue;

            float procent = (float)value / (float)max;
            animTransform.position = new Vector3(width * procent + rectTransform.position.x, animTransform.position.y, animTransform.position.z);

            float animProcent = (animValue - value) / (float)max;
            animTransform.sizeDelta = new Vector2(width * animProcent, animTransform.sizeDelta.y);
        }
    }

    private void Resize()
    {
        float procent = (float)value / (float)max;
        rectTransform.sizeDelta = new Vector2(width * procent, rectTransform.sizeDelta.y);

        Image image = GetComponent<Image>();

        if (image != null)
        {
            Color next_color = color;

            for (int i = 0; i < colors.Length; i++)
            {
                if (procent <= colors[i].percent)
                {
                    next_color = colors[i].color;
                }
            }

            image.color = next_color;
        }
    }
}
