using UnityEngine;
using System.Collections;

public class Stats 
{
	public enum side
	{
		RIGHT,
		LEFT 
	}
	public static side playerWinner = side.RIGHT;
	public static int winnerScore = 0;
	public static int loserScore = 0;
}
