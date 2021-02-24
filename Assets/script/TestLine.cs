using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLine : MonoBehaviour
{
	private LineRenderer lineRenderer;
	//定义三个点坐标，可开放为公共在面板自定义坐标
	private Vector3 startP;
	private Vector3 endP;
	private Vector3 topP;
	private Vector3 resultP;
	private List<Vector3> resultPList = new List<Vector3>();

	private float time = 0;
	private float timeLerp;
	private float maxTime = 1f;

	//可自定义模块
	public float width = 10f;  //线段宽度
	public float speed = 1f;   //动画流动速度
	private float roadChange = 0;
	public bool 是否是曲线 = false; //线段直曲选择，默认为直线，勾选后为曲线
	public int 分段 = 1;              //线段分几段
	public bool direction = true;   //线段流动方向，默认为从point1到point2
	public float 弧度 = 1;            //曲线最高点位置，比例值非高度绝对值，负数则为倒转
	public float 偏移 = 0;
	public bool 起终点物体是否隐藏 = false;

	//一开始准备用代码来控制线段颜色，显示有问题，后续修改
	//public Color startcolor = new Color(1f, 1f, 0f, 0.5f);
	//public Color endcolor = new Color(0f, 1f, 1f, 0.5f);
	//public bool 是否自定义线段颜色 = false;
	//public Color linecolor;

	void Start()
	{
		lineRenderer = gameObject.GetComponent<LineRenderer>();
		//传入自定义的线段宽度
		lineRenderer.startWidth = width;
		lineRenderer.endWidth = width;
		//修改线段分段
		lineRenderer.material.SetTextureScale("_MainTex", new Vector2(分段, 1));
		//把场景里标记的起点重点物体的坐标值传入
		startP = transform.GetChild(0).position;
		endP = transform.GetChild(1).position;

		topP = (startP + endP) / 2;
		//处理曲线
		if (是否是曲线)
		{
			double radius = System.Math.Sqrt((startP.x) * (startP.x) + (topP.y) * (topP.y) + (endP.z) * (endP.z));
			float r = (float)radius;
			topP.y += (r * 弧度);
			topP.z += 偏移 * r;
		}
		if (起终点物体是否隐藏)
		{
			transform.GetChild(0).gameObject.SetActive(false);
			transform.GetChild(1).gameObject.SetActive(false);
		}

		//同上用于颜色控制的未完善模块
		/*
		lineRenderer.startColor = startcolor;
		lineRenderer.endColor = endcolor;
		if (是否自定义线段颜色)
		{
			lineRenderer.material.SetColor("_EmissionColor", linecolor);
		}
		*/
	}

	void FixedUpdate()
	{
		CalculatePosition();   //实用贝塞尔公式来生成曲线
		lineRenderer.positionCount = resultPList.ToArray().Length;
		if (lineRenderer.positionCount >= 2)
		{
			lineRenderer.SetPositions(resultPList.ToArray());
		}

		//控制动画流动方向和速度
		if (direction)
		{
			roadChange += Time.deltaTime * speed * -1;
		}
		else
		{
			roadChange += Time.deltaTime * speed;
		}
		lineRenderer.material.SetTextureOffset("_MainTex", new Vector2(roadChange, 0));
	}

	void CalculatePosition()
	{
		resultP = new Vector3();
		timeLerp = Mathf.Lerp(0, 1, time / maxTime);
		resultP.x = Mathf.Pow(1 - timeLerp, 2) * startP.x + 2 * timeLerp * Mathf.Pow(1 - timeLerp, 1) * topP.x + Mathf.Pow(timeLerp, 2) * endP.x;
		resultP.y = Mathf.Pow(1 - timeLerp, 2) * startP.y + 2 * timeLerp * Mathf.Pow(1 - timeLerp, 1) * topP.y + Mathf.Pow(timeLerp, 2) * endP.y;
		resultP.z = Mathf.Pow(1 - timeLerp, 2) * startP.z + 2 * timeLerp * Mathf.Pow(1 - timeLerp, 1) * topP.z + Mathf.Pow(timeLerp, 2) * endP.z;
		resultPList.Add(resultP);
		time += Time.deltaTime;
	}

	void Update()
	{
	}
}

