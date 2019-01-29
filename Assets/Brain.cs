using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Brain : MonoBehaviour
{

	public class Equation<T, U, V> {
		public Equation() {
		}

		public Equation(T first, U second, V operation) {
			this.First = first;
			this.Second = second;
			this.Operation = operation;
		}

		public T First { get; set; }
		public U Second { get; set; }
		public V Operation { get; set; }
	};
	
	enum Operation
	{
		Addition,
		Subtraction,
		Multiplication,
		Division
	}
	
	
	public Text TopNumber;
	public Text BottonNumber;
	public Text EqualsNumber;
	public Text OperationField;
	public Button DoneButton;

	private int pendingNumber;
	private int correctNumber;

	List<Equation<int,int,Operation>> equations = new List<Equation<int, int, Operation>>(200);
	
	// Use this for initialization
	void Start () {
		fill();
		pick();
		pendingNumber = 0;
	}

	void fill()
	{
		for (int i = 0; i < 10; ++i)
		{
			for (int j = 0; j < 10; ++j)
			{
				equations.Add(new Equation<int, int, Operation>(i, j, Operation.Addition));
				if (i <= j)
				{
					equations.Add(new Equation<int, int, Operation>(i, j, Operation.Subtraction));
				}
				equations.Add(new Equation<int, int, Operation>(i, j, Operation.Multiplication));
				//equations.Add(new Equation<int, int, Operation>(i, j, Operation.Division));
			}
		}
	}

	void pick()
	{
		if (equations.Count == 0)
		{
			fill();
		}
		
		var equation = equations[Random.Range(0, equations.Count)];
		equations.Remove(equation);
		TopNumber.text = "" + equation.First;
		BottonNumber.text = "" + equation.Second;

		switch (equation.Operation)
		{
			case Operation.Addition:
				OperationField.text = "+";
				TopNumber.text = "" + equation.First;
				BottonNumber.text = "" + equation.Second;
				correctNumber = equation.First + equation.Second; 
				break;
			
			case Operation.Multiplication:
				OperationField.text = "x";
				TopNumber.text = "" + equation.First;
				BottonNumber.text = "" + equation.Second;
				correctNumber = equation.First * equation.Second; 
				break;
			
			case Operation.Subtraction:
				OperationField.text = "-";
				int sum = equation.First + equation.Second;
				TopNumber.text = "" + sum;
				BottonNumber.text = "" + equation.Second;
				correctNumber = equation.First; 
				break;
			
			case Operation.Division:
				OperationField.text = "÷";
				int product = equation.First * equation.Second;
				TopNumber.text = "" + product;
				BottonNumber.text = "" + equation.Second;
				correctNumber = equation.First; 
				break;			
		}
		Clear();
	} 
	
	// Update is called once per frame
	void Update ()
	{
		string color = "red";
		DoneButton.enabled = false;
		if (pendingNumber == correctNumber)
		{
			color = "green";
			DoneButton.enabled = true;
		}
		EqualsNumber.text = "<color="+color+">" + pendingNumber + "</color>";
	}

	public void ButtonPressed(int digit)
	{
		pendingNumber *= 10;
		pendingNumber += digit;		
	}

	public void Clear()
	{
		pendingNumber = 0;
	}

	public void Done()
	{
		pick();
	}
}
