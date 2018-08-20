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
    private Color color;

    [SerializeField]
    private ColorInfo[] colors;

    
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
